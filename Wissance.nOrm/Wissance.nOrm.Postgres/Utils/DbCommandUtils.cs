using System.Data.Common;
using Npgsql;
using Wissance.nOrm.Sql;

namespace Wissance.nOrm.Postgres.Utils
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
                    NpgsqlParameter p1 = new NpgsqlParameter(pName, parameter.FilterValues[0]);
                    command.Parameters.Add(p1);
                    
                    parameterCounter++;
                    pName = $"@p{parameterCounter}";
                    NpgsqlParameter p2 = new NpgsqlParameter(pName, parameter.FilterValues[1]);
                    command.Parameters.Add(p2);
                }
                else
                {
                    if (parameter.FilterValues.Count == 1)
                    {
                        NpgsqlParameter p = new NpgsqlParameter(pName, parameter.FilterValues[0]);
                        command.Parameters.Add(p);
                    }
                    else
                    {
                        // probably IN (....)
                        NpgsqlParameter p = new NpgsqlParameter(pName, parameter.FilterValues);
                        command.Parameters.Add(p);
                    }
                }
                
                parameterCounter++;
            }
        }
    }
}