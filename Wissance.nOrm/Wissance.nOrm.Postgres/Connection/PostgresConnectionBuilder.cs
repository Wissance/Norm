using System.Data.Common;
using Npgsql;
using Wissance.nOrm.Database.Connection;

namespace Wissance.nOrm.Postgres.Connection
{
    public class PostgresConnectionBuilder : IConnectionBuilder
    {
        public DbConnection BuildConnection(string connStr)
        {
            return new NpgsqlConnection(connStr);
        }
    }
}