using System.Text;

namespace Wissance.nOrm.Sql
{
    public static class StatementsGenerator
    {
        public static string GenerateSelectSql(string table, IList<TableJoin> joinTables, IList<WhereParameter> parameters)
        {
            StringBuilder sb = new StringBuilder();

            // ...
            
            // ...
            return sb.ToString();
        }
        
        public const string SelectAllColumns = "*";
        private const string SelectTemplate = "SELECT {0} FROM {1} ";
        private const string JoinStatement = "{0} JOIN {} ON ";
        private const string WhereStatement = " WHERE ";
        private const string OrderByTemplate = " ORDER BY {0} {1}";
        private const string GroupByTemplate = " GROUP BY {0}";

        private static IDictionary<WhereJoinCondition, string> _conditionsStatements = new Dictionary<WhereJoinCondition, string>()
        {
            {WhereJoinCondition.Or, " OR "},
            {WhereJoinCondition.And, " AND "},
            {WhereJoinCondition.Not, " NOT "}
        };
    }
}