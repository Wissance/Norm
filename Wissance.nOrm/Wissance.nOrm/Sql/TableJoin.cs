namespace Wissance.nOrm.Sql
{
    public class TableJoin
    {
        /// <summary>
        ///  Type of JOIN: INNER, LEFT, RIGHT 
        /// </summary>
        public string JoinType { get; set; }
        public string Table { get; set; }
        public string Alias { get; set; }
    }
}