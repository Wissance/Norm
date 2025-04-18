using System.Data.Common;
using System.Data.SqlClient;
using Wissance.nOrm.Database.Command;

namespace Wissance.nOrm.SqlServer.Command
{
    public class SqlServerCommandBuilder : ICommandBuilder
    {
        public DbCommand BuildCommand(string sqlCmd, DbConnection conn)
        {
            return new SqlCommand(sqlCmd, conn as SqlConnection);
        }
    }
}