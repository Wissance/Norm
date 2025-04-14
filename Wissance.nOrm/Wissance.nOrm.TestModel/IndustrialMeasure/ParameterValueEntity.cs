namespace Wissance.nOrm.TestModel.IndustrialMeasure
{
    public class ParameterValueEntity
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public DateTimeOffset Time { get; set; }
        public int ParameterId { get; set; }
    }
}