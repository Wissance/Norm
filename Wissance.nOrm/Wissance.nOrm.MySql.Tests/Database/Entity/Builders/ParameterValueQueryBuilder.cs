using Wissance.nOrm.Entity.QueryBuilders;
using Wissance.nOrm.TestModel.IndustrialMeasure;

namespace Wissance.nOrm.Tests.Database.Entity.Builders
{
    internal class ParameterValueQueryBuilder : IDbEntityQueryBuilder<ParameterValueEntity>
    {
        public string BuildSelectManyQuery(int? page, int? size, IDictionary<string, object> whereClause = null, IList<string> columns = null)
        {
            throw new NotImplementedException();
        }

        public string BuildSelectOneQuery(IDictionary<string, object> whereClause = null, IList<string> columns = null)
        {
            throw new NotImplementedException();
        }

        public string BuildInsertSqlQuery(ParameterValueEntity entity)
        {
            throw new NotImplementedException();
        }

        public string BuildBulkInsertSqlQuery(IList<ParameterValueEntity> entities)
        {
            throw new NotImplementedException();
        }

        public string BuildUpdateSqlQuery(ParameterValueEntity entity)
        {
            throw new NotImplementedException();
        }
        
        public string BuildDeleteQuery(IDictionary<string, object> whereClause)
        {
            throw new NotImplementedException();
        }

        public string GetTableSchema()
        {
            throw new NotImplementedException();
        }

        public string GetTableName()
        {
            throw new NotImplementedException();
        }

        public string GetModelType()
        {
            throw new NotImplementedException();
        }
    }
}