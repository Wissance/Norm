namespace Wissance.nOrm.Entity
{
    /// <summary>
    ///     IDbEntity is a interface that should be implemented by all Entities to perform operation:
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
    /// <typeparam name="T">Comparable type, Key</typeparam>
    public interface IDbEntity<T>
    where T:IComparable
    {
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
        
        /// <summary>
        ///    Returns Identifier of Entity
        /// </summary>
        /// <returns></returns>
        T GetId();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetCreateSqlQuery();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetUpdateSqlQuery();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        // TODO(UMV) : whereClause should be modified as a structure that describes a way how to compare
        string GetSelectManyQuery(IList<string> columns, IDictionary<string, object> whereClause);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        // TODO(UMV) : whereClause should be modified as a structure that describes a way how to compare
        string GetSelectOneQuery(IList<string> columns, IDictionary<string, object> whereClause);
    }
}