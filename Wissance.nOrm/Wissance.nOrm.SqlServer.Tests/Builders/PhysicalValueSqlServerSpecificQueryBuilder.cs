using System.Data.Common;
using System.Text;
using Wissance.nOrm.Entity.Config;
using Wissance.nOrm.Entity.QueryBuilders;
using Wissance.nOrm.Sql;
using Wissance.nOrm.TestModel.IndustrialMeasure.Entity;

namespace Wissance.nOrm.SqlServer.Tests.Builders
{
    public class PhysicalValueSqlServerSpecificQueryBuilder : SingleTableSqlQueryBuilder<PhysicalValueEntity>
    {
        public PhysicalValueSqlServerSpecificQueryBuilder(EntityConfig config, Action<DbCommand, IList<WhereParameter>> commandParametersHandler,
            Func<string, object, DbParameter> parameterBuilderFunc)
            :base(config, commandParametersHandler, parameterBuilderFunc)
        {
            _config = config;
            _commandParametersHandler = commandParametersHandler;
        }

        public override string BuildSelectManyQuery(int? page, int? size, IList<WhereParameter> whereClause = null, 
            IList<string> columns = null)
        {
            string columnsList = string.Join(", ", _config.FullColumnList);
            if (columns != null && columns.Any())
            {
                columnsList = string.Join(", ", columns);
            }

            string whereStatement = String.Empty;
            if (whereClause != null && whereClause.Any())
            {
                whereStatement = $" {StatementsGenerator.BuildWhereStatement(whereClause)}";
            }

            string limitStatement = String.Empty;
            if (page.HasValue && size.HasValue)
            {
                int offsetValue = page.Value > 0 ? (page.Value - 1) * size.Value : 0;
                limitStatement = $" ORDER BY id OFFSET {offsetValue} ROWS FETCH NEXT {size.Value} ROWS ONLY ";
            }

            // Consider that in MySQL we don't use Schema in Pg or SQL Server we use Schema.TableName
            // Here is a scheme for query : 0 -> column list, 1 -> Table name 2 -> WHERE Clause
            string query = String.Format("SELECT {0} FROM {1} {2} {3}", columnsList, GetTableNameWithScheme(), whereStatement, limitStatement);
            return query;
        }

        public override void BuildSelectManyCommandQueryAndParams(DbCommand command, int? page, int? size, IList<WhereParameter> whereClause = null,
            IList<string> columns = null)
        {
            string columnsList = string.Join(", ", _config.FullColumnList);
            if (columns != null && columns.Any())
            {
                columnsList = string.Join(", ", columns);
            }

            string whereStatement = String.Empty;
            if (whereClause != null && whereClause.Any())
            {
                whereStatement = $" {StatementsGenerator.BuildWherePreparedStatement(whereClause)}";
            }

            string limitStatement = String.Empty;
            if (page.HasValue && size.HasValue)
            {
                int offsetValue = page.Value > 0 ? (page.Value - 1) * size.Value : 0;
                limitStatement = $" ORDER BY id OFFSET {offsetValue} ROWS FETCH NEXT {size.Value} ROWS ONLY ";
            }

            // Consider that in MySQL we don't use Schema in Pg or SQL Server we use Schema.TableName
            // Here is a scheme for query : 0 -> column list, 1 -> Table name 2 -> WHERE Clause
            string query = String.Format("SELECT {0} FROM {1} {2} {3}", columnsList, GetTableNameWithScheme(), whereStatement, limitStatement);
            command.CommandText = query;
            _commandParametersHandler(command, whereClause);
        }
        
        public override string BuildInsertSqlQuery(PhysicalValueEntity entity)
        {
            bool hasIdColumn = entity.Id > 0;
            string queryTemplate = "INSERT INTO {0} ({1} name, description, designation) VALUES({2} '{3}', '{4}', '{5}');";
            string idColumn = hasIdColumn ? "id," : "";
            string idValue = hasIdColumn ? $"{entity.Id}," : "";
            return string.Format(queryTemplate, GetTableNameWithScheme(), idColumn, idValue, entity.Name, entity.Description, entity.Designation);
        }
        
        public override void BuildInsertCommandQueryAndParams(DbCommand command, PhysicalValueEntity entity)
        {
            // command.Parameters.Add()
            bool hasIdColumn = entity.Id > 0;
            string queryTemplate = "INSERT INTO {0} ({1} name, description, designation) VALUES({2} {3}, {4}, {5});";
            string idColumn = hasIdColumn ? "id," : "";
            string query = hasIdColumn
                ? string.Format(queryTemplate, GetTableNameWithScheme(), idColumn, "@p1,", "@p2", "@p3", "@p4")
                : string.Format(queryTemplate, GetTableNameWithScheme(), idColumn, "", "@p1", "@p2", "@p3");
            command.CommandText = query;
            if (hasIdColumn)
                command.Parameters.Add(_parameterBuilderFunc("@p1", entity.Id));
            command.Parameters.Add(_parameterBuilderFunc(hasIdColumn ? "@p2" : "@p1", entity.Name));
            command.Parameters.Add(_parameterBuilderFunc(hasIdColumn ? "@p3" : "@p2", entity.Description));
            command.Parameters.Add(_parameterBuilderFunc(hasIdColumn ? "@p4" : "@p3", entity.Designation));
        }


