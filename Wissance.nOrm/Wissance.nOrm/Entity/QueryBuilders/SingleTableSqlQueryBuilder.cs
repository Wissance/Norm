using System.Data.Common;
using Wissance.nOrm.Entity.Config;
using Wissance.nOrm.Sql;

namespace Wissance.nOrm.Entity.QueryBuilders
{
    /// <summary>
    ///     This is an abstract class partially implementing  IDbEntityQueryBuilder. All BuildSelect... methods
    ///     using only one table defined in EntityConfig.Table
    /// </summary>
    /// <typeparam name="TE"></typeparam>
    public abstract class SingleTableSqlQueryBuilder<TE> : IDbEntityQueryBuilder<TE>
        where TE : class
    {
        public SingleTableSqlQueryBuilder(EntityConfig config, Action<DbCommand, IList<WhereParameter>> commandParametersHandler,
            Func<string, object, DbParameter> parameterBuilderFunc)
        {
            _config = config;
            _commandParametersHandler = commandParametersHandler;
            _parameterBuilderFunc = parameterBuilderFunc;
        }

        public virtual string BuildSelectManyQuery(int? page, int? size, IList<WhereParameter> whereClause = null, IList<string> columns = null)
        {
            string columnsList = string.Join(", ", _config.FullColumnList);
            if (columns != null && columns.Any())
            {
                columnsList = string.Join(", ", columns);
            }

            string whereStatement = String.Empty;
            if (whereClause != null && whereClause.Any())
            {
                whereStatement = StatementsGenerator.BuildWhereStatement(whereClause);
            }

            string limitStatement = String.Empty;
            if (page.HasValue && size.HasValue)
            {
                int offsetValue = page.Value > 0 ? (page.Value - 1) * size.Value : 0;
                limitStatement = $" LIMIT {size.Value} OFFSET {offsetValue}";
            }

            // Consider that in MySQL we don't use Schema in Pg or SQL Server we use Schema.TableName
            // Here is a scheme for query : 0 -> column list, 1 -> Table name 2 -> WHERE Clause
            string query = String.Format("SELECT {0} FROM {1} {2} {3}", columnsList, GetTableNameWithScheme(), whereStatement, limitStatement);
            return query;
        }

        public virtual void BuildSelectManyCommandQueryAndParams(DbCommand command, int? page, int? size, IList<WhereParameter> whereClause = null,
            IList<string> columns = null)
        {
            string columnsList = string.Join(", ", _config.FullColumnList);
            if (columns != null && columns.Any())
            {
                columnsList = string.Join(", ", columns);
            }
            
            string limitStatement = String.Empty;
            if (page.HasValue && size.HasValue)
            {
                int offsetValue = page.Value > 0 ? (page.Value - 1) * size.Value : 0;
                limitStatement = $" LIMIT {size.Value} OFFSET {offsetValue}";
            }

            string wherePreparedStatement = StatementsGenerator.BuildWherePreparedStatement(whereClause ?? new List<WhereParameter>());
            string query = String.Format("SELECT {0} FROM {1} {2} {3}", columnsList, 
                GetTableNameWithScheme(), wherePreparedStatement, limitStatement);
            command.CommandText = query;
            _commandParametersHandler(command, whereClause);
        }

        public virtual string BuildSelectOneQuery(IList<WhereParameter> whereClause = null, IList<string> columns = null)
        {
            string columnsList = string.Join(", ", _config.FullColumnList);
            if (columns != null && columns.Any())
            {
                columnsList = string.Join(", ", columns);
            }
            
            string whereStatement = String.Empty;
            if (whereClause != null && whereClause.Any())
            {
                whereStatement = StatementsGenerator.BuildWhereStatement(whereClause);
            }
            string query = String.Format("SELECT {0} FROM {1} {2} LIMIT 1", columnsList, GetTableNameWithScheme(), whereStatement);
            return query;
        }

        public virtual void BuildSelectOneCommandQueryAndParams(DbCommand command, IList<WhereParameter> whereClause = null, 
            IList<string> columns = null)
        {
            string columnsList = string.Join(", ", _config.FullColumnList);
            if (columns != null && columns.Any())
            {
                columnsList = string.Join(", ", columns);
            }

            string wherePreparedStatement = StatementsGenerator.BuildWherePreparedStatement(whereClause ?? new List<WhereParameter>());
            string query = String.Format("SELECT {0} FROM {1} {2} LIMIT 1", columnsList, GetTableNameWithScheme(), wherePreparedStatement);
            command.CommandText = query;
            _commandParametersHandler(command, whereClause);
        }

        public abstract string BuildInsertSqlQuery(TE entity);
        public abstract void BuildInsertCommandQueryAndParams(DbCommand command, TE entity);
        public abstract string BuildBulkInsertSqlQuery(IList<TE> entities);
        public abstract void BuildBulkInsertCommandQueryAndParams(DbCommand command, IList<TE> entities);
        public abstract void BuildUpdateCommandQueryAndParams(DbCommand command, TE entity);
        public abstract void BuildBulkUpdateCommandQueryAndParams(DbCommand command, IList<TE> entities);
        public abstract string BuildUpdateSqlQuery(TE entity);

        public virtual string BuildDeleteQuery(IList<WhereParameter> whereClause)
        {
            string whereStatement = StatementsGenerator.BuildWherePreparedStatement(whereClause);
            return $"DELETE FROM {GetTableNameWithScheme()} {whereStatement}";
        }

        public virtual void BuildDeleteCommandQueryAndParams(DbCommand command, IList<WhereParameter> whereClause)
        {
            string whereStatement = StatementsGenerator.BuildWherePreparedStatement(whereClause);
            command.CommandText = $"DELETE FROM {GetTableNameWithScheme()} {whereStatement}";
            _commandParametersHandler(command, whereClause);
        }

        public virtual string GetTableSchema()
        {
            return _config.Schema;
        }

        public virtual string GetTableName()
        {
            return _config.Table;
        }

        public virtual string GetModelType()
        {
            return _config.Model;
        }
        
        protected string GetTableNameWithScheme()
        {
            if (string.IsNullOrEmpty(GetTableSchema()))
                return GetTableName();
            return $"{GetTableSchema()}.{GetTableName()}";
        }

        private readonly EntityConfig _config;
        private readonly Action<DbCommand, IList<WhereParameter>> _commandParametersHandler;
        private readonly Func<string, object, DbParameter> _parameterBuilderFunc;
    }
}