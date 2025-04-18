using System.Data.Common;
using System.Data.SQLite;
using Wissance.nOrm.Database.Command;

namespace Wissance.nOrm.Sqlite.Command
{
    public class SqLiteCommandBuilder : ICommandBuilder
    {
        public DbCommand BuildCommand(string sqlCmd, DbConnection conn)
        {
            return new SQLiteCommand(sqlCmd, conn as SQLiteConnection);
        }
    }
}