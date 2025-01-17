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
        
        [Fact]
        public async Task TestGetManyAsync()
        {
        }
        
        [Fact]
        public async Task TestGetOneAsync()
        {
        }

        private const string CreateScript = @"../../../TestData/test_db_structure.sql";
        private const string InsertDataScript = @"../../../TestData/test_db_data.sql";
    }
}