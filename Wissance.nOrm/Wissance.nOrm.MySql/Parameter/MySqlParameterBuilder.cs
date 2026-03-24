using System.Data.Common;
using MySql.Data.MySqlClient;
using Wissance.nOrm.Database.Parameter;

namespace Wissance.nOrm.MySql.Parameter
{
    public class MySqlParameterBuilder : IParameterBuilder
    {
        public DbParameter Build(string name, object value)
        {
            return new MySqlParameter(name, value);
        }
    }
}