using System;
using Microsoft.Extensions.Logging;
using Wissance.nOrm.Database;
using Wissance.nOrm.Entity.QueryBuilders;
using Wissance.nOrm.MySql.Database;
using Wissance.nOrm.Repository;

namespace Wissance.nOrm.MySql.Repository
{
    public class MySqlBufferedRepository<T> : BufferedDbRepository<T>
        where T: class, new()
    {
        public MySqlBufferedRepository(string connStr, IDbEntityQueryBuilder<T> sqlBuilder, Func<object[], IList<string>, T> entityFactoryFunc, ILoggerFactory loggerFactory) 
            : base(connStr,new MySqlAdapter(), sqlBuilder, entityFactoryFunc, loggerFactory)
        {
        }
    }
}