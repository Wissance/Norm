namespace Wissance.nOrm.TestModel.IndustrialMeasure.Entity
{
    public class ParameterEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Aliases { get; set; }
        public string Description { get; set; }
        public int MeasureUnitId { get; set; }
        public int UpdateFrequency { get; set; }
    }
}