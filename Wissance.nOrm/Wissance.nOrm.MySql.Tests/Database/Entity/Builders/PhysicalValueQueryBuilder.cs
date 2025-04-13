using System.Text;
using Wissance.nOrm.Entity.QueryBuilders;

namespace Wissance.nOrm.Tests.Database.Entity.Builders
{
    internal class PhysicalValueQueryBuilder : IDbEntityQueryBuilder<PhysicalValueEntity>
    {
        public string BuildSelectManyQuery(int? page, int? size, IDictionary<string, object> whereClause = null, 
            IList<string> columns = null)
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

            // Consider that in MySQL we don't use Schema in Pg or SQL Server we use Schema.TableName
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
            bool hasIdColumn = entity.Id > 0;
            string queryTemplate = "INSERT INTO {0} ({1} name, description, designation) VALUES({2} '{3}', '{4}', '{5}')";
            string idColumn = hasIdColumn ? "id," : "";
            string idValue = hasIdColumn ? $"{entity.Id}," : "";
            return string.Format(queryTemplate, TableName, idColumn, idValue, entity.Name, entity.Description, entity.Designation);
        }

        public string BuildBulkInsertSqlQuery(IList<PhysicalValueEntity> entities)
        {
            bool hasIdColumn = entities[0].Id > 0;
            string columns = "name, description, designation";
            if (hasIdColumn)
                columns = $"id, {columns}";
            StringBuilder queryBuilder = new StringBuilder($"INSERT INTO {GetTableName()} ({columns}) VALUES");
            bool appendComma = false;
            foreach (PhysicalValueEntity entity in entities)
            {
                if (appendComma)
                    queryBuilder.Append(",\n");

                queryBuilder.Append("(");
                string values = $"'{entity.Name}','{entity.Description}','{entity.Designation}'";
                if (hasIdColumn)
                {
                    queryBuilder.Append($"{entity.Id},");
                }

                queryBuilder.Append(values);
                queryBuilder.Append(")");
                appendComma = true;
            }

            queryBuilder.Append(";");
            return queryBuilder.ToString();
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