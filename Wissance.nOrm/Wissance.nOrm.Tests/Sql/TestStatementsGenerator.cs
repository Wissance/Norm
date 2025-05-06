using Wissance.nOrm.Sql;

namespace Wissance.nOrm.Tests.Sql
{
    public class TestStatementsGenerator
    {
        [Theory]
        [InlineData("id", WhereComparison.Equal, false, false, "2", "id = 2 ")]
        [InlineData("id",WhereComparison.NotEqual, false, false, "2", "id != 2 ")]
        public void TestGenerateSingleWhereCondition(string column, WhereComparison comparisonOperator, bool invertedComparison, 
            bool quotesRequired, string value, string expectedStatement)
        {
            WhereParameter parameter = new WhereParameter(column, null, invertedComparison, comparisonOperator,
                new List<object>() {value}, quotesRequired);
            string actualStatement = StatementsGenerator.BuildWhereStatement(new List<WhereParameter>() {parameter});
            Assert.Equal(expectedStatement, actualStatement);
        }
    }
}