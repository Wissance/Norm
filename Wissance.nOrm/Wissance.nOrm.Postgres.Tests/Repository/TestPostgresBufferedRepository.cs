using DbTools.Core;
using Microsoft.Extensions.Logging.Abstractions;
using Wissance.nOrm.Common.Tests;
using Wissance.nOrm.Postgres.Repository;
using Wissance.nOrm.Postgres.Tests.TestData.Expected;
using Wissance.nOrm.Repository;
using Wissance.nOrm.Sql;
using Wissance.nOrm.TestModel.IndustrialMeasure;
using Wissance.nOrm.TestModel.IndustrialMeasure.Builders;
using Wissance.nOrm.TestModel.IndustrialMeasure.Checkers;
using Wissance.nOrm.TestModel.IndustrialMeasure.Entity;
using Wissance.nOrm.TestModel.IndustrialMeasure.Factories;

namespace Wissance.nOrm.Postgres.Tests.Repository
{
    public class TestPostgresBufferedRepository : DatabaseRelatedTestBase, IDisposable
    {
        public TestPostgresBufferedRepository()
            :base(DbEngine.PostgresSql, PostgresDefs.TestDbHost, PostgresDefs.TestDbUser, PostgresDefs.TestDbPassword)
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
            IDbRepository<PhysicalValueEntity> repo = new PostgresBufferedRepository<PhysicalValueEntity>(ConnectionString,
                BufferThreshold, new PhysicalValueQueryBuilder("public"), PhysicalValueFactory.Create, new NullLoggerFactory());
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
        [InlineData(5, 10, 1, 10)]
        [InlineData(2, 15, 2, 5)]
        public async Task TestGetManyPhysicalValuesWithIdFilerAsync(int lowerIdValue, int upperIdValue, int page, int size)
        {
            IDbRepository<PhysicalValueEntity> repo = new PostgresBufferedRepository<PhysicalValueEntity>(ConnectionString,
                BufferThreshold, new PhysicalValueQueryBuilder(), PhysicalValueFactory.Create, new NullLoggerFactory());
            IList<PhysicalValueEntity> actual = await repo.GetManyAsync(page, size, new List<WhereParameter>()
            {
                new WhereParameter("id", null, false, WhereComparison.Greater, 
                    new List<object>(){lowerIdValue}, false),
                new WhereParameter("id", WhereJoinCondition.And, false, WhereComparison.Less, 
                    new List<object>(){upperIdValue}, false)
            }, null);
            Assert.NotNull(actual);
            IList<PhysicalValueEntity> expected = ExpectedPhysicalValues.Values.Where(v => v.Id > lowerIdValue && v.Id < upperIdValue).ToList();
            
            expected = expected.Skip(page > 1 ? (page - 1) * size : 0).Take(size).ToList();
            PhysicalValueChecker.Check(expected, actual);
            repo.Dispose();
        }

        private const string CreateScript = @"../../../../Wissance.nOrm.TestModel/IndustrialMeasure/TestData/postgres_test_db_structure.sql";
        private const string InsertDataScript = @"../../../../Wissance.nOrm.TestModel/IndustrialMeasure/TestData/postgres_test_db_data.sql";
        private const int BufferThreshold = 100;
    }
}