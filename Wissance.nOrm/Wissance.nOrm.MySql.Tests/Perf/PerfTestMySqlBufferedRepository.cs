using System.Diagnostics;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Logging.Abstractions;
using Wissance.nOrm.MySql.Repository;
using Wissance.nOrm.Repository;
using Wissance.nOrm.TestModel.IndustrialMeasure;
using Wissance.nOrm.Tests.Database.Entity.Builders;
using Wissance.nOrm.Tests.Database.Entity.Factories;
using Wissance.nOrm.Tests.TestUtils;
using Xunit.Abstractions;

namespace Wissance.nOrm.Tests.Perf
{
    public class PerfTestMySqlBufferedRepository : MySqlRelatedTestBase, IDisposable
    {
        public PerfTestMySqlBufferedRepository(ITestOutputHelper outputCollector)
        {
            PrepareDbAndData(CreateScript, InsertDataScript);
            _outputCollector = outputCollector;
            _logger = new AccumulationLogger();

            _config = ManualConfig.Create(DefaultConfig.Instance)
                .AddLogger(_logger)
                .WithOptions(ConfigOptions.DisableOptimizationsValidator);
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
            _logger.WriteLine($"Bulk insert for {numberOfSamples} rows time is : {elapsedMs} ms");
            _outputCollector.WriteLine(_logger.GetLog());
            Assert.Equal(numberOfSamples, result);
        }
        
        private const string CreateScript = @"../../../TestData/test_db_structure.sql";
        private const string InsertDataScript = @"../../../TestData/test_db_data.sql";
        
        private readonly ITestOutputHelper _outputCollector;
        private readonly AccumulationLogger _logger;
        private readonly ManualConfig _config;
    }
}