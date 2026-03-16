using System.Data.Common;
using System.Data.SqlClient;
using Wissance.nOrm.Sql;

namespace Wissance.nOrm.SqlServer.Utils
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
                    SqlParameter p1 = new SqlParameter(pName, parameter.FilterValues[0]);
                    command.Parameters.Add(p1);
                    
                    parameterCounter++;
                    pName = $"@p{parameterCounter}";
                    SqlParameter p2 = new SqlParameter(pName, parameter.FilterValues[1]);
                    command.Parameters.Add(p2);
                }
                else
                {
                    if (parameter.FilterValues.Count == 1)
                    {
                        SqlParameter p = new SqlParameter(pName, parameter.FilterValues[0]);
                        command.Parameters.Add(p);
                    }
                    else
                    {
                        // probably IN (....)
                        SqlParameter p = new SqlParameter(pName, parameter.FilterValues);
                        command.Parameters.Add(p);
                    }
                }
                
                parameterCounter++;
            }
        }
    }
}