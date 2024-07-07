namespace Wissance.nOrm.Sql
{
    /// <summary>
    /// Condition for parameters Join 
    ///     Or Name = Value
    ///     And Name = Value
    ///     Not Name = Value, if this parameter is not first it should follow the Or or And
    ///     Combined conditions - And Not , Or Not
    /// </summary>
    public enum JoinCondition
    {
        Or,                  // Or Name = Value
        And,                 // And Name = Value
        Not                  // Not Name = Value, if this parameter is not first it should follow the Or or And
    }
    
    public class QueryParameter
    {
        /// <summary>
        /// Sql parameter constructor
        /// </summary>
        /// <param name="parameterName"> 
        ///     Name of column 
        /// </param>
        /// <param name="comparisonOperator">
        ///     >, >=, !=, =, IS, In - Name In Value, Value is: (v1, v2, ....v3), Between - Name Between Value, Value is: v1 and v2, Like
        /// </param>
        /// <param name="parameterValue">
        ///    a) for where parameter is actual value but for IN should be string 'v1, v2, v3' for BETWEEN - string 'v1 AND v2'
        ///    b) for order by parameters ASC or DESC
        /// </param>
        public QueryParameter(string parameterName, string comparisonOperator, string parameterValue)
        {
            ParameterName = parameterName;
            ComparisonOperator = comparisonOperator;
            ParameterValue = parameterValue;
        }
        
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
        public string ComparisonOperator { get; set; }
    }
}