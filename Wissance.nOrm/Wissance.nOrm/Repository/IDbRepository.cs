namespace Wissance.nOrm.Repository
{
    /// <summary>
    ///   This interface describes operation over persistent Entities. Entity could be:
    ///       1. a database table
    ///       2. an aggregate a composition of tables
    ///   1. In General we are support traditional common operations:
    ///       1.1 Read (select) many entities with filtering
    ///       1.2 Read (select) one entities with filtering
    ///       1.3 Create (insert) an one entity
    ///       1.4 Update (update) en existing entity
    ///       1.5 Delete (delete) an existing entity
    ///   2. Additionally we are support bulk operations:
    ///       2.1 Create multiple (Bulk insert) entities
    ///       2.2 Update multiple (Bulk update) entities
    /// </summary>
    /// <typeparam name="T">Entity that is ether direct mapping to table or aggregate</typeparam>
    public interface IDbRepository<T> : IDisposable 
        where T: class, new()
    {
        public Task<IList<T>> GetManyAsync(int? page, int? size, IDictionary<string, object> whereClause = null, 
            IList<string> columns = null);
        public Task<T> GetOneAsync(IDictionary<string, object> whereClause = null, IList<string> columns = null);
        public Task<bool> InsertAsync(T item, bool immediately);             // immediately = true means now, otherwise via background process
        public Task<int> BulkInsertAsync(IList<T> items, bool immediately);  // immediately means now, otherwise via background process
        public Task<bool> UpdateAsync(T item, bool immediately);             // immediately means now, otherwise via background process
        public Task<int> BulkUpdateAsync(IList<T> items, bool immediately);  // immediately means now, otherwise via background process
        public Task<bool> DeleteAsync(IDictionary<string, object> whereClause);
        // Force Synchronize all buffered changes (to insert + to update), if items = null then synch ALL!
        public Task SyncAsync(int[] items);
    }
}