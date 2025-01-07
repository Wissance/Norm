using System.Data.Common;

namespace Wissance.nOrm.Database.Command
{
    public interface ICommandBuilder
    {
        DbCommand BuildCommand(string sqlCmd, DbConnection conn);
    }
}