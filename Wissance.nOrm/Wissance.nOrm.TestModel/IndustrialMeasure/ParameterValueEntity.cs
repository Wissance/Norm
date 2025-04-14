namespace Wissance.nOrm.TestModel.IndustrialMeasure
{
    public class ParameterValueEntity
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTimeOffset Time { get; set; }
        public int ParameterId { get; set; }
    }
}