using Microsoft.Extensions.Logging;
using Wissance.nOrm.Entity;

namespace Wissance.nOrm.Repository
{
    public class BufferedDbRepository<T> : IDbRepository<T>
        where T: class, new()
    {
        public BufferedDbRepository(IDbEntityQueryBuilder<T> builder, ILoggerFactory loggerFactory)
        {
            _builder = builder;
            _logger = loggerFactory.CreateLogger<BufferedDbRepository<T>>();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> GetManyAsync(IDictionary<string, object> whereClause, int? page, int? size)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetOneAsync(IDictionary<string, object> whereClause)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(T item, bool immediately)
        {
            throw new NotImplementedException();
        }

        public Task<int> BulkInsertAsync(IList<T> items, bool immediately)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(T item, bool immediately)
        {
            throw new NotImplementedException();
        }

        public Task<int> BulkUpdateAsync(IList<T> items, bool immediately)
        {
            throw new NotImplementedException();
        }

        public Task SyncAsync(int[] items)
        {
            throw new NotImplementedException();
        }

        private readonly IDbEntityQueryBuilder<T> _builder;
        private readonly ILogger<BufferedDbRepository<T>> _logger;
    }
}