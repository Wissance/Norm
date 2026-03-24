using Wissance.nOrm.Database.Command;
using Wissance.nOrm.Database.Connection;
using Wissance.nOrm.Database.Parameter;

namespace Wissance.nOrm.Database
{
    public class DbAdapter
    {
        public DbAdapter(IConnectionBuilder connBuilder, ICommandBuilder cmdBuilder, IParameterBuilder parameterBuilder)
        {
            ConnBuilder = connBuilder;
            CmdBuilder = cmdBuilder;
        }

        public ICommandBuilder CmdBuilder { get; internal set; }
        public IConnectionBuilder ConnBuilder { get; internal set; }
        public IParameterBuilder ParamBuilder { get; internal set; }
    }
}