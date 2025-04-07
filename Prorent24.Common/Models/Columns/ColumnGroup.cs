using System.Collections.Generic;

namespace Prorent24.Common.Models.Columns
{
    public class ColumnGroup
    {
        public ColumnGroup()
        {
            Columns = new List<Column>();
        }

        public int Key { get; set; }
        public string Name { get; set; }
        public List<Column> Columns { get; set; }
    }
}