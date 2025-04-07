using Prorent24.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Subhire
{
    [GridOptions(HierarchyRoot = "childrens")]
    public class SubhireShortageGridDto
    {
        public int? ParentId { get; set; }

        [IncludeToGrid(Order = 1, IsDisplay = false)]
        public int ProjectId { get; set; }

        [IncludeToGrid(Order = 2)]
        public string ProjectName { get; set; }

        [IncludeToGrid(Order = 3, IsDisplay = false)]
        public int EquipmentId { get; set; }

        [IncludeToGrid(Order = 4)]
        public string EquipmentName { get; set; }

        [IncludeToGrid(Order = 5)]
        public int? PlannedQuantity { get; set; }

        [IncludeToGrid(Order = 6)]
        public int? OwnStockQuantity { get; set; }

        [IncludeToGrid(Order = 7)]
        public int? SubhiredQuantity { get; set; }

        [IncludeToGrid(Order = 8)]
        public int? ShortageQuantity { get; set; }

        [IncludeToGrid(Order = 9)]
        public string Explanation { get; set; }

        [IncludeToGrid(Order = 10)]
        public DateTime? StartUsePeriod { get; set; }

        [IncludeToGrid(Order = 11)]
        public DateTime? EndUsePeriod { get; set; }

        [IncludeToGrid(Order = 12, IsDisplay = false)]
        public int[] SubhireIds { get; set; }

        [IncludeToGrid(Order = 13, IsEntity = true, EntityKey = "childrens", IsDisplay = false)]
        public List<SubhireShortageGridDto> Childrens { get; set; } = new List<SubhireShortageGridDto>();

    }
}
