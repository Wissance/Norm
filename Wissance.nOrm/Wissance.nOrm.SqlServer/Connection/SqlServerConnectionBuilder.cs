using System.Data.Common;
using System.Data.SqlClient;
using Wissance.nOrm.Database.Connection;

namespace Wissance.nOrm.SqlServer.Connection
{
    public class SqlServerConnectionBuilder : IConnectionBuilder
    {
        public DbConnection BuildConnection(string connStr)
        {
            return new SqlConnection(connStr);
        }
    }
}