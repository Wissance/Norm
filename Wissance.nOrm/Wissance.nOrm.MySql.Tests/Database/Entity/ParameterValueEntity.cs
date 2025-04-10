namespace Wissance.nOrm.Tests.Database.Entity
{
    internal class ParameterValueEntity
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTimeOffset Time { get; set; }
        public int ParameterId { get; set; }
    }
}