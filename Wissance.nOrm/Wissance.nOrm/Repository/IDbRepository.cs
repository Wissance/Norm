namespace Wissance.nOrm.Repository
{
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
        // todo(UMV): add delete in future (here we don't need it)
    }
}