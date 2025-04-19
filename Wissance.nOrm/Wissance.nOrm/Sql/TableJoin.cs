namespace Wissance.nOrm.Sql
{
    // todo(UMV): temporarily not export until https://github.com/Wissance/Norm/issues/2 is solved
    class TableJoin
    {
        /// <summary>
        ///  Type of JOIN: INNER, LEFT, RIGHT 
        /// </summary>
        public string JoinType { get; set; }
        public string JoinTable { get; set; }
        public string JoinAlias { get; set; }
    }
}