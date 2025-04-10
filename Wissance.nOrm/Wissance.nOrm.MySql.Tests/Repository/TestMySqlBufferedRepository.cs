using Microsoft.Extensions.Logging.Abstractions;
using Wissance.nOrm.MySql.Repository;
using Wissance.nOrm.Repository;
using Wissance.nOrm.Tests.Database.Entity;
using Wissance.nOrm.Tests.Database.Entity.Builders;
using Wissance.nOrm.Tests.Database.Entity.Factories;
using Wissance.nOrm.Tests.TestUtils;

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
        public async Task TestGetManyPhysicalValuesAsync(int? page, int? size, int expectedSize)
        {
            IDbRepository<PhysicalValueEntity> repo = new MySqlBufferedRepository<PhysicalValueEntity>(ConnectionString,
                new PhysicalValueQueryBuilder(), PhysicalValueFactory.Create, new NullLoggerFactory());
            IList<PhysicalValueEntity> actual = await repo.GetManyAsync(page, size, null, null);
            Assert.NotNull(actual);
            Assert.Equal(expectedSize, actual.Count);
        }
        
        [Fact]
        public async Task TestGetOneAsync()
        {
        }

        private const string CreateScript = @"../../../TestData/test_db_structure.sql";
        private const string InsertDataScript = @"../../../TestData/test_db_data.sql";
    }
}