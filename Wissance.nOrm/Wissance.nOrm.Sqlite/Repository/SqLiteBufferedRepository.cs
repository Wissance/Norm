using Microsoft.Extensions.Logging;
using Wissance.nOrm.Entity.QueryBuilders;
using Wissance.nOrm.Repository;
using Wissance.nOrm.Settings;

namespace Wissance.nOrm.Sqlite.Repository
{
    public class SqLiteBufferedRepository<T>: BufferedDbRepository<T>
        where T: class, new()
    {
        public SqLiteBufferedRepository(string connStr, DbRepositorySettings settings, IDbEntityQueryBuilder<T> sqlBuilder, Func<object[], IList<string>, T> entityFactoryFunc, ILoggerFactory loggerFactory) 
            : base(connStr, settings, new SqLiteAdapter(), sqlBuilder, entityFactoryFunc, loggerFactory)
        {
        }
    }
}