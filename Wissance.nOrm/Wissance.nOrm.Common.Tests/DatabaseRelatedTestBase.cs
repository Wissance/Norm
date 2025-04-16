using DbTools.Core;
using DbTools.Core.Managers;
using DbTools.Simple.Extensions;
using DbTools.Simple.Factories;
using Microsoft.Extensions.Logging;

namespace Wissance.nOrm.Common.Tests
{
    public class DatabaseRelatedTestBase
    {
        public DatabaseRelatedTestBase(DbEngine dbEngine, string host, string user, string password)
        {
            _dbEngine = dbEngine;
            _host = host;
            _user = user;
            _password = password;
        }

        public void PrepareDbAndData(string createScript, string insertScript)
        {
            IDbManager dbManager = DbManagerFactory.Create(_dbEngine, new LoggerFactory());

            string dbNameTemplate = "nORM_Test_DB_{0}";
            Guid dbId = Guid.NewGuid();
            string dbName = string.Format(dbNameTemplate, dbId.ToString().Replace("-", ""));

            ConnectionString = dbManager.Create(_dbEngine, _host, dbName, false, _user, _password,
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
            IDbManager dbManager = DbManagerFactory.Create(_dbEngine, new LoggerFactory());
            dbManager.DropDatabase(ConnectionString);
        }
        
        protected string ConnectionString { get; set; }

        private readonly DbEngine _dbEngine;
        private readonly string _host;
        private readonly string _user;
        private readonly string _password;
    }
}