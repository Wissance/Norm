using DbTools.Core;
using Microsoft.Extensions.Logging.Abstractions;
using Wissance.nOrm.Common.Tests;
using Wissance.nOrm.Repository;
using Wissance.nOrm.SqlServer.Repository;
using Wissance.nOrm.SqlServer.Tests.Builders;
using Wissance.nOrm.SqlServer.Tests.TestData.Expected;
using Wissance.nOrm.TestModel.IndustrialMeasure.Builders;
using Wissance.nOrm.TestModel.IndustrialMeasure.Checkers;
using Wissance.nOrm.TestModel.IndustrialMeasure.Entity;
using Wissance.nOrm.TestModel.IndustrialMeasure.Factories;

namespace Wissance.nOrm.SqlServer.Tests.Repository
{
    public class TestSqlServerBufferedRepository : DatabaseRelatedTestBase, IDisposable
    {
        public TestSqlServerBufferedRepository()
            :base(DbEngine.SqlServer, SqlServerDefs.TestDbHost, SqlServerDefs.TestDbUser, SqlServerDefs.TestDbPassword)
        {
            PrepareDbAndData(CreateScript, InsertDataScript);
        }
        
        public void Dispose()
        {
            DestroyDb();
        }
        
        [Theory]
        [InlineData(null, null, 15)]
        [InlineData(0, 10, 10)]
        [InlineData(1, 10, 10)]
        [InlineData(2, 10, 5)]
        public async Task TestGetManyPhysicalValuesWithFullColumnListAsync(int? page, int? size, int expectedSize)
        {
            IDbRepository<PhysicalValueEntity> repo = new SqlServerBufferedRepository<PhysicalValueEntity>(ConnectionString,
                BufferThreshold, new PhysicalValueSqlServerSpecificQueryBuilder("dbo"), PhysicalValueFactory.Create, new NullLoggerFactory());
            IList<PhysicalValueEntity> actual = await repo.GetManyAsync(page, size, null, null);
            Assert.NotNull(actual);
            IList<PhysicalValueEntity> expected = ExpectedPhysicalValues.Values;
            if (page != null && size != null)
            {
                expected = expected.Skip(page.Value > 1 ? (page.Value - 1) * size.Value : 0).Take(size.Value).ToList();
            }
            PhysicalValueChecker.Check(expected, actual);
            repo.Dispose();
        }
        
        private const string CreateScript = @"../../../../Wissance.nOrm.TestModel/IndustrialMeasure/TestData/sqlserver_test_db_structure.sql";
        private const string InsertDataScript = @"../../../../Wissance.nOrm.TestModel/IndustrialMeasure/TestData/sqlserver_test_db_data.sql";
        private const int BufferThreshold = 100;
    }
}