using System.Data.Common;
using MySql.Data.MySqlClient;
using Wissance.nOrm.Database.Command;

namespace Wissance.nOrm.MySql.Database.Command
{
    internal class MySqlCommandBuilder : ICommandBuilder
    {
        public DbCommand BuildCommand(string sqlCmd, DbConnection conn)
        {
            return new MySqlCommand(sqlCmd, conn as MySqlConnection);
        }
    }
}