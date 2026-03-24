using System.Data.Common;
using MySql.Data.MySqlClient;
using Wissance.nOrm.Sql;

namespace Wissance.nOrm.MySql.Utils
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
                    MySqlParameter p1 = new MySqlParameter(pName, parameter.FilterValues[0]);
                    command.Parameters.Add(p1);
                    
                    parameterCounter++;
                    pName = $"@p{parameterCounter}";
                    MySqlParameter p2 = new MySqlParameter(pName, parameter.FilterValues[1]);
                    command.Parameters.Add(p2);
                }
                else
                {
                    if (parameter.FilterValues.Count == 1)
                    {
                        MySqlParameter p = new MySqlParameter(pName, parameter.FilterValues[0]);
                        command.Parameters.Add(p);
                    }
                    else
                    {
                        // probably IN (....)
                        MySqlParameter p = new MySqlParameter(pName, parameter.FilterValues);
                        command.Parameters.Add(p);
                    }
                }
                
                parameterCounter++;
            }
        }
    }
}