using System.Data.Common;
using Wissance.nOrm.Sql;

namespace Wissance.nOrm.Entity.QueryBuilders
{
    /// <summary>
    ///     IDbEntityQueryBuilder is a interface that helps to build queries to perform following operation:
    ///         1. GetCreateSqlQuery returns Query to INSERT an item based on Entity fields,
    ///            it forms an SQL like INSERT INTO @GetTableSchema().@GetTableName() (col1, col2, ...) VALUES(val1, val2, ...);
    ///         2. GetUpdateSqlQuery returns Query to UPDATE an item based on Entity fields,
    ///            it forms an SQL like UPDATE @GetTableSchema().@GetTableName() SET (col1=val1, col2=val2, ...)
    ///         3. GetSelectManyQuery returns Query to SELECT a multiple items based on WHERE clauses,
    ///            it forms an SQL like SELECT (col1, col2, ...) FROM @GetTableSchema().@GetTableName() WHERE (col1=...)
    ///         4. GetSelectOneQuery returns Query to SELECT a single item based in WHERE clauses,
    ///            it forms an SQL like SELECT (col1, col2, ...) FROM @GetTableSchema().@GetTableName() WHERE (col1=...)
    ///      Notes:
    ///         1. Methods without DbCommand all are Unsafe (Vulnerable to SQL Injection) methods
    ///         2. Methods with DbCommand params are safe methods i.e. BuildSelectManyCommandQueryAndParams
    ///      Entity is a class that related with table.
    /// </summary>
    /// <typeparam name="TE"> Entity </typeparam>
    public interface IDbEntityQueryBuilder<TE>
        where TE : class
    {
        /// <summary>
        ///     Code for build query for select multiple Entity from Database. This method assume that
        ///     parameters in whereClause are placed without processing in Raw SQL. Therefore, this
        ///     is an Unsafe method, safe version is a BuildSelectManyCommandQueryAndParams.
        /// </summary>
        /// <param name="page">page number</param>
        /// <param name="size">size of page</param>
        /// <param name="whereClause">a set of where params</param>
        /// <param name="columns">list of columns to select, if not defined all columns will be selected</param>
        /// <returns>SQL query for selecting many items</returns>
        [Obsolete("This func is obsolete due to it unsafe and vulnerable to SQL-Injection. Use BuildSelectManyCommandQueryAndParams instead.", false)]
        string BuildSelectManyQuery(int? page, int? size, IList<WhereParameter> whereClause, IList<string> columns);

        /// <summary>
        ///     Code for build query for select multiple Entity from Database. This method assume that parameters are placing
        ///     by mention @p1 and so on. There is a default implementation of this method in class SingleTableSqlQueryBuilder
        ///     it is working with a single table.
        /// </summary>
        /// <param name="command">DbCommand to be configured Command and Parameters</param>
        /// <param name="page">rows to skip size*(page-1)</param>
        /// <param name="size">size of portion in a number of rows</param>
        /// <param name="whereClause">set of WhereParameters</param>
        /// <param name="columns">columns to select</param>
        void BuildSelectManyCommandQueryAndParams(DbCommand command, int? page, int? size, IList<WhereParameter> whereClause, 
            IList<string> columns);
        
        /// <summary>
        ///     Code for build query for select one Entity from Database. This method assume that
        ///     parameters in whereClause are placed without processing in Raw SQL. Therefore, this
        ///     is an Unsafe method, safe version is a BuildSelectOneCommandQueryAndParams. There is a default
        ///     implementation of this method in class SingleTableSqlQueryBuilder it is working with a single table.
        /// </summary>
        /// <param name="whereClause">a set of where clauses</param>
        /// <param name="columns">columns to select</param>
        /// <returns>SQL query for selecting an item</returns>
        [Obsolete("This func is obsolete due to it unsafe and vulnerable to SQL-Injection. Use BuildSelectOneCommandQueryAndParams instead.", false)]
        string BuildSelectOneQuery(IList<WhereParameter> whereClause, IList<string> columns);
        
        /// <summary>
        ///     BuildSelectOneCommandQueryAndParams is a method to select only a one item by SELECT statement
        ///     this method has command parameters which implies parameter passing via IParameterBuilder.Build(name, value)
        ///     method where parameter name is @pN, i.e. @p1, @p2, ....@p99
        ///     Query template is passing via command.CommandText property
        /// </summary>
        /// <param name="command">DbCommand instance</param>
        /// <param name="whereClause">a set of where clauses</param>
        /// <param name="columns">columns to select</param>
        /// <returns>nothing, SQL template and params assigned to appropriate DbCommand properties</returns>
        void BuildSelectOneCommandQueryAndParams(DbCommand command, IList<WhereParameter> whereClause, 
            IList<string> columns);

        /// <summary>
        ///     Code for build query for insert one Entity to Database. Unsafe method (due to it vulnerable to SQL injection) because it is
        ///     using direct placing properties values into placeholders
        /// </summary>
        /// <returns>SQL for insert entity</returns>
        [Obsolete("This func is obsolete due to it unsafe and vulnerable to SQL-Injection. Use BuildInsertCommandQueryAndParams instead.", false)]
        string BuildInsertSqlQuery(TE entity);
        
        /// <summary>
        ///     Code for build query for insert Entity to Database
        /// </summary>
        /// <param name="command">DbCommand instance</param>
        /// <param name="entity"></param>
        /// <returns>nothing, SQL template and params assigned to appropriate DbCommand properties</returns>
        void BuildInsertCommandQueryAndParams(DbCommand command, TE entity);
        
        /// <summary>
        ///     Code for build query for insert multiple Entities to Database
        /// </summary>
        /// <returns>SQL with Bukl insert statement</returns>
        [Obsolete("This func is obsolete due to it unsafe and vulnerable to SQL-Injection. Use BuildBulkInsertCommandQueryAndParams instead.", false)]
        string BuildBulkInsertSqlQuery(IList<TE> entities);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="entities"></param>
        void BuildBulkInsertCommandQueryAndParams(DbCommand command, IList<TE> entities);
        
        /// <summary>
        ///     Code for build query for update Entity in Database. Unsafe method (due to it vulnerable to SQL injection) because it is
        ///     using direct placing properties values into placeholders
        /// </summary>
        /// <returns>SQL with update statement</returns>
        [Obsolete("This func is obsolete due to it unsafe and vulnerable to SQL-Injection. Use BuildUpdateCommandQueryAndParams instead.", false)]
        string BuildUpdateSqlQuery(TE entity);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="entity"></param>
        void BuildUpdateCommandQueryAndParams(DbCommand command, TE entity);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="entities"></param>
        void BuildBulkUpdateCommandQueryAndParams(DbCommand command, IList<TE> entities);
        
        /// <summary>
        ///     Code for build query for delete Entities from Database. This is an Unsafe method because
        ///     it assumes direct placeholder replacement. Safe version is BuildDeleteCommandQueryAndParams
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns>SQL query for delete items</returns>
        string BuildDeleteQuery(IList<WhereParameter> whereClause);
        
        /// <summary>
        ///     Safe version of Delete operation because it is using parameters substitution
        /// </summary>
        /// <param name="command"></param>
        /// <param name="whereClause"></param>
        void BuildDeleteCommandQueryAndParams(DbCommand command, IList<WhereParameter> whereClause);

        /// <summary>
        ///     Returns Table Schema i.e. for postgres public
        /// </summary>
        /// <returns>Returns TableSchema i.e. for postgres public</returns>
        string GetTableSchema();
        
        /// <summary>
        ///    Returns Table Name
        /// </summary>
        /// <returns>Returns Table Name</returns>
        string GetTableName();
        
        /// <summary>
        ///    Returns ModelType as a string, probably we could replace it with $"{GetTableSchema()}.{GetTableName()}"
        /// </summary>
        /// <returns></returns>
        string GetModelType();
    }
}