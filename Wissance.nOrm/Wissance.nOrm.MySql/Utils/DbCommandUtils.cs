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
            const string valuesJoin = ",";
            foreach (WhereParameter parameter in whereParameters)
            {
                string pName = $"$@p{parameterCounter}";
                
                if (parameter.ComparisonOperator == WhereComparison.Between)
                {
                    object v1 = parameter.ValueQuotesWrap ? $"'{parameter.FilterValues[0]}'" : parameter.FilterValues[0];
                    MySqlParameter p1 = new MySqlParameter(pName, v1);
                    command.Parameters.Add(p1);
                    
                    parameterCounter++;
                    pName = $"$@p{parameterCounter}";
                    object v2 = parameter.ValueQuotesWrap ? $"'{parameter.FilterValues[1]}'" : parameter.FilterValues[1];
                    MySqlParameter p2 = new MySqlParameter(pName, v2);
                    command.Parameters.Add(p2);
                }
                else
                {
                    string values = string.Join(valuesJoin, parameter.FilterValues.Select(v => parameter.ValueQuotesWrap ? $"'{v}'" : v));
                    MySqlParameter p = new MySqlParameter(pName, values);
                    command.Parameters.Add(p);
                }
                
                parameterCounter++;
            }
        }
    }
}