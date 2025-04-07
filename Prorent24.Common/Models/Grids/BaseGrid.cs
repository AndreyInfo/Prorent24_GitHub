using Prorent24.Common.Models.Actions;
using Prorent24.Common.Models.Columns;
using Prorent24.Common.Models.Trees;
using System.Collections.Generic;

namespace Prorent24.Common.Models.Grids
{
    public class BaseGrid
    {
        public BaseGrid()
        {
            Columns = new List<Column>();
            Data = new List<Dictionary<string, object>>();
        }

        public BaseGrid(GridActions actions)
        {
            Columns = new List<Column>();
            Data = new List<Dictionary<string, object>>();
            DataGroups =
            this.Add = actions.Add;
            this.Edit = actions.Edit;
            this.View = actions.View;
            this.Delete = actions.Delete;
        }

        public bool GroupData { get; set; }

        public List<Column> Columns { get; set; }

        public List<Dictionary<string,object>> Data { get; set; }

        public TreeHierarchy Hierarchy { get; set; }

        public object DataGroups { get; set; }

        public bool Add { get; set; } = true;
        public bool View { get; set; } = true;
        public bool Edit { get; set; } = true;
        public bool Delete { get; set; } = true;

        //public List<Row> Data { get; set; }
    }
}