using Wissance.nOrm.Database;
using Wissance.nOrm.Database.Command;
using Wissance.nOrm.Database.Connection;
using Wissance.nOrm.SqlServer.Command;
using Wissance.nOrm.SqlServer.Connection;

namespace Wissance.nOrm.SqlServer
{
    public class SqlServerAdapter : DbAdapter
    {
        public SqlServerAdapter() 
            : base(new SqlServerConnectionBuilder(), new SqlServerCommandBuilder())
        {
        }
    }
}