using DbTools.Core;
using Microsoft.Extensions.Logging.Abstractions;
using Wissance.nOrm.Common.Tests;
using Wissance.nOrm.MySql.Repository;
using Wissance.nOrm.MySql.Tests.TestData.Expected;
using Wissance.nOrm.Repository;
using Wissance.nOrm.TestModel.IndustrialMeasure;
using Wissance.nOrm.TestModel.IndustrialMeasure.Builders;
using Wissance.nOrm.TestModel.IndustrialMeasure.Checkers;
using Wissance.nOrm.TestModel.IndustrialMeasure.Entity;
using Wissance.nOrm.TestModel.IndustrialMeasure.Factories;

namespace Wissance.nOrm.MySql.Tests.Repository
{
    public class TestMySqlBufferedRepository : DatabaseRelatedTestBase, IDisposable
    {
        public TestMySqlBufferedRepository()
            :base(DbEngine.MySql, MySqlDefs.TestDbHost, MySqlDefs.TestDbUser, MySqlDefs.TestDbPassword)
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
                BufferThreshold, new PhysicalValueQueryBuilder(), PhysicalValueFactory.Create, new NullLoggerFactory());
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
        
        [Theory]
        [InlineData(2)]
        public async Task TestGetOneWithFullColumnListAsync(int id)
        {
            IDbRepository<PhysicalValueEntity> repo = new MySqlBufferedRepository<PhysicalValueEntity>(ConnectionString,
                BufferThreshold, new PhysicalValueQueryBuilder(), PhysicalValueFactory.Create, new NullLoggerFactory());
            PhysicalValueEntity actual = await repo.GetOneAsync(new Dictionary<string, object>() {{"id", id}});
            PhysicalValueEntity expected = ExpectedPhysicalValues.Values.First(v => v.Id == id);
            PhysicalValueChecker.Check(expected, actual);
            repo.Dispose();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(20)]
        public async Task TestInsertPhysicalValueImmediately(int id)
        {
            IDbRepository<PhysicalValueEntity> repo = new MySqlBufferedRepository<PhysicalValueEntity>(ConnectionString,
                BufferThreshold, new PhysicalValueQueryBuilder(), PhysicalValueFactory.Create, new NullLoggerFactory());
            PhysicalValueEntity entity = new PhysicalValueEntity()
            {
                Id = id,
                Name = "new phys value",
                Description = "new phys value",
                Designation = "NPV"
            };
            bool result = await repo.InsertAsync(entity, true);
            Assert.True(result);
            PhysicalValueEntity actual = await repo.GetOneAsync(new Dictionary<string, object>() {{"name", entity.Name}});
            if (id <= 0)
                entity.Id = actual.Id;
            PhysicalValueChecker.Check(entity, actual);
            repo.Dispose();
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(20)]
        public async Task TestInsertPhysicalValueInBackground(int id)
        {
            IDbRepository<PhysicalValueEntity> repo = new MySqlBufferedRepository<PhysicalValueEntity>(ConnectionString,
                1, new PhysicalValueQueryBuilder(), PhysicalValueFactory.Create, new NullLoggerFactory());
            PhysicalValueEntity entity = new PhysicalValueEntity()
            {
                Id = id,
                Name = "new phys value",
                Description = "new phys value",
                Designation = "NPV"
            };
            bool result = await repo.InsertAsync(entity, false);
            Assert.True(result);
            Thread.Sleep(2000);
            PhysicalValueEntity actual = await repo.GetOneAsync(new Dictionary<string, object>() {{"name", entity.Name}});
            if (id <= 0)
                entity.Id = actual.Id;
            PhysicalValueChecker.Check(entity, actual);
            repo.Dispose();
        }

        [Fact]
        public async Task TestBulkInsertPhysicalValuesImmediately()
        {
            IDbRepository<PhysicalValueEntity> repo = new MySqlBufferedRepository<PhysicalValueEntity>(ConnectionString,
                1, new PhysicalValueQueryBuilder(), PhysicalValueFactory.Create, new NullLoggerFactory());
            IList<PhysicalValueEntity> newPhysValues = new List<PhysicalValueEntity>()
            {
                new PhysicalValueEntity()
                {
                    Id = 30,
                    Name = "new phys value",
                    Description = "new phys value",
                    Designation = "NPV"
                },
                new PhysicalValueEntity()
                {
                    Id = 31,
                    Name = "new phys value2",
                    Description = "new phys value2",
                    Designation = "NPV2"
                },
                new PhysicalValueEntity()
                {
                    Id = 32,
                    Name = "new phys value3",
                    Description = "new phys value3",
                    Designation = "NPV3"
                }
            };
            int result = await repo.BulkInsertAsync(newPhysValues, true);
            Assert.Equal(newPhysValues.Count, result);
            repo.Dispose();
        }

