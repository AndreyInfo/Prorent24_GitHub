using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Project
{
    [GridOptions(HierarchyRoot = "childrens")]
    public class ProjectEquipmentGridDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5, IsDisplay = false)]
        public int GroupId { get; set; }


        public ProjectEquipmentGroupDto Group { get; set; }

        [IncludeToGrid(Order = 6, ColumnGroup = ColumnGroupEnum.General)]
        public string GroupName
        {
            get
            {
                return Group?.Name;
            }
            set { }
        }

        [IncludeToGrid(Order = 7, ColumnGroup = ColumnGroupEnum.General)]
        public string PeriodOfUse
        {
            get
            {
                return string.Concat(StartUsePeriod.HasValue ? StartUsePeriod.Value.ToString() : "---", " - ", EndUsePeriod.HasValue ? EndUsePeriod.Value.ToString() : "---");
            }
        }

        [IncludeToGrid(Order = 8, IsSystem = true, IsDisplay = false)]
        public int? EquipmentId { get; set; }

        [IncludeToGrid(Order = 9, ColumnGroup = ColumnGroupEnum.General)]
        public string EquipmentName { get; set; }

        [IncludeToGrid(Order = 10, IsSystem = true, IsDisplay = false)]
        public int? ParentId { get; set; } // for equipment kit type

        [IncludeToGrid(IsEntity = true, EntityKey = "childrens", IsDisplay = false)]
        public List<ProjectEquipmentGridDto> Childrens { get; set; } = new List<ProjectEquipmentGridDto>();

        [IncludeToGrid(Order = 11, ColumnGroup = ColumnGroupEnum.General)]
        public int? Quantity { get; set; }

        [IncludeToGrid(Order = 12, ColumnGroup = ColumnGroupEnum.General)]
        public decimal? Price { get; set; }

        [IncludeToGrid(Order = 13, ColumnGroup = ColumnGroupEnum.General)]
        public decimal? Discount { get; set; }

        [IncludeToGrid(Order = 14, ColumnGroup = ColumnGroupEnum.General)]
        public decimal? Factor { get; set; }

        [IncludeToGrid(Order = 15, ColumnGroup = ColumnGroupEnum.General)]
        public string Remark { get; set; }

        [IncludeToGrid(Order = 16, ColumnGroup = ColumnGroupEnum.General)]
        public decimal? TotalPrice { get; set; }

        [IncludeToGrid(IsDisplay = false)]
        public long? AvailableQuantity { get; set; }

        [IncludeToGrid(IsDisplay = false)]
        public DateTime? StartUsePeriod { get; set; }

        [IncludeToGrid(IsDisplay = false)]
        public DateTime? EndUsePeriod { get; set; }

        [IncludeToGrid(IsDisplay = false)]
        public DateTime? StartPlanPeriod { get; set; }

        [IncludeToGrid(IsDisplay = false)]
        public DateTime? EndPlanPeriod { get; set; }


        public int? VatClassId { get; set; }
    }
}
