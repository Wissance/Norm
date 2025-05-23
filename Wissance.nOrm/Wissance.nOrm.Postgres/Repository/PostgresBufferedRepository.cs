using Microsoft.Extensions.Logging;
using Wissance.nOrm.Entity.QueryBuilders;
using Wissance.nOrm.Repository;
using Wissance.nOrm.Settings;

namespace Wissance.nOrm.Postgres.Repository
{
    public class PostgresBufferedRepository<T>: BufferedDbRepository<T>
        where T: class, new()
    {
        public PostgresBufferedRepository(string connStr, DbRepositorySettings settings, IDbEntityQueryBuilder<T> sqlBuilder, Func<object[], IList<string>, T> entityFactoryFunc, ILoggerFactory loggerFactory) 
            : base(connStr, settings, new PostgresAdapter(), sqlBuilder, entityFactoryFunc, loggerFactory)
        {
        }
    }
}