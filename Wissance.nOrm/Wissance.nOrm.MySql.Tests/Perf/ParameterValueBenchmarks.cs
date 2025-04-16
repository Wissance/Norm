using Wissance.nOrm.Repository;
using Wissance.nOrm.TestModel.IndustrialMeasure;
using Wissance.nOrm.TestModel.IndustrialMeasure.Entity;

namespace Wissance.nOrm.MySql.Tests.Perf
{
    internal class ParameterValueBenchmarks
    {
        public async Task<int> RunBulkInsertBenchmark(IDbRepository<ParameterValueEntity> repository, IList<ParameterValueEntity> entities)
        {
            return await repository.BulkInsertAsync(entities, true);
        }

        public async Task<IList<ParameterValueEntity>> RunGetManyAsync(IDbRepository<ParameterValueEntity> repository,
            int? page, int? size,
            IDictionary<string, object> whereParams)
        {
            return await repository.GetManyAsync(page, size, whereParams);
        }
    }
}