        public override string BuildBulkInsertSqlQuery(IList<PhysicalValueEntity> entities)
        {
            bool hasIdColumn = entities[0].Id > 0;
            string columns = "name, description, designation";
            if (hasIdColumn)
                columns = $"id, {columns}";
            StringBuilder queryBuilder = new StringBuilder($"INSERT INTO {GetTableNameWithScheme()} ({columns}) VALUES");
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
        
        public override void BuildBulkInsertCommandQueryAndParams(DbCommand command, IList<PhysicalValueEntity> entities)
        {
            bool hasIdColumn = entities[0].Id > 0;
            string columns = "name, description, designation";
            if (hasIdColumn)
                columns = $"id, {columns}";
            StringBuilder queryBuilder = new StringBuilder($"INSERT INTO {GetTableNameWithScheme()} ({columns}) VALUES");
            bool appendComma = false;
            int objCounter = 1;
            foreach (PhysicalValueEntity entity in entities)
            {
                if (appendComma)
                    queryBuilder.Append(",\n");

                queryBuilder.Append("(");
                string values = hasIdColumn 
                    ? $"@p{objCounter}1, @p{objCounter}2, @p{objCounter}3, @p{objCounter}4" 
                    : $"@p{objCounter}1, @p{objCounter}2, @p{objCounter}3";
                if (hasIdColumn)
                    command.Parameters.Add(_parameterBuilderFunc($"@p{objCounter}1", entity.Id));
                command.Parameters.Add(_parameterBuilderFunc(hasIdColumn ? $"@p{objCounter}2" : $"@p{objCounter}1", entity.Name));
                command.Parameters.Add(_parameterBuilderFunc(hasIdColumn ? $"@p{objCounter}3" : $"@p{objCounter}2", entity.Description));
                command.Parameters.Add(_parameterBuilderFunc(hasIdColumn ? $"@p{objCounter}4" : $"@p{objCounter}3", entity.Designation));
                
                queryBuilder.Append(values);
                queryBuilder.Append(")");
                appendComma = true;
                objCounter++;
            }

            command.CommandText = queryBuilder.ToString();
        }


        public override string BuildUpdateSqlQuery(PhysicalValueEntity entity)
        {
            return $"UPDATE {GetTableNameWithScheme()} SET name='{entity.Name}', description='{entity.Description}', designation='{entity.Designation}' WHERE id={entity.Id};";
        }
        
        public override void BuildUpdateCommandQueryAndParams(DbCommand command, PhysicalValueEntity entity)
        {
            string queryTemplate = "UPDATE {0} SET name={1}, description={2}, designation={3} WHERE id={4};";
            string query = string.Format(queryTemplate, GetTableNameWithScheme(), "@p1", "@p2", "@p3", "@p4");
            command.CommandText = query;
            command.Parameters.Add(_parameterBuilderFunc("@p4", entity.Id));
            command.Parameters.Add(_parameterBuilderFunc("@p1", entity.Name));
            command.Parameters.Add(_parameterBuilderFunc("@p2", entity.Description));
            command.Parameters.Add(_parameterBuilderFunc("@p3", entity.Designation));
        }
        
        public override void BuildBulkUpdateCommandQueryAndParams(DbCommand command, IList<PhysicalValueEntity> entities)
        {
            StringBuilder queryBuilder = new StringBuilder();
            string queryTemplate = "UPDATE {0} SET name={1}, description={2}, designation={3} WHERE id={4};";
            int objCounter = 1;
            foreach (PhysicalValueEntity entity in entities)
            {
                queryBuilder.Append(string.Format(queryTemplate, GetTableNameWithScheme(), $"@p{objCounter}1",
                    $"@p{objCounter}2", $"@p{objCounter}3", $"@p{objCounter}4"));
                
                command.Parameters.Add(_parameterBuilderFunc($"@p{objCounter}4", entity.Id));
                command.Parameters.Add(_parameterBuilderFunc($"@p{objCounter}1", entity.Name));
                command.Parameters.Add(_parameterBuilderFunc($"@p{objCounter}2", entity.Description));
                command.Parameters.Add(_parameterBuilderFunc($"@p{objCounter}3", entity.Designation));
                objCounter++;
            }

            command.CommandText = queryBuilder.ToString();
        }
        
        /*public string BuildDeleteQuery(IList<WhereParameter> whereClause)
        {
            string whereStatement = StatementsGenerator.BuildWhereStatement(whereClause);
            return $"DELETE FROM {GetTableNameWithScheme()} {whereStatement}";
        }*/
        

        private const string ModelName = "PhysicalValue";
        private const string TableName = "physical_values";
        //public static IList<string> FullColumnsList = new List<string>(){"id", "name", "designation", "description"};

        //private readonly string _schema;
        private readonly EntityConfig _config;
        private readonly Action<DbCommand, IList<WhereParameter>> _commandParametersHandler;
        private readonly Func<string, object, DbParameter> _parameterBuilderFunc;
    }
}