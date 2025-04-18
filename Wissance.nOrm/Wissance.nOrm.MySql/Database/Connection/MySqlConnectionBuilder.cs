using System.Data.Common;
using MySql.Data.MySqlClient;
using Wissance.nOrm.Database.Connection;

namespace Wissance.nOrm.MySql.Database.Connection
{
    internal class MySqlConnectionBuilder : IConnectionBuilder
    {
        public DbConnection BuildConnection(string connStr)
        {
            return new MySqlConnection(connStr);
        }
    }
}