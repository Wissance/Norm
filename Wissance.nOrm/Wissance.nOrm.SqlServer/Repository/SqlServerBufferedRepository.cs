using Microsoft.Extensions.Logging;
using Wissance.nOrm.Entity.QueryBuilders;
using Wissance.nOrm.Repository;

namespace Wissance.nOrm.SqlServer.Repository
{
    public class SqlServerBufferedRepository<T> : BufferedDbRepository<T>
        where T: class, new()
    {
        public SqlServerBufferedRepository(string connStr, int bufferThreshold, IDbEntityQueryBuilder<T> sqlBuilder, Func<object[], IList<string>, T> entityFactoryFunc, ILoggerFactory loggerFactory) 
            : base(connStr,bufferThreshold, new SqlServerAdapter(), sqlBuilder, entityFactoryFunc, loggerFactory)
        {
        }
    }
}