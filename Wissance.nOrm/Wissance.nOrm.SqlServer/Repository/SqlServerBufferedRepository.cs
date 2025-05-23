using Microsoft.Extensions.Logging;
using Wissance.nOrm.Entity.QueryBuilders;
using Wissance.nOrm.Repository;
using Wissance.nOrm.Settings;

namespace Wissance.nOrm.SqlServer.Repository
{
    public class SqlServerBufferedRepository<T> : BufferedDbRepository<T>
        where T: class, new()
    {
        public SqlServerBufferedRepository(string connStr, DbRepositorySettings settings, IDbEntityQueryBuilder<T> sqlBuilder, Func<object[], IList<string>, T> entityFactoryFunc, ILoggerFactory loggerFactory) 
            : base(connStr, settings, new SqlServerAdapter(), sqlBuilder, entityFactoryFunc, loggerFactory)
        {
        }
    }
}