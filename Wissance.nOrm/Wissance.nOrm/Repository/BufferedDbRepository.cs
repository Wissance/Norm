using System.Collections.Concurrent;
using System.Data.Common;
using System.Text;
using Microsoft.Extensions.Logging;
using Wissance.nOrm.Database;
using Wissance.nOrm.Entity.QueryBuilders;

namespace Wissance.nOrm.Repository
{
    public class BufferedDbRepository<T> : IDbRepository<T>
        where T: class, new()
    {
        /// <summary>
        ///     This function construct a new instance of Repository, 1 Repository instance is for 1 table
        /// </summary>
        /// <param name="connStr">Connection string to specific database</param>
        /// <param name="bufferThreshold"> Buffer size to start auto synchronization in background</param>
        /// <param name="dbAdapter">Adapter that contains all required ADO Net Builders</param>
        /// <param name="sqlBuilder">Builds Sql queries for provided entities</param>
        /// <param name="entityFactoryFunc">Function that creates item of type T from list of column values</param>
        /// <param name="loggerFactory"></param>
        public BufferedDbRepository(string connStr, int bufferThreshold, DbAdapter dbAdapter, IDbEntityQueryBuilder<T> sqlBuilder, 
            Func<object[], IList<string>, T> entityFactoryFunc, ILoggerFactory loggerFactory)
        {
            _connStr = connStr;
            _threshold = bufferThreshold;
            _dbAdapter = dbAdapter;
            _sqlBuilder = sqlBuilder;
            _entityFactoryFunc = entityFactoryFunc;
            _logger = loggerFactory.CreateLogger<BufferedDbRepository<T>>();
            
            Task bufferedUpsertTask = new Task(async () =>
            {
                await ProcessBufferedItems();
            }, _cancellationSource.Token);
            bufferedUpsertTask.Start();
        }

        public void Dispose()
        {
            _cancellationSource.Cancel();
            _createSync.Dispose();
            _updateSync.Dispose();
        }

        /// <summary>
        ///     Method for getting collection of entity items with setting size of reading portion (page, size) and
        ///     with configure selecting columns and filtering conditions
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="whereClause"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public async Task<IList<T>> GetManyAsync(int? page, int? size, IDictionary<string, object> whereClause = null, IList<string> columns = null)
        {
            string sql = "";
            try
            {
                T item = new T();
                IList<T> items = new List<T>();
                // 1. Create Connection from Adapter
                using (DbConnection conn = _dbAdapter.ConnBuilder.BuildConnection(_connStr))
                {
                    await conn.OpenAsync(_cancellationSource.Token);
                    sql = _sqlBuilder.BuildSelectManyQuery(page, size, whereClause, columns);
                    // 2. Create Command from Adapter
                    using (DbCommand cmd = _dbAdapter.CmdBuilder.BuildCommand(sql, conn))
                    {
                        // 3. Execute Db Reader && read
                        DbDataReader reader = await cmd.ExecuteReaderAsync(_cancellationSource.Token);
                        // 4. Construct item from a fieldset using a Factory method
                        while (await reader.ReadAsync())
                        {
                            object[] tableColumns = new object[reader.FieldCount];
                            reader.GetValues(tableColumns);
                            item = _entityFactoryFunc(tableColumns, columns);
                            if (item != null)
                            {
                                items.Add(item);
                            }
                            else
                            {
                                _logger.LogWarning(string.Format( "Result of object creation of {0} is NULL, ensure \"_entityFactoryFunc\" works properly", 
                                    typeof(T)));
                            }
                        }
                    }
                    
                    await conn.CloseAsync();
                }

                return items;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred during getting List of objects of type \"{typeof(T)}\" with SQL query: \"{sql}\", error: ${e.Message}");
                _logger.LogDebug(e.ToString());
                return null;
            }
        }

