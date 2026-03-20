using System.Data.Common;
using System.Text;
using Wissance.nOrm.Entity.Config;
using Wissance.nOrm.Entity.QueryBuilders;
using Wissance.nOrm.Sql;
using Wissance.nOrm.TestModel.IndustrialMeasure.Entity;

namespace Wissance.nOrm.TestModel.IndustrialMeasure.Builders
{
    public class PhysicalValueQueryBuilder : SingleTableSqlQueryBuilder<PhysicalValueEntity>
    {
        public PhysicalValueQueryBuilder(EntityConfig config, Action<DbCommand, IList<WhereParameter>> commandParametersHandler,
            Func<string, object, DbParameter> parameterBuilderFunc)
            :base(config, commandParametersHandler, parameterBuilderFunc)
        {
            _parameterBuilderFunc = parameterBuilderFunc;
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

        public override string BuildUpdateSqlQuery(PhysicalValueEntity entity)
        {
            return $"UPDATE {GetTableNameWithScheme()} SET name='{entity.Name}', description='{entity.Description}', designation='{entity.Designation}' WHERE id={entity.Id};";
        }
        
        private readonly Func<string, object, DbParameter> _parameterBuilderFunc;
    }
}