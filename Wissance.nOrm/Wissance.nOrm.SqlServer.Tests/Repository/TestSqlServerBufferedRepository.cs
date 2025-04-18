using DbTools.Core;
using Wissance.nOrm.Common.Tests;

namespace Wissance.nOrm.SqlServer.Tests.Repository
{
    public class TestSqlServerBufferedRepository : DatabaseRelatedTestBase, IDisposable
    {
        public TestSqlServerBufferedRepository()
            :base(DbEngine.SqlServer, SqlServerDefs.TestDbHost, SqlServerDefs.TestDbUser, SqlServerDefs.TestDbPassword)
        {
            PrepareDbAndData(CreateScript, InsertDataScript);
        }
        
        public void Dispose()
        {
            DestroyDb();
        }
        
        private const string CreateScript = @"../../../../Wissance.nOrm.TestModel/IndustrialMeasure/TestData/sqlserver_test_db_structure.sql";
        private const string InsertDataScript = @"../../../../Wissance.nOrm.TestModel/IndustrialMeasure/TestData/sqlserver_test_db_data.sql";
        private const int BufferThreshold = 100;
    }
}