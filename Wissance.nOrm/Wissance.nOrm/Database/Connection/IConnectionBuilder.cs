using System.Data.Common;

namespace Wissance.nOrm.Database.Connection
{
    public interface IConnectionBuilder
    {
        DbConnection BuildConnection(string connStr);
    }
}