using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Project
{
    public class ProjectAdditionalCostDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5, ColumnGroup = ColumnGroupEnum.General)]
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public ProjectDto Project { get; set; }
        //public string ProjectName { get { return this.Project?.Name; } }
        [IncludeToGrid(Order = 6, ColumnGroup = ColumnGroupEnum.General)]
        public decimal PurchasePrice { get; set; }
        [IncludeToGrid(Order = 7, ColumnGroup = ColumnGroupEnum.General)]
        public decimal SalePrice { get; set; }
        public int VatClassId { get; set; }
        [IncludeToGrid(Order = 8, ColumnGroup = ColumnGroupEnum.General, IsDisplay = false, IsEntity = true)]
        public VatClassDto VatClass { get; set; }

        [IncludeToGrid(Order = 9, ColumnGroup = ColumnGroupEnum.General, IsDisplay = false)]
        public string VatClassName { get { return this.VatClass?.Name; } }

        [IncludeToGrid(Order = 10, ColumnGroup = ColumnGroupEnum.General)]
        public string Details { get; set; }
    }
}
