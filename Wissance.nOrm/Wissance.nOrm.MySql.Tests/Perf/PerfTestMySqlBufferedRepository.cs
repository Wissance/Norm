using System.Diagnostics;
using DbTools.Core;
using Microsoft.Extensions.Logging.Abstractions;
using Wissance.nOrm.Common.Tests;
using Wissance.nOrm.MySql.Repository;
using Wissance.nOrm.Repository;
using Wissance.nOrm.Sql;
using Wissance.nOrm.TestModel.IndustrialMeasure;
using Wissance.nOrm.TestModel.IndustrialMeasure.Builders;
using Wissance.nOrm.TestModel.IndustrialMeasure.Entity;
using Wissance.nOrm.TestModel.IndustrialMeasure.Factories;
using Xunit.Abstractions;

namespace Wissance.nOrm.MySql.Tests.Perf
{
    public class PerfTestMySqlBufferedRepository : DatabaseRelatedTestBase, IDisposable
    {
        public PerfTestMySqlBufferedRepository(ITestOutputHelper outputCollector)
            :base(DbEngine.MySql, MySqlDefs.TestDbHost, MySqlDefs.TestDbUser, MySqlDefs.TestDbPassword)
        {
            PrepareDbAndData(CreateScript, InsertDataScript);
            _outputCollector = outputCollector;
        }
        
        public void Dispose()
        {
            DestroyDb();
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(10000)]
        [InlineData(100000)]
        public async Task PerfTestBulkInsertParametersValuesImmediately(int numberOfSamples)
        {
            IDbRepository<ParameterValueEntity> repo = new MySqlBufferedRepository<ParameterValueEntity>(ConnectionString,
                100, new ParameterValueQueryBuilder(), ParameterValueFactory.Create, new NullLoggerFactory());
            IList<ParameterValueEntity> values = new List<ParameterValueEntity>();
            DateTimeOffset time = DateTimeOffset.Now.AddMonths(-3);
            Random rnd = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < numberOfSamples; i++)
            {
                int randomValue = rnd.Next(50, 60);
                values.Add(new ParameterValueEntity()
                {
                    ParameterId = 10,
                    Time = time,
                    Value = randomValue.ToString()
                });
                time = time.AddMinutes(5);
            }

            ParameterValueBenchmarks benchmarks = new ParameterValueBenchmarks();
            Stopwatch watch = Stopwatch.StartNew();
            int result = await benchmarks.RunBulkInsertBenchmark(repo, values);
            watch.Stop();
            long elapsedMs = watch.ElapsedMilliseconds;
            _outputCollector.WriteLine($"Bulk insert for {numberOfSamples} rows time is : {elapsedMs} ms");
            Assert.Equal(numberOfSamples, result);
        }

        [Theory]
        [InlineData(100000, null, null)]
        [InlineData(100000, 1, 10000)]
        [InlineData(100000, 7, 10000)]
        [InlineData(1000000, 1, 100000)]
        [InlineData(1000000, 1, 500000)]
        [InlineData(1000000, 10, 20000)]
        public async Task PerfTestReadManyParametersValues(int numberOfSamples, int? selectingPage, int? selectingPageSize)
        {
            IDbRepository<ParameterValueEntity> repo = new MySqlBufferedRepository<ParameterValueEntity>(ConnectionString,
                100, new ParameterValueQueryBuilder(), ParameterValueFactory.Create, new NullLoggerFactory());
            IList<ParameterValueEntity> values = new List<ParameterValueEntity>();
            DateTimeOffset time = DateTimeOffset.Now.AddMonths(-3);
            Random rnd = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < numberOfSamples; i++)
            {
                int randomValue = rnd.Next(50, 60);
                values.Add(new ParameterValueEntity()
                {
                    ParameterId = 10,
                    Time = time,
                    Value = randomValue.ToString()
                });
                time = time.AddMinutes(5);
            }

            int result = await repo.BulkInsertAsync(values, true);
            Assert.Equal(numberOfSamples, result);
            
            ParameterValueBenchmarks benchmarks = new ParameterValueBenchmarks();
            Stopwatch watch = Stopwatch.StartNew();
            IList<ParameterValueEntity> readingPage = await benchmarks.RunGetManyAsync(repo, selectingPage, selectingPageSize,
                new List<WhereParameter>() { });
            watch.Stop();
            long elapsedMs = watch.ElapsedMilliseconds;
            int actualRowsRead = selectingPageSize ?? numberOfSamples;
            _outputCollector.WriteLine($"Read {actualRowsRead} rows from database containing {numberOfSamples} rows, time is : {elapsedMs} ms");
        }

        private const string CreateScript = @"../../../../Wissance.nOrm.TestModel/IndustrialMeasure/TestData/mysql_test_db_structure.sql";
        private const string InsertDataScript = @"../../../../Wissance.nOrm.TestModel/IndustrialMeasure/TestData/mysql_test_db_data.sql";
        
        private readonly ITestOutputHelper _outputCollector;
    }
}