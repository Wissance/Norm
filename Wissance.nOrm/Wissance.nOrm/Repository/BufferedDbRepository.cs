using System.Data.Common;
using Microsoft.Extensions.Logging;
using Wissance.nOrm.Database;
using Wissance.nOrm.Database.Connection;
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
        /// <param name="dbAdapter">Adapter that contains all required ADO Net Builders</param>
        /// <param name="sqlBuilder">Builds Sql queries for provided entities</param>
        /// <param name="entityFactoryFunc">Function that creates item of type T from list of column values</param>
        /// <param name="loggerFactory"></param>
        public BufferedDbRepository(string connStr, DbAdapter dbAdapter, IDbEntityQueryBuilder<T> sqlBuilder, 
            Func<object[], T> entityFactoryFunc, ILoggerFactory loggerFactory)
        {
            _connStr = connStr;
            _dbAdapter = dbAdapter;
            _sqlBuilder = sqlBuilder;
            _entityFactoryFunc = entityFactoryFunc;
            _logger = loggerFactory.CreateLogger<BufferedDbRepository<T>>();
        }

        public void Dispose()
        {
            _cancellationSource.Cancel();
        }

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
                            T obj = _entityFactoryFunc(tableColumns);
                            if (obj != null)
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
                            item = _entityFactoryFunc(tableColumns);
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
            throw new NotImplementedException();
        }

        public async Task<int> BulkInsertAsync(IList<T> items, bool immediately)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(T item, bool immediately)
        {
            throw new NotImplementedException();
        }

        public async Task<int> BulkUpdateAsync(IList<T> items, bool immediately)
        {
            throw new NotImplementedException();
        }

        public async Task SyncAsync(int[] items)
        {
            throw new NotImplementedException();
        }

        private readonly string _connStr;
        private readonly DbAdapter _dbAdapter;
        private readonly IDbEntityQueryBuilder<T> _sqlBuilder;
        private readonly ILogger<BufferedDbRepository<T>> _logger;
        private readonly Func<object[], T> _entityFactoryFunc;
        private readonly CancellationTokenSource _cancellationSource = new CancellationTokenSource();
    }
}