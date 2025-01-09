using Wissance.nOrm.Tests.TestUtils;

namespace Wissance.nOrm.Tests.Repository
{
    public class TestMySqlBufferedRepository : MySqlRelatedTestBase, IDisposable
    {
        public TestMySqlBufferedRepository()
        {
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        
        [Fact]
        public async Task TestGetManyAsync()
        {
        }
        
        [Fact]
        public async Task TestGetOneAsync()
        {
        }
    }
}