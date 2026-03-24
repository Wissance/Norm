using System.Data.Common;

namespace Wissance.nOrm.Database.Parameter
{
    public interface IParameterBuilder
    {
        DbParameter Build(string name, object value);
    }
}