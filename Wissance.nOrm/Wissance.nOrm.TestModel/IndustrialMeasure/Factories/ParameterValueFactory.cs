using Wissance.nOrm.TestModel.IndustrialMeasure.Entity;

namespace Wissance.nOrm.TestModel.IndustrialMeasure.Factories
{
    public static class ParameterValueFactory
    {
        public static ParameterValueEntity Create(IList<object> columnsValues, IList<string> columns)
        {
            ParameterValueEntity entity = new ParameterValueEntity();
            entity.Id = Convert.ToInt64(columnsValues[0]);
            entity.ParameterId = Convert.ToInt32(columnsValues[1].ToString());
            entity.Time = new DateTimeOffset((DateTime)columnsValues[2]);
            entity.Value = columnsValues[3].ToString();
            return entity;
        }
    }
}