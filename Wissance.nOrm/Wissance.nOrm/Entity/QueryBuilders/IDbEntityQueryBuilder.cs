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
    ///      Entity is a class that related with table.
    /// </summary>
    /// <typeparam name="TE"> Entity </typeparam>
    public interface IDbEntityQueryBuilder<TE>
        where TE : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="whereClause"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        // TODO(UMV) : whereClause should be modified as a structure that describes a way how to compare
        string BuildSelectManyQuery(int? page, int? size, IDictionary<string, object> whereClause = null, IList<string> columns = null);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="columns"></param>\
        /// <returns></returns>
        // TODO(UMV) : whereClause should be modified as a structure that describes a way how to compare
        string BuildSelectOneQuery(IDictionary<string, object> whereClause = null, IList<string> columns = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string BuildInsertSqlQuery(TE entity);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string BuildUpdateSqlQuery(TE entity);
        

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