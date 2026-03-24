using Wissance.nOrm.Database;
using Wissance.nOrm.Sqlite.Command;
using Wissance.nOrm.Sqlite.Connection;
using Wissance.nOrm.Sqlite.Parameter;

namespace Wissance.nOrm.Sqlite
{
    public class SqLiteAdapter: DbAdapter
    {
        public SqLiteAdapter() 
            : base(new SqlLiteConnectionBuilder(), new SqLiteCommandBuilder(), new SqLiteParameterBuilder())
        {
        }
    }
}