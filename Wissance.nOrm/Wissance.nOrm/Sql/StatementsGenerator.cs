using System.Text;

namespace Wissance.nOrm.Sql
{
    public static class StatementsGenerator
    {
        public static string GenerateSelectSql()
        {
            StringBuilder sb = new StringBuilder();

            // ...
            
            return sb.ToString();
        }
        
        public const string SelectAllColumns = "*";
        private const string SelectTemplate = "SELECT {0} FROM {1} ";
        private const string WhereStatement = " WHERE ";
        private const string OrderByTemplate = " ORDER BY {0} {1}";
        private const string GroupByTemplate = " GROUP BY {0}";

        private static IDictionary<JoinCondition, string> _conditionsStatements = new Dictionary<JoinCondition, string>()
        {
            {JoinCondition.Or, " OR "},
            {JoinCondition.And, " AND "},
            {JoinCondition.Not, " NOT "}
        };
    }
}