namespace Wissance.nOrm.Tests.Database.Entity
{
    internal class MeasureUnitEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Description { get; set; }
        public int PhysicalValueId { get; set; }
    }
}