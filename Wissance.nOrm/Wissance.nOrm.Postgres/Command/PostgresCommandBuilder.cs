using System.Data.Common;
using Npgsql;
using Wissance.nOrm.Database.Command;

namespace Wissance.nOrm.Postgres.Command
{
    internal class PostgresCommandBuilder : ICommandBuilder
    {
        public DbCommand BuildCommand(string sqlCmd, DbConnection conn)
        {
            return new NpgsqlCommand(sqlCmd, conn as NpgsqlConnection);
        }
    }
}