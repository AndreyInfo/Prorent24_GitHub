using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Subhire
{
    public class SubhireShortageDto
    {
        [IncludeToGrid(Order = 1, IsDisplay = false)]
        public int ProjectId { get; set; }

        [IncludeToGrid(Order = 2, ColumnGroup = ColumnGroupEnum.General)]
        public string ProjectName { get; set; }

        [IncludeToGrid(Order = 3, IsDisplay = false)]
        public int EquipmentId { get; set; }

        [IncludeToGrid(Order = 4, ColumnGroup = ColumnGroupEnum.General)]
        public string EquipmentName { get; set; }

        [IncludeToGrid(Order = 5, ColumnGroup = ColumnGroupEnum.General)]
        public int PlannedQuantity { get; set; }

        [IncludeToGrid(Order = 6, ColumnGroup = ColumnGroupEnum.General)]
        public int OwnStockQuantity { get; set; }

        [IncludeToGrid(Order = 7, ColumnGroup = ColumnGroupEnum.General)]
        public int SubhiredQuantity { get; set; }

        [IncludeToGrid(Order = 8, ColumnGroup = ColumnGroupEnum.General)]
        public int ShortageQuantity { get; set; }

        [IncludeToGrid(Order = 9, ColumnGroup = ColumnGroupEnum.General)]
        public string Explanation { get; set; }

        [IncludeToGrid(Order = 10, ColumnGroup = ColumnGroupEnum.General)]
        public DateTime? StartUsePeriod { get; set; }

        [IncludeToGrid(Order = 11, ColumnGroup = ColumnGroupEnum.General)]
        public DateTime? EndUsePeriod { get; set; }

        [IncludeToGrid(Order = 12, IsDisplay = false)]
        public int[] SubhireIds { get; set; }

        [IncludeToGrid(Order = 13, IsEntity = true, EntityKey = "childrens", IsDisplay = false)]
        public List<SubhireShortageDto> Childrens { get; set; } = new List<SubhireShortageDto>();
    }
}