        [Fact]
        public async Task TestUpdatePhysicalValueImmediately()
        {
            IDbRepository<PhysicalValueEntity> repo = new MySqlBufferedRepository<PhysicalValueEntity>(ConnectionString,
                1, new PhysicalValueQueryBuilder(), PhysicalValueFactory.Create, new NullLoggerFactory());
            PhysicalValueEntity newPhysValue = new PhysicalValueEntity()
            {
                Id = 30,
                Name = "new phys value",
                Description = "new phys value",
                Designation = "NPV"
            };

            bool result = await repo.InsertAsync(newPhysValue, true);
            Assert.True(result);
            newPhysValue.Name = "new new phys value";
            result = await repo.UpdateAsync(newPhysValue, true);
            Assert.True(result);
            PhysicalValueEntity actual = await repo.GetOneAsync(new Dictionary<string, object>() {{"id", newPhysValue.Id}});
            PhysicalValueChecker.Check(newPhysValue, actual);
            repo.Dispose();
        }

        [Fact]
        public async Task TestBulkUpdatePhysicalValuesImmediately()
        {
            IDbRepository<PhysicalValueEntity> repo = new MySqlBufferedRepository<PhysicalValueEntity>(ConnectionString,
                1, new PhysicalValueQueryBuilder(), PhysicalValueFactory.Create, new NullLoggerFactory());
            IList<PhysicalValueEntity> newPhysValues = new List<PhysicalValueEntity>()
            {
                new PhysicalValueEntity()
                {
                    Id = 30,
                    Name = "new phys value",
                    Description = "new phys value",
                    Designation = "NPV"
                },
                new PhysicalValueEntity()
                {
                    Id = 31,
                    Name = "new phys value2",
                    Description = "new phys value2",
                    Designation = "NPV2"
                },
                new PhysicalValueEntity()
                {
                    Id = 32,
                    Name = "new phys value3",
                    Description = "new phys value3",
                    Designation = "NPV3"
                }
            };
            int result = await repo.BulkInsertAsync(newPhysValues, true);
            Assert.Equal(newPhysValues.Count, result);

            foreach (PhysicalValueEntity entity in newPhysValues)
            {
                entity.Name += DateTime.UtcNow.Ticks;
            }
            
            result = await repo.BulkUpdateAsync(newPhysValues, true);
            Assert.Equal(newPhysValues.Count, result);
            repo.Dispose();
        }

        [Fact]
        public async Task TestDeletePhysicalValuesImmediately()
        {
            IDbRepository<PhysicalValueEntity> repo = new MySqlBufferedRepository<PhysicalValueEntity>(ConnectionString,
                1, new PhysicalValueQueryBuilder(), PhysicalValueFactory.Create, new NullLoggerFactory());
            PhysicalValueEntity newPhysValue = new PhysicalValueEntity()
            {
                Id = 30,
                Name = "new phys value",
                Description = "new phys value",
                Designation = "NPV"
            };

            bool result = await repo.InsertAsync(newPhysValue, true);
            Assert.True(result);
            result = await repo.DeleteAsync(new Dictionary<string, object>() {{"id", newPhysValue.Id}});
            Assert.True(result);
            PhysicalValueEntity physVal = await repo.GetOneAsync(new Dictionary<string, object>() {{"id", newPhysValue.Id}});
            Assert.Null(physVal);
        }

        private const string CreateScript = @"../../../../Wissance.nOrm.TestModel/IndustrialMeasure/TestData/mysql_test_db_structure.sql";
        private const string InsertDataScript = @"../../../../Wissance.nOrm.TestModel/IndustrialMeasure/TestData/mysql_test_db_data.sql";
        private const int BufferThreshold = 100;
    }
}