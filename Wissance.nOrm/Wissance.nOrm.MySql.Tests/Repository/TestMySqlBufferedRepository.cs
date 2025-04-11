using Microsoft.Extensions.Logging.Abstractions;
using Wissance.nOrm.MySql.Repository;
using Wissance.nOrm.Repository;
using Wissance.nOrm.Tests.Database.Entity;
using Wissance.nOrm.Tests.Database.Entity.Builders;
using Wissance.nOrm.Tests.Database.Entity.Factories;
using Wissance.nOrm.Tests.TestData.Expected;
using Wissance.nOrm.Tests.TestUtils;
using Wissance.nOrm.Tests.TestUtils.Checkers;

namespace Wissance.nOrm.Tests.Repository
{
    public class TestMySqlBufferedRepository : MySqlRelatedTestBase, IDisposable
    {
        public TestMySqlBufferedRepository()
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
            IDbRepository<PhysicalValueEntity> repo = new MySqlBufferedRepository<PhysicalValueEntity>(ConnectionString,
                new PhysicalValueQueryBuilder(), PhysicalValueFactory.Create, new NullLoggerFactory());
            IList<PhysicalValueEntity> actual = await repo.GetManyAsync(page, size, null, null);
            Assert.NotNull(actual);
            IList<PhysicalValueEntity> expected = ExpectedPhysicalValues.Values;
            if (page != null && size != null)
            {
                expected = expected.Skip(page.Value > 1 ? (page.Value - 1) * size.Value : 0).Take(size.Value).ToList();
            }
            PhysicalValueChecker.Check(expected, actual);
        }
        
        [Fact]
        public async Task TestGetOneWithFullColumnListAsync()
        {
        }

        private const string CreateScript = @"../../../TestData/test_db_structure.sql";
        private const string InsertDataScript = @"../../../TestData/test_db_data.sql";
    }
}