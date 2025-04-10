using Wissance.nOrm.Entity.QueryBuilders;

namespace Wissance.nOrm.Tests.Database.Entity.Builders
{
    internal class PhysicalValueQueryBuilder : IDbEntityQueryBuilder<PhysicalValueEntity>
    {
        public string BuildSelectManyQuery(int? page, int? size, IDictionary<string, object> whereClause, IList<string> columns)
        {
            throw new NotImplementedException();
        }

        public string BuildSelectOneQuery(IDictionary<string, object> whereClause = null, IList<string> columns = null)
        {
            throw new NotImplementedException();
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