using Wissance.nOrm.Entity;

namespace Wissance.nOrm.Repository
{
    public interface IDbRepository<T, TId> : IDisposable 
        where T: class, IDbEntityQueryBuilder<TId>, new()
        where TId: class
    {
        public Task<IList<T>> GetManyAsync(IDictionary<string, object> whereClause, int? page, int? size);
        public Task<T> GetOneAsync(IDictionary<string, object> whereClause);
        public Task<bool> InsertAsync(T item, bool immediately);              // immediately = true means now, otherwise via background process
        public Task<int> BulkInsertAsync(IList<T> items, bool immediately);  // immediately means now, otherwise via background process
        public Task<bool> UpdateAsync(T item, bool immediately);                 // immediately means now, otherwise via background process
        public Task<int> BulkUpdateAsync(IList<T> items, bool immediately);      // immediately means now, otherwise via background process
        
        // Force Synchonize all buffered changes (to insert + to update), if items = null then synch ALL!
        public Task SyncAsync(int[] items);
        // todo(UMV): add delete in future (here we don't need it)
    }
}