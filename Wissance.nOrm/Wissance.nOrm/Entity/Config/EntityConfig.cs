namespace Wissance.nOrm.Entity.Config
{
    public class EntityConfig
    {
        public EntityConfig(string schema, string table,string model, IList<string> fullColumnList)
        {
            Schema = schema;
            Table = table;
            Model = model;
            FullColumnList  = fullColumnList;
        }

        public string Schema { get; set; }
        public string Table { get; set; }
        public string Model { get; set; }
        public IList<string> FullColumnList { get; set; }
    }
}