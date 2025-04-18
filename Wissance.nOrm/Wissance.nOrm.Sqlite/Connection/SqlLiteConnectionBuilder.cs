using System.Data.Common;
using System.Data.SQLite;
using Wissance.nOrm.Database.Connection;

namespace Wissance.nOrm.Sqlite.Connection
{
    public class SqlLiteConnectionBuilder : IConnectionBuilder
    {
        public DbConnection BuildConnection(string connStr)
        {
            return new SQLiteConnection(connStr);
        }
    }
}