using System.Data.Common;
using System.Data.SQLite;
using Wissance.nOrm.Database.Parameter;

namespace Wissance.nOrm.Sqlite.Parameter
{
    internal class SqLiteParameterBuilder : IParameterBuilder
    {
        public DbParameter Build(string name, object value)
        {
            return new SQLiteParameter(name, value);
        }
    }
}