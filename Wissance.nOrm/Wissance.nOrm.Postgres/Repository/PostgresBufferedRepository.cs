using Microsoft.Extensions.Logging;
using Wissance.nOrm.Entity.QueryBuilders;
using Wissance.nOrm.Repository;

namespace Wissance.nOrm.Postgres.Repository
{
    public class PostgresBufferedRepository<T>: BufferedDbRepository<T>
        where T: class, new()
    {
        public PostgresBufferedRepository(string connStr, int bufferThreshold, IDbEntityQueryBuilder<T> sqlBuilder, Func<object[], IList<string>, T> entityFactoryFunc, ILoggerFactory loggerFactory) 
            : base(connStr,bufferThreshold, new PostgresAdapter(), sqlBuilder, entityFactoryFunc, loggerFactory)
        {
        }
    }
}