using Prorent24.Common.Models.Grids;
using Prorent24.Common.Models.Trees;
using System.Collections.Generic;

namespace Prorent24.Common.Models.Excels
{
    public class ExcelCustomWorksheet
    {
        public ExcelCustomWorksheet()
        {
            RequiredFields = new List<string>();
            Entities = new List<EntityGrid>();
            SubEntities = new List<TreeGroupColumn>();
            Rows = new List<ExcelCustomRow>();
            Columns = new List<ExcelCustomRow>();
        }
        public string FilePath { get; set; }
        public int FolderId { get; set; }

        public bool ContainsHeader = true;

        public List<string> RequiredFields {get;set;}

        public List<EntityGrid> Entities { get; set; }

        public List<TreeGroupColumn> SubEntities { get; set; }

        public List<ExcelCustomRow> Rows { get; set; }
        public List<ExcelCustomRow> Columns { get; set; }
    }
}
