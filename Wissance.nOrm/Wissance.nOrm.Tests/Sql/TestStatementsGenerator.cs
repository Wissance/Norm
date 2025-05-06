using Wissance.nOrm.Sql;

namespace Wissance.nOrm.Tests.Sql
{
    public class TestStatementsGenerator
    {
        [Theory]
        [InlineData("id", WhereComparison.Equal, false, false, new[] {"2"}, "id = 2 ")]
        [InlineData("id", WhereComparison.Equal, true, false, new[] {"2"}, "id NOT  = 2 ")]
        [InlineData("id",WhereComparison.NotEqual, false, false, new[] {"2"}, "id != 2 ")]
        [InlineData("name", WhereComparison.Equal, false, true, new[] {"sensor_1"}, "name = 'sensor_1' ")]
        [InlineData("value",WhereComparison.GreaterOrEqual, false, false, new[] {"50"}, "value >= 50 ")]
        [InlineData("code",WhereComparison.In, false, false, new[] {"50", "75", "90"}, "code IN (50,75,90) ")]
        [InlineData("code",WhereComparison.In, true, false, new[] {"50", "75", "90"}, "code NOT  IN (50,75,90) ")]
        [InlineData("city",WhereComparison.In, false, true, new[] {"Yekaterinburg", "Perm", "Krasnoyarsk"}, "city IN ('Yekaterinburg','Perm','Krasnoyarsk') ")]
        [InlineData("value",WhereComparison.Between, false, false, new[] {"75", "90"}, "value BETWEEN 75 AND 90 ")]
        [InlineData("code",WhereComparison.Between, false, true, new[] {"0001", "0101"}, "code BETWEEN '0001' AND '0101' ")]
        public void TestGenerateSingleWhereCondition(string column, WhereComparison comparisonOperator, bool invertedComparison, 
            bool quotesRequired, string[] values, string expectedStatement)
        {
            WhereParameter parameter = new WhereParameter(column, null, invertedComparison, comparisonOperator,
                values, quotesRequired);
            string actualStatement = StatementsGenerator.BuildWhereStatement(new List<WhereParameter>() {parameter});
            Assert.Equal(expectedStatement, actualStatement);
        }

        [Fact]
        public void TestGenerateMultipleWhereCondition()
        {
            IList<WhereParameter> parameters = new List<WhereParameter>()
            {
                new WhereParameter("city", null, false, WhereComparison.In, 
                    new List<object>() {"Yekaterinburg", "Perm"}, true),
                new WhereParameter("age", WhereJoinCondition.And, false, WhereComparison.Greater, 
                    new List<object>(){"18"}, false),
                new WhereParameter("age", WhereJoinCondition.And, false, WhereComparison.LessOrEqual, 
                    new List<object>(){"60"}, false),
                new WhereParameter("name", WhereJoinCondition.Or, true, WhereComparison.In, 
                    new List<object>(){"Ivan", "Petr"}, true)
            };
            string expectedStatement = "city IN ('Yekaterinburg','Perm')   AND  age > 18   AND  age <= 60   OR  name NOT  IN ('Ivan','Petr') ";
            string actualStatement = StatementsGenerator.BuildWhereStatement(parameters);
            Assert.Equal(expectedStatement, actualStatement);
        }
    }
}