using System.Text;
using Wissance.nOrm.Entity.QueryBuilders;

namespace Wissance.nOrm.Tests.Database.Entity.Builders
{
    internal class PhysicalValueQueryBuilder : IDbEntityQueryBuilder<PhysicalValueEntity>
    {
        public string BuildSelectManyQuery(int? page, int? size, IDictionary<string, object> whereClause = null, IList<string> columns = null)
        {
            string columnsList = string.Join(", ", FullColumnsList);
            if (columns != null && columns.Any())
            {
                columnsList = string.Join(", ", columns);
            }

            string whereStatement = String.Empty;
            /*if (whereClause != null && whereClause.Any())
            {
                whereStatement = string.Join(", ", whereClause.Select(kv => $"{kv.Key}"));
            }*/

            string limitStatement = String.Empty;
            if (page.HasValue && size.HasValue)
            {
                int offsetValue = page.Value > 0 ? (page.Value - 1) * size.Value : 0;
                limitStatement = $" LIMIT {size.Value} OFFSET {offsetValue}";
            }

            // Here is a scheme for query : 0 -> column list, 1 -> Table name 2 -> WHERE Clause
            string query = String.Format("SELECT {0} FROM {1} {2} {3}", columnsList, TableName, whereStatement, limitStatement);
            return query;
        }

        public string BuildSelectOneQuery(IDictionary<string, object> whereClause = null, IList<string> columns = null)
        {
            string columnsList = string.Join(", ", FullColumnsList);
            if (columns != null && columns.Any())
            {
                columnsList = string.Join(", ", columns);
            }
            
            string whereStatement = String.Empty;
            if (whereClause != null && whereClause.Any())
            {
                string whereStatementVal = string.Join(", ", whereClause.Select(kv =>
                    kv.Key == "id" ? $"{kv.Key}={kv.Value}" : $"{kv.Key}='{kv.Value}'"));
                whereStatement = $" WHERE {whereStatementVal}";
            }
            string query = String.Format("SELECT {0} FROM {1} {2} LIMIT 1", columnsList, TableName, whereStatement);
            return query;
        }

        public string BuildInsertSqlQuery(PhysicalValueEntity entity)
        {
            throw new NotImplementedException();
        }

        public string BuildUpdateSqlQuery(PhysicalValueEntity entity)
        {
            throw new NotImplementedException();
        }

        public string GetTableSchema()
        {
            return String.Empty;
        }

        public string GetTableName()
        {
            return TableName;
        }

        public string GetModelType()
        {
            return "PhysicalValue";
        }

        private const string TableName = "`physical_values`";
        public static IList<string> FullColumnsList = new List<string>(){"id", "name", "designation", "description"};
    }
}