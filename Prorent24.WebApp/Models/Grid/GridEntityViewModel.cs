using Prorent24.Common.Models.Grids;
using Prorent24.Common.Models.Trees;
using Prorent24.Enum.Entity;
using System.Collections.Generic;

namespace Prorent24.WebApp.Models.Grid
{
    public class GridEntityViewModel
    {
        public EntityEnum ModuleEntity { get; set; }
        public List<TreeColumn> Columns { get; set; }
    }
}
