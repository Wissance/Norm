using System.Data.Common;
using Npgsql;
using Wissance.nOrm.Database.Parameter;

namespace Wissance.nOrm.Postgres.Parameter
{
    public class PostgresParameterBuilder : IParameterBuilder
    {
        public DbParameter Build(string name, object value)
        {
            return new NpgsqlParameter(name, value);
        }
    }
}