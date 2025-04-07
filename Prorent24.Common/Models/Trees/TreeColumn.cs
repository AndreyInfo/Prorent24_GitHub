namespace Prorent24.Common.Models.Trees
{
    public class TreeColumn
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public bool Selected { get; set; }
        public bool IsRequiredForImport { get; set; }
        public int Order { get; set; }
    }
}
