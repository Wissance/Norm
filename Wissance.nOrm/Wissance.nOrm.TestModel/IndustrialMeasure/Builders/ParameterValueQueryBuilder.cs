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
        public ParameterValueQueryBuilder(EntityConfig config, Action<DbCommand, IList<WhereParameter>> commandParametersHandler)
            :base(config, commandParametersHandler)
        {

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
        
        /*public override string BuildDeleteQuery(IList<WhereParameter> whereClause)
        {
            throw new NotImplementedException();
        }*/
    }
}