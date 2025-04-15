using Wissance.nOrm.Database;
using Wissance.nOrm.Database.Command;
using Wissance.nOrm.Database.Connection;
using Wissance.nOrm.Postgres.Command;
using Wissance.nOrm.Postgres.Connection;

namespace Wissance.nOrm.Postgres
{
    public class PostgresAdapter : DbAdapter
    {
        public PostgresAdapter() 
            : base(new PostgresConnectionBuilder(), new PostgresCommandBuilder())
        {
        }
    }
}