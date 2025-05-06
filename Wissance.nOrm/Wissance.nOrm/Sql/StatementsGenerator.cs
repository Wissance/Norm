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
                string values = string.Join(",", parameter.FilterValues.Select(v => parameter.ValueQuotesWrap ? $"'{v}'" : v));
                string template = FilterStatementsTemplates[parameter.ComparisonOperator];
                string fullComparison = String.Format(template, values);
                sb.Append(fullComparison);
            }
           
            return sb.ToString();
        }

        /*public static string GenerateSelectSql(IList<string> columns, string schema, string table,  IList<WhereParameter> parameters)
        {
            StringBuilder sb = new StringBuilder();

            // ...
            
            // ...
            return sb.ToString();
        }*/
        
        public const string SelectAllColumns = "*";
        private const string SelectTemplate = "SELECT {0} FROM {1} ";
        private const string JoinStatement = "{0} JOIN {} ON ";
        private const string WhereStatement = " WHERE ";
        private const string OrderByTemplate = " ORDER BY {0} {1}";
        private const string GroupByTemplate = " GROUP BY {0}";

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
            {WhereComparison.Between, " BETWEEN ({0}) "}
        };
    }
}