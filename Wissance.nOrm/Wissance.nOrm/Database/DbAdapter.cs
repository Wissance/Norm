using Wissance.nOrm.Database.Command;
using Wissance.nOrm.Database.Connection;

namespace Wissance.nOrm.Database
{
    public class DbAdapter
    {
        public DbAdapter(IConnectionBuilder connBuilder, ICommandBuilder cmdBuilder)
        {
            ConnBuilder = connBuilder;
            CmdBuilder = cmdBuilder;
        }

        public ICommandBuilder CmdBuilder { get; internal set; }
        public IConnectionBuilder ConnBuilder { get; internal set; }
    }
}