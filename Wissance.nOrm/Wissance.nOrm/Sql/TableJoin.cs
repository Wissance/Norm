namespace Wissance.nOrm.Sql
{
    public class TableJoin
    {
        /// <summary>
        ///  Type of JOIN: INNER, LEFT, RIGHT 
        /// </summary>
        public string JoinType { get; set; }
        public string JoinTable { get; set; }
        public string JoinAlias { get; set; }
    }
}