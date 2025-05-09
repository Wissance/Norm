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
        Or = 1,   // Or Name = Value
        And,      // And Name = Value
    }

    public enum WhereComparison
    {
        Equal = 1,
        NotEqual,
        Less,
        LessOrEqual,
        Greater,
        GreaterOrEqual,
        In,
        Between
    }

    // TODO(umv): make comparisonOperator enum
    // todo(UMV): temporarily not export until https://github.com/Wissance/Norm/issues/2 is solved
    public class WhereParameter
    {
        /// <summary>
        /// Where parameter descriptor, this class holds only description and not building SQL
        /// It should be noted that if we using alias (AS ...) i.e. SELECT o.Id, o.Date FROM Orders as o
        /// we should pass o.Column name , this class does n't care about aliases this is up to developer
        /// </summary>
        /// <param name="column"> 
        ///     Name of column could contain aliases
        /// </param>
        /// <param name="joinCondition">
        ///     this is for how to WHERE joins in SQL query i.e. "...WHERE o.Id > 100 OR o.Date<'2025-05-05 10:00:00';"
        ///     Null for first item JOIN operation for Left item
        /// </param>
        /// <param name="inverted">
        ///     if true NOT should be added between column and comparison operator i.e. WHERE o.City NOT IN (...)
        /// </param>
        /// <param name="comparisonOperator">
        ///     >, >=, !=, =, IS, In - Name In Value, Value is: (v1, v2, ....v3), Between - Name Between Value, Value is: v1 and v2, Like
        /// </param>
        /// <param name="filterValues">
        ///    contains values to compare, i.e. for operator consider single value i.e. > filterValues contains only one
        ///    value, for multiple values i.e. IN, BETWEEN contains more then 1 operator
        /// </param>
        /// <param name="valueQuotesWrap">
        ///    defines whether single quotes should be used around values or not, we don't use Reflection here to reach
        ///    maximum performance
        /// </param>
        public WhereParameter(string column, WhereJoinCondition? joinCondition, bool inverted, 
            WhereComparison comparisonOperator, IList<object> filterValues, bool valueQuotesWrap = false)
        {
            Column = column;
            JoinCondition = joinCondition;
            Inverted = inverted;
            ComparisonOperator = comparisonOperator;
            FilterValues = filterValues;
            ValueQuotesWrap = valueQuotesWrap;
        }

        public string Column { get; set; }
        public bool Inverted { get; set; }
        public IList<object> FilterValues { get; set; }
        public WhereComparison ComparisonOperator { get; set; }
        
        public WhereJoinCondition? JoinCondition { get; set; }
        public bool ValueQuotesWrap { get; set; }
    }
}