using System.Data.Common;
using System.Text;
using Wissance.nOrm.Entity.Config;
using Wissance.nOrm.Entity.QueryBuilders;
using Wissance.nOrm.Sql;
using Wissance.nOrm.TestModel.IndustrialMeasure.Entity;

namespace Wissance.nOrm.TestModel.IndustrialMeasure.Builders
{
    public class ParameterValueQueryBuilder : SingleTableSqlQueryBuilder<ParameterValueEntity>
    {
        public ParameterValueQueryBuilder(EntityConfig config, Action<DbCommand, IList<WhereParameter>> commandParametersHandler,
            Func<string, object, DbParameter> parameterBuilderFunc)
            :base(config, commandParametersHandler, parameterBuilderFunc)
        {
            _parameterBuilderFunc = parameterBuilderFunc;
        }

        public override string BuildInsertSqlQuery(ParameterValueEntity entity)
        {
            bool hasIdColumn = entity.Id > 0;
            string queryTemplate = "INSERT INTO {0} ({1} parameter_id, time, value) VALUES({2} {3}, '{4}', '{5}');";
            string idColumn = hasIdColumn ? "id," : "";
            string idValue = hasIdColumn ? $"{entity.Id}," : "";
            return string.Format(queryTemplate, GetTableNameWithScheme(), idColumn, idValue, entity.ParameterId, 
                entity.Time.UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss"), entity.Value);
        }

        public override void BuildInsertCommandQueryAndParams(DbCommand command, ParameterValueEntity entity)
        {
            bool hasIdColumn = entity.Id > 0;
            string queryTemplate = "INSERT INTO {0} ({1} parameter_id, time, value) VALUES({2} {3}, {4}, {5});";
            string idColumn = hasIdColumn ? "id," : "";
            string query = hasIdColumn
                         ? string.Format(queryTemplate, GetTableNameWithScheme(), idColumn, "@p1,", "@p2", "@p3", "@p4")
                         : string.Format(queryTemplate, GetTableNameWithScheme(), idColumn, "", "@p1", "@p2", "@p3");
            command.CommandText = query;
            if (hasIdColumn)
                command.Parameters.Add(_parameterBuilderFunc("@p1", entity.Id));
            command.Parameters.Add(_parameterBuilderFunc(hasIdColumn ? "@p2" : "@p1", entity.ParameterId));
            command.Parameters.Add(_parameterBuilderFunc(hasIdColumn ? "@p3" : "@p2", entity.Time));
            command.Parameters.Add(_parameterBuilderFunc(hasIdColumn ? "@p4" : "@p3", entity.Value));
        }

        public override string BuildBulkInsertSqlQuery(IList<ParameterValueEntity> entities)
        {
            bool hasIdColumn = entities[0].Id > 0;
            string columns = "parameter_id, time, value";
            if (hasIdColumn)
                columns = $"id, {columns}";
            StringBuilder queryBuilder = new StringBuilder($"INSERT INTO {GetTableNameWithScheme()} ({columns}) VALUES");
            bool appendComma = false;
            foreach (ParameterValueEntity entity in entities)
            {
                if (appendComma)
                    queryBuilder.Append(",\n");

                queryBuilder.Append("(");
                string values = $"{entity.ParameterId},'{entity.Time.UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss")}','{entity.Value}'";
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

        public override string BuildUpdateSqlQuery(ParameterValueEntity entity)
        {
            throw new NotImplementedException();
        }
        
        public override void BuildUpdateCommandQueryAndParams(DbCommand command, ParameterValueEntity entity)
        {
            string queryTemplate = "UPDATE {0} SET time={1}, value={2} WHERE id={3};";
            string query = string.Format(queryTemplate, GetTableNameWithScheme(), "@p1", "@p2", "@p3");
            command.CommandText = query;
            command.Parameters.Add(_parameterBuilderFunc("@p3", entity.Id));
            command.Parameters.Add(_parameterBuilderFunc("@p1", entity.Time));
            command.Parameters.Add(_parameterBuilderFunc("@p2", entity.Value));
        }

        private readonly Func<string, object, DbParameter> _parameterBuilderFunc;
    }
}