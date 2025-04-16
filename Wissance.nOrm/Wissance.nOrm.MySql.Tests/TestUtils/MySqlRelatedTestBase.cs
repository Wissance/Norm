using DbTools.Core;
using DbTools.Core.Managers;
using DbTools.Simple.Extensions;
using DbTools.Simple.Factories;
using Microsoft.Extensions.Logging;

namespace Wissance.nOrm.MySql.Tests.TestUtils
{

    public class MySqlRelatedTestBase
    {
        public void PrepareDbAndData(string createScript, string insertScript)
        {
            IDbManager dbManager = DbManagerFactory.Create(DbEngine.MySql, new LoggerFactory());

            string dbNameTemplate = "nORM_Test_DB_{0}";
            Guid dbId = Guid.NewGuid();
            string dbName = string.Format(dbNameTemplate, dbId.ToString().Replace("-", ""));

            ConnectionString = dbManager.Create(DbEngine.MySql, TestDbHost, dbName, false, TestDbUser, TestDbPassword,
                new List<string>());
            CreateTestSchema(dbManager, createScript);
            InsertTestData(dbManager, insertScript);
        }

        private void CreateTestSchema(IDbManager dbManager, string createScript)
        {
            string scriptContent = File.ReadAllText(Path.GetFullPath(createScript));
            dbManager.ExecuteNonQuery(ConnectionString, scriptContent);
        }

        private void InsertTestData(IDbManager dbManager, string dataScript)
        {
            string scriptContent = File.ReadAllText(Path.GetFullPath(dataScript));
            dbManager.ExecuteNonQuery(ConnectionString, scriptContent);
        }

        public void DestroyDb()
        {
            IDbManager dbManager = DbManagerFactory.Create(DbEngine.MySql, new LoggerFactory());
            dbManager.DropDatabase(ConnectionString);
        }
        
        protected string ConnectionString { get; set; }

        private const string TestDbHost = "localhost";
        private const string TestDbUser = "developer";
        private const string TestDbPassword = "123";
    }
}