        public async Task<T> GetOneAsync(IDictionary<string, object> whereClause = null, IList<string> columns = null)
        {
            string sql = "";
            try
            {
                T item = null;
                // 1. Create Connection from Adapter
                using (DbConnection conn = _dbAdapter.ConnBuilder.BuildConnection(_connStr))
                {
                    await conn.OpenAsync(_cancellationSource.Token);
                    sql = _sqlBuilder.BuildSelectOneQuery(whereClause, columns);
                    // 2. Create Command from Adapter
                    using (DbCommand cmd = _dbAdapter.CmdBuilder.BuildCommand(sql, conn))
                    {
                        // 3. Execute Db Reader && read
                        DbDataReader reader = await cmd.ExecuteReaderAsync(_cancellationSource.Token);
                        // 4. Construct item from a fieldset using a Factory method
                        while (await reader.ReadAsync())
                        {
                            object[] tableColumns = new object[reader.FieldCount];
                            reader.GetValues(tableColumns);
                            item = _entityFactoryFunc(tableColumns, columns);
                            if (item == null)
                            {
                                _logger.LogWarning(string.Format( "Result of object creation of {0} is NULL, ensure \"_entityFactoryFunc\" works properly",
                                    typeof(T)));
                            }
                        }

                        await conn.CloseAsync();
                    }
                }

                return item;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred during getting object of type \"{typeof(T)}\" with SQL query: \"{sql}\", error: ${e.Message}");
                _logger.LogDebug(e.ToString());
                return null;
            }
        }

        public async Task<bool> InsertAsync(T item, bool immediately)
        {
            string insertQuery = string.Empty;
            try
            {
                if (immediately)
                {
                    insertQuery = _sqlBuilder.BuildInsertSqlQuery(item);
                    int result = await UpsertImpl(insertQuery);
                    return result > 0;
                }
                
                await _createSync.WaitAsync(_cancellationSource.Token);
                int key = _itemsToCreate.Keys.Any() ? _itemsToCreate.Keys.Last() + 1 : 1;
                _itemsToCreate[key] = new List<T>() { item };
                _createSync.Release();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred during insert object of type \"{typeof(T)}\" with SQL query: \"{insertQuery}\", error: ${e.Message}");
                _logger.LogDebug(e.ToString());
                return false;
            }
        }

        public async Task<int> BulkInsertAsync(IList<T> items, bool immediately)
        {
            string bulkInsertQuery = "";
            try
            {
                if (items == null || !items.Any())
                    return 0;
                if (immediately)
                {
                    bulkInsertQuery = _sqlBuilder.BuildBulkInsertSqlQuery(items);

                    int result = await UpsertImpl(bulkInsertQuery);
                    return result;
                }

                await _createSync.WaitAsync(_cancellationSource.Token);
                int key = _itemsToCreate.Keys.Any() ? _itemsToCreate.Keys.Last() + 1 : 1;
                _itemsToCreate[key] = items;
                _createSync.Release();
                return items.Count;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred during bulk insert objects of type \"{typeof(T)}\" with SQL query: \"{bulkInsertQuery}\", error: ${e.Message}");
                _logger.LogDebug(e.ToString());
                return -666;
            }
        }

        public async Task<bool> UpdateAsync(T item, bool immediately)
        {
            string updateQuery = string.Empty;
            try
            {
                if (immediately)
                {
                    updateQuery = _sqlBuilder.BuildUpdateSqlQuery(item);
                    int result = await UpsertImpl(updateQuery);
                    return result > 0;
                }
                
                await _updateSync.WaitAsync(_cancellationSource.Token);
                int key = _itemsToUpdate.Keys.Any() ? _itemsToUpdate.Keys.Last() + 1 : 1;
                _itemsToUpdate[key] = new List<T>() { item };
                _createSync.Release();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred during update objects of type \"{typeof(T)}\" with SQL query: \"{updateQuery}\", error: ${e.Message}");
                _logger.LogDebug(e.ToString());
                return false;
            }
        }

        public async Task<int> BulkUpdateAsync(IList<T> items, bool immediately)
        {
            StringBuilder bulkUpdateQueryBuilder = new StringBuilder();
            try
            {
                if (immediately)
                {
                    foreach (T item in items)
                    {
                        bulkUpdateQueryBuilder.Append(_sqlBuilder.BuildUpdateSqlQuery(item));
                    }
                    
                    int result = await UpsertImpl(bulkUpdateQueryBuilder.ToString());
                    return result;
                }
                
                await _updateSync.WaitAsync(_cancellationSource.Token);
                int key = _itemsToCreate.Keys.Any() ? _itemsToCreate.Keys.Last() + 1 : 1;
                _itemsToUpdate[key] = items;
                _updateSync.Release();
                return items.Count;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred during bulk update objects of type \"{typeof(T)}\" with SQL query: \"{bulkUpdateQueryBuilder.ToString()}\", error: ${e.Message}");
                _logger.LogDebug(e.ToString());
                return -777;
            }
        }

        public async Task<bool> DeleteAsync(IDictionary<string, object> whereClause)
        {
            string deleteQuery = _sqlBuilder.BuildDeleteQuery(whereClause);
            int result = -1;
            DbTransaction transaction = null;
            try
            {
                using (DbConnection conn = _dbAdapter.ConnBuilder.BuildConnection(_connStr))
                {
                    await conn.OpenAsync(_cancellationSource.Token);
                    transaction = await conn.BeginTransactionAsync(_cancellationSource.Token);

                    using (DbCommand cmd = _dbAdapter.CmdBuilder.BuildCommand(deleteQuery, conn))
                    {
                        result = await cmd.ExecuteNonQueryAsync();
                    }
                    
                    await transaction.CommitAsync(_cancellationSource.Token);
                    await conn.CloseAsync();
                }

                return result > 0;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred during delete object of type \"{typeof(T)}\" with SQL query: \"{deleteQuery}\", error: ${e.Message}");
                _logger.LogDebug(e.ToString());
                if (transaction != null)
                    await transaction.RollbackAsync(_cancellationSource.Token);
                return false;
            }
        }

        public async Task SyncAsync(int[] items)
        {
            throw new NotImplementedException();
        }
        
        private async Task<int> UpsertImpl(string upsertQuery)
        {
            int result = 0;
            try
            {
                using (DbConnection conn = _dbAdapter.ConnBuilder.BuildConnection(_connStr))
                {
                    DbTransaction transaction = null;
                    try
                    {
                        await conn.OpenAsync(_cancellationSource.Token);
                        transaction = await conn.BeginTransactionAsync(_cancellationSource.Token);

                        using (DbCommand cmd = _dbAdapter.CmdBuilder.BuildCommand(upsertQuery, conn))
                        {
                            cmd.CommandTimeout = MaxCommandTimeout;
                            result = await cmd.ExecuteNonQueryAsync(_cancellationSource.Token);
                        }

                        await transaction.CommitAsync(_cancellationSource.Token);
                        await conn.CloseAsync();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError($"An error occurred during insert/update object of type \"{typeof(T)}\" with SQL query: \"{upsertQuery}\", error: ${e.Message}");
                        _logger.LogDebug(e.ToString());
                        result = -1;
                        if (transaction != null)
                            await transaction.RollbackAsync(_cancellationSource.Token);
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred during insert/update object of type \"{typeof(T)}\" with SQL query: \"{upsertQuery}\", error: ${e.Message}");
                _logger.LogDebug(e.ToString());
                return -2;
            }
        }
        
        private async Task ProcessBufferedItems()
        {
            T item = new T();
            _logger?.LogInformation($"Background job for insert/update db model items of type: \'{_sqlBuilder.GetTableName()}\' started");
            while (!_cancellationSource.IsCancellationRequested)
            {
                int check = 0;
                // 1. Process create items
                foreach (KeyValuePair<int,IList<T>> pack in _itemsToCreate)
                {
                    // 1.1  Check whether we overcome limit (> _threshold), take empirically some of them ? (what number)
                    check += pack.Value.Count;
                    if (check > _threshold)
                        break;
                }

                if (check >= _threshold)
                {
                    StringBuilder sb = new StringBuilder();
                    await _createSync.WaitAsync();
                    // 1.2  Form Batch Within A transaction
                    foreach (KeyValuePair<int, IList<T>> pack in _itemsToCreate)
                    {
                        foreach (T creatingItem in pack.Value)
                        {
                            sb.Append(_sqlBuilder.BuildInsertSqlQuery(creatingItem));
                            sb.Append(" ");
                        }
                    }
                    
                    _itemsToCreate.Clear();
                    _createSync.Release();
                    
                    // 1.3  Write to Database
                    await UpsertImpl(sb.ToString());
                }
                
                // 2. Process Update items
                check = 0;
                // 2.1  Check whether we overcome limit (> _threshold), take empirically some of them ? (what number)
                foreach (KeyValuePair<int,IList<T>> pack in _itemsToUpdate)
                {
                    // 1.1  Check whether we overcome limit (> _threshold), take empirically some of them ? (what number)
                    check += pack.Value.Count;
                    if (check > _threshold)
                        break;
                }

                if (check >= _threshold)
                {
                    // 2.2  Form Batch Within A transaction
                    StringBuilder sb = new StringBuilder();
                    await _updateSync.WaitAsync();
                    foreach (KeyValuePair<int, IList<T>> pack in _itemsToUpdate)
                    {
                        foreach (T updatingItem in pack.Value)
                        {
                            sb.Append(_sqlBuilder.BuildUpdateSqlQuery(updatingItem));
                            sb.Append(" ");
                        }
                    }
                    _itemsToUpdate.Clear();
                    _updateSync.Release();
                    // 2.3  Write to Database
                    await UpsertImpl(sb.ToString());
                }

                await Task.Delay(100); // todo(UMV) make this param configurable!
            }
            _logger?.LogInformation($"Background job for insert/update db model items of type: \'{_sqlBuilder.GetTableName()}\' stopped");
        }

        private const int MaxCommandTimeout = 120;

        private readonly string _connStr;
        private readonly int _threshold;
        private readonly DbAdapter _dbAdapter;
        private readonly IDbEntityQueryBuilder<T> _sqlBuilder;
        private readonly ILogger<BufferedDbRepository<T>> _logger;
        private readonly Func<object[], IList<string>, T> _entityFactoryFunc;
        
        private readonly CancellationTokenSource _cancellationSource = new CancellationTokenSource();
        private readonly SemaphoreSlim _createSync = new SemaphoreSlim (1);
        private readonly SemaphoreSlim _updateSync = new SemaphoreSlim (1);
        
        private readonly IDictionary<int, IList<T>> _itemsToCreate = new ConcurrentDictionary<int, IList<T>>();
        private readonly IDictionary<int, IList<T>> _itemsToUpdate = new ConcurrentDictionary<int, IList<T>>();
    }
}