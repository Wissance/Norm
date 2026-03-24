using System.Text;
using Microsoft.VisualBasic;

namespace Wissance.nOrm.Sql
{
    // todo(UMV): temporarily not export until https://github.com/Wissance/Norm/issues/2 is solved
    public static class StatementsGenerator
    {
        public static string BuildWhereStatement(IList<WhereParameter> parameters)
        {
            StringBuilder sb = new StringBuilder();
            foreach (WhereParameter parameter in parameters)
            {
                if (parameter.JoinCondition != null)
                {
                    sb.Append($" {JoinStatements[parameter.JoinCondition.Value]} ");
                }

                sb.Append($"{ parameter.Column }");
                if (parameter.Inverted)
                {
                    sb.Append(" NOT ");
                }
                // todo(umv): formatter required
                string valuesJoin = ",";

                if (parameter.ComparisonOperator == WhereComparison.Between)
                {
                    valuesJoin = " AND ";
                }

                string values = string.Join(valuesJoin, parameter.FilterValues.Select(v => parameter.ValueQuotesWrap ? $"'{v}'" : v));
                string template = FilterStatementsTemplates[parameter.ComparisonOperator];
                string fullComparison = String.Format(template, values);
                sb.Append(fullComparison);
            }
            if (sb.Length > 0)
            {
                sb.Insert(0, "WHERE ");
            }
            return sb.ToString();
        }

        public static string BuildWherePreparedStatement(IList<WhereParameter> parameters)
        {
            StringBuilder sb = new StringBuilder();
            int paramCounter = 1;
            foreach (WhereParameter parameter in parameters)
            {
                if (parameter.JoinCondition != null)
                {
                    sb.Append($" {JoinStatements[parameter.JoinCondition.Value]} ");
                }

                sb.Append($"{ parameter.Column }");
                if (parameter.Inverted)
                {
                    sb.Append(" NOT ");
                }
                
                string template = FilterStatementsTemplates[parameter.ComparisonOperator];
                string value = "";
                if (parameter.ComparisonOperator == WhereComparison.Between)
                {
                    value = string.Format(template, $"@p{paramCounter} AND @p{++paramCounter}");
                }
                else
                {
                    value = string.Format(template, $"@p{paramCounter}");
                }

                sb.Append(value);
                paramCounter++;
            }

            if (sb.Length > 0)
            {
                sb.Insert(0, "WHERE ");
            }
            return sb.ToString();
        }

        private static readonly IDictionary<WhereJoinCondition, string> JoinStatements = new Dictionary<WhereJoinCondition, string>()
        {
            {WhereJoinCondition.Or, " OR "},
            {WhereJoinCondition.And, " AND "}
        };

        private static readonly IDictionary<WhereComparison, string> FilterStatementsTemplates = new Dictionary<WhereComparison, string>()
        {
            {WhereComparison.Equal, " = {0} "},
            {WhereComparison.NotEqual, " != {0} "},
            {WhereComparison.Less, " < {0} "},
            {WhereComparison.LessOrEqual, " <= {0} "},
            {WhereComparison.Greater, " > {0} "},
            {WhereComparison.GreaterOrEqual, " >= {0} "},
            {WhereComparison.In, " IN ({0}) "},
            {WhereComparison.Between, " BETWEEN {0} "}
        };
    }
}