using DbTools.Core;
using Wissance.nOrm.Common.Tests;

namespace Wissance.nOrm.Sqlite.Tests.Repository
{
    public class TestSqLiteBufferedRepository : DatabaseRelatedTestBase, IDisposable
    {
        public TestSqLiteBufferedRepository()
            :base(DbEngine.SqLite, "", "", "")
        {
            PrepareDbAndData(CreateScript, InsertDataScript);
        }
        
        public void Dispose()
        {
            DestroyDb();
        }
        
        private const string CreateScript = @"../../../../Wissance.nOrm.TestModel/IndustrialMeasure/TestData/sqlite_test_db_structure.sql";
        private const string InsertDataScript = @"../../../../Wissance.nOrm.TestModel/IndustrialMeasure/TestData/sqlite_test_db_data.sql";
        private const int BufferThreshold = 100;
    }
}