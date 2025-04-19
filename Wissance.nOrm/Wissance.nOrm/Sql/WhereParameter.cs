using System.Text;

namespace Wissance.nOrm.Sql
{
    /// <summary>
    /// Condition for parameters Join 
    ///     Or Name = Value
    ///     And Name = Value
    ///     Not Name = Value, if this parameter is not first it should follow the Or or And
    ///     Combined conditions - And Not , Or Not
    /// </summary>
    public enum WhereJoinCondition
    {
        Or,                  // Or Name = Value
        And,                 // And Name = Value
        Not                  // Not Name = Value, if this parameter is not first it should follow the Or or And
    }

    // TODO(umv): make comparisonOperator enum
    // todo(UMV): temporarily not export until https://github.com/Wissance/Norm/issues/2 is solved
    class WhereParameter
    {
        /// <summary>
        /// Sql parameter constructor
        /// </summary>
        /// <param name="tableAlias"> 
        ///     Alias of table to query parameter from any table i.e.
        ///     SELECT {alias}.col, ... FROM {table} AS {alias} WHERE {alias}.col {operator} {value}
        /// </param>
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
        public WhereParameter(string tableAlias, string parameterName, string comparisonOperator, string parameterValue)
        {
            TableAlias = tableAlias;
            ParameterName = parameterName;
            ComparisonOperator = comparisonOperator;
            ParameterValue = parameterValue;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(TableAlias))
            {
                sb.Append($"{TableAlias}.");
            }

            sb.Append(ParameterName);
            sb.Append(ComparisonOperator);
            sb.Append(ParameterValue);

            return sb.ToString();
        }

        public string TableAlias { get; set; }
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
        public string ComparisonOperator { get; set; }
    }
}