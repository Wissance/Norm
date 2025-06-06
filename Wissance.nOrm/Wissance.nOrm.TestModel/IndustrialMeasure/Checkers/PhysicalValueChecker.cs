using Wissance.nOrm.TestModel.IndustrialMeasure.Entity;
using Xunit;

namespace Wissance.nOrm.TestModel.IndustrialMeasure.Checkers
{
    public static class PhysicalValueChecker
    {
        public static void Check(PhysicalValueEntity expected, PhysicalValueEntity actual)
        {
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.Designation, actual.Designation);
        }

        public static void Check(IList<PhysicalValueEntity> expected, IList<PhysicalValueEntity> actual)
        {
            Assert.Equal(expected.Count, actual.Count);
            
            foreach (PhysicalValueEntity e in expected)
            {
                PhysicalValueEntity a = actual.FirstOrDefault(i => i.Id == e.Id);
                Assert.NotNull(a);
                Check(e, a);
            }
        }
    }
}