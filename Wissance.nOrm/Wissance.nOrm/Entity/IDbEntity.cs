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
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDbEntity<T>
    where T:IComparable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetTableSchema();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetTableName();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetModelType();
        /// <summary>
        /// 
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
        string GetSelectManyQuery(IList<string> columns, IDictionary<string, object> whereClause);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        string GetSelectOneQuery(IList<string> columns, IDictionary<string, object> whereClause);
    }
}