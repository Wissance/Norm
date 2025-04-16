using Wissance.nOrm.TestModel.IndustrialMeasure;

namespace Wissance.nOrm.MySql.Tests.Database.Entity.Factories
{
    internal static class PhysicalValueFactory
    {
        public static PhysicalValueEntity Create(IList<object> columnsValues, IList<string> columns)
        {
            PhysicalValueEntity entity = new PhysicalValueEntity();
            // consider FULL column list
            // todo(UMV) : assign only props from columns
            /*IList<string> actualColumns = columns ?? PhysicalValueQueryBuilder.FullColumnsList;
            for (int i = 0; i < actualColumns.Count; i++)
            {
                
            }*/
            entity.Id = Convert.ToInt32(columnsValues[0]);
            entity.Name = columnsValues[1].ToString();
            entity.Designation = columnsValues[2].ToString();
            entity.Description = columnsValues[3].ToString();
            return entity;
        }
    }
}