using System.Data.Common;
using System.Data.SQLite;
using Wissance.nOrm.Sql;

namespace Wissance.nOrm.Sqlite.Utils
{
    public static class DbCommandUtils
    {
        public static void PassDbParameters(DbCommand command, IList<WhereParameter> whereParameters)
        {
            if (whereParameters == null)
                return;
            int parameterCounter = 1;
            foreach (WhereParameter parameter in whereParameters)
            {
                string pName = $"@p{parameterCounter}";
                
                if (parameter.ComparisonOperator == WhereComparison.Between)
                {
                    SQLiteParameter p1 = new SQLiteParameter(pName, parameter.FilterValues[0]);
                    command.Parameters.Add(p1);
                    
                    parameterCounter++;
                    pName = $"@p{parameterCounter}";
                    SQLiteParameter p2 = new SQLiteParameter(pName, parameter.FilterValues[1]);
                    command.Parameters.Add(p2);
                }
                else
                {
                    if (parameter.FilterValues.Count == 1)
                    {
                        SQLiteParameter p = new SQLiteParameter(pName, parameter.FilterValues[0]);
                        command.Parameters.Add(p);
                    }
                    else
                    {
                        // probably IN (....)
                        SQLiteParameter p = new SQLiteParameter(pName, parameter.FilterValues);
                        command.Parameters.Add(p);
                    }
                }
                
                parameterCounter++;
            }
        }
    }
}