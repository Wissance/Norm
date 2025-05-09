using DbTools.Core;
using Microsoft.Extensions.Logging.Abstractions;
using Wissance.nOrm.Common.Tests;
using Wissance.nOrm.Repository;
using Wissance.nOrm.Settings;
using Wissance.nOrm.Sql;
using Wissance.nOrm.Sqlite.Repository;
using Wissance.nOrm.Sqlite.Tests.TestData.Expected;
using Wissance.nOrm.TestModel.IndustrialMeasure.Builders;
using Wissance.nOrm.TestModel.IndustrialMeasure.Checkers;
using Wissance.nOrm.TestModel.IndustrialMeasure.Entity;
using Wissance.nOrm.TestModel.IndustrialMeasure.Factories;

namespace Wissance.nOrm.Sqlite.Tests.Repository
{
    public class TestSqLiteBufferedRepository : DatabaseRelatedTestBase, IDisposable
    {
        public TestSqLiteBufferedRepository()
            :base(DbEngine.SqLite, "", "", "")
        {
            PrepareDbAndData(CreateScript, InsertDataScript);
            _dbRepositorySettings = new DbRepositorySettings()
            {
                BufferThreshold = 100,
                CommandTimeout = 120,
                BufferSynchronizationDelayTimeout = 100,
                ForceSynchronizationBufferDelay = 500
            };
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
            IDbRepository<PhysicalValueEntity> repo = new SqLiteBufferedRepository<PhysicalValueEntity>(ConnectionString,
                _dbRepositorySettings, new PhysicalValueQueryBuilder(""), PhysicalValueFactory.Create, new NullLoggerFactory());
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
            IDbRepository<PhysicalValueEntity> repo = new SqLiteBufferedRepository<PhysicalValueEntity>(ConnectionString,
                _dbRepositorySettings, new PhysicalValueQueryBuilder(), PhysicalValueFactory.Create, new NullLoggerFactory());
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
        
        private const string CreateScript = @"../../../../Wissance.nOrm.TestModel/IndustrialMeasure/TestData/sqlite_test_db_structure.sql";
        private const string InsertDataScript = @"../../../../Wissance.nOrm.TestModel/IndustrialMeasure/TestData/sqlite_test_db_data.sql";
        
        private readonly DbRepositorySettings _dbRepositorySettings;
    }
}