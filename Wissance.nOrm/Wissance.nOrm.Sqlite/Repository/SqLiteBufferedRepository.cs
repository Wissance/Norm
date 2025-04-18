using Microsoft.Extensions.Logging;
using Wissance.nOrm.Entity.QueryBuilders;
using Wissance.nOrm.Repository;

namespace Wissance.nOrm.Sqlite.Repository
{
    public class SqLiteBufferedRepository<T>: BufferedDbRepository<T>
        where T: class, new()
    {
        public SqLiteBufferedRepository(string connStr, int bufferThreshold, IDbEntityQueryBuilder<T> sqlBuilder, Func<object[], IList<string>, T> entityFactoryFunc, ILoggerFactory loggerFactory) 
            : base(connStr,bufferThreshold, new SqLiteAdapter(), sqlBuilder, entityFactoryFunc, loggerFactory)
        {
        }
    }
}