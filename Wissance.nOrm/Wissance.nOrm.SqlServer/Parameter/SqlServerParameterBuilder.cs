using System.Data.Common;
using System.Data.SqlClient;
using Wissance.nOrm.Database.Parameter;

namespace Wissance.nOrm.SqlServer.Parameter
{
    internal class SqlServerParameterBuilder : IParameterBuilder
    {
        public DbParameter Build(string name, object value)
        {
            return new SqlParameter(name, value);
        }
    }
}