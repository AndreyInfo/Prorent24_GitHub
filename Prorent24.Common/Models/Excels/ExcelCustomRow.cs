using Prorent24.Common.Models.Columns;
using Prorent24.Common.Models.Grids;
using System.Collections.Generic;

namespace Prorent24.Common.Models.Excels
{
    public class ExcelCustomRow
    {
        public ExcelCustomRow()
        {
            Cells = new List<Column>();
        }

        public List<Column> Cells { get; set; }
    }
}
