using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models.Equipment;
using Prorent24.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Subhire
{
    [GridOptions(HierarchyRoot = "childrens")]
    public class SubhireEquipmentGridDto : BaseDto<int>
    {
        public int? SubhireId { get; set; }

        [IncludeToGrid(Order = 5, IsDisplay = false)]
        public int GroupId { get; set; }

        [IncludeToGrid(Order = 6)]
        public string GroupName { get; set; }

        [IncludeToGrid(Order = 7, IsDisplay = false)]
        public int EquipmentId { get; set; }

        [IncludeToGrid(Order = 8)]
        public string EquipmentName { get; set; }


        [IncludeToGrid(Order = 9, IsDisplay = false)]
        public int? ParentId { get; set; } // for equipment kit type

        [IncludeToGrid(Order = 10, IsEntity = true, EntityKey = "childrens", IsDisplay = false)]
        public List<SubhireEquipmentGridDto> Childrens { get; set; } = new List<SubhireEquipmentGridDto>();

        [IncludeToGrid(Order = 11, IsDisplay = false)]
        public string Name { get; set; }

        [IncludeToGrid(Order = 12)]
        public int? Quantity { get; set; }

        [IncludeToGrid(Order = 13)]
        public decimal? Price { get; set; }

        [IncludeToGrid(Order = 14)]
        public decimal? Discount { get; set; }

        [IncludeToGrid(Order = 15)]
        public decimal? Factor { get; set; }

        [IncludeToGrid(Order = 16)]
        public string Remark { get; set; }

        [IncludeToGrid(Order = 17)]
        public decimal? TotalPrice { get; set; }

    }
}
