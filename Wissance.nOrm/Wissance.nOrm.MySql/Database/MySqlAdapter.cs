using Wissance.nOrm.Database;
using Wissance.nOrm.MySql.Database.Command;
using Wissance.nOrm.MySql.Database.Connection;
using Wissance.nOrm.MySql.Parameter;

namespace Wissance.nOrm.MySql.Database
{
   internal class MySqlAdapter : DbAdapter 
    {
        public MySqlAdapter() 
            : base(new MySqlConnectionBuilder(), new MySqlCommandBuilder(), new MySqlParameterBuilder())
        {
        }
    }
}