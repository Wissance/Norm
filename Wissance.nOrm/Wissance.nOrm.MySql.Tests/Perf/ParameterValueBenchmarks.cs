using BenchmarkDotNet.Attributes;
using Wissance.nOrm.Repository;
using Wissance.nOrm.TestModel.IndustrialMeasure;

namespace Wissance.nOrm.Tests.Perf
{
    [MemoryDiagnoser]
    internal class ParameterValueBenchmarks
    {
        [Benchmark]
        public async Task<int> RunBulkInsertBenchmark(IDbRepository<ParameterValueEntity> repository, IList<ParameterValueEntity> entities)
        {
            return await repository.BulkInsertAsync(entities, true);
        }
    }
}