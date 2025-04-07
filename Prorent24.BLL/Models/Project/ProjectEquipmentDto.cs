using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Models.Equipment;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using System.Collections.Generic;

namespace Prorent24.BLL.Models.Project
{
    public class ProjectEquipmentDto : BaseDto<int>
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        [IncludeToGrid(Order = 5, IsDisplay = false)]
        public int GroupId { get; set; }

        [IncludeToGrid(Order = 6, ColumnGroup = ColumnGroupEnum.General)]
        public string GroupName { get { return this.Group?.Name; } }
        public ProjectEquipmentGroupDto Group { get; set; }

        [IncludeToGrid(Order = 7, IsDisplay = false)]
        public int? EquipmentId { get; set; }

        [IncludeToGrid(Order = 8, ColumnGroup = ColumnGroupEnum.General)]
        public string EquipmentName { get { return this.Equipment?.Name; } }
        public EquipmentDto Equipment { get; set; }

        [IncludeToGrid(Order = 9, IsDisplay = false)]
        public int? ParentId { get; set; } // for equipment kit type

        public ICollection<ProjectEquipmentDto> Children { get; set; }

        //[IncludeToGrid]
        public string Name { get; set; }

        [IncludeToGrid(Order = 10, ColumnGroup = ColumnGroupEnum.General)]
        public int Quantity { get; set; }

        [IncludeToGrid(Order = 11, ColumnGroup = ColumnGroupEnum.General)]
        public decimal Price { get; set; }

        [IncludeToGrid(ColumnGroup = ColumnGroupEnum.General)]
        public decimal Discount { get; set; }

        [IncludeToGrid(Order = 12, ColumnGroup = ColumnGroupEnum.General)]
        public decimal Factor { get; set; }

        [IncludeToGrid(Order = 13, ColumnGroup = ColumnGroupEnum.General)]
        public string Remark { get; set; }

        [IncludeToGrid(Order = 14, ColumnGroup = ColumnGroupEnum.General)]
        public decimal TotalPrice { get; set; }

        public int? VatClassId { get; set; }

        public VatClassDto VatClass { get; set; }

        [IncludeToGrid(Order = 15, IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        public long? AvailableQuantity { get; set; }
        


    }
}
