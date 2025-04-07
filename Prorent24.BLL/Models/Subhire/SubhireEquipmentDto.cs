using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models.Equipment;
using Prorent24.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Subhire
{
    public class SubhireEquipmentDto : BaseDto<int>
    {
        public int? SubhireId { get; set; }

        [IncludeToGrid(Order = 5, IsDisplay = false)]
        public int GroupId { get; set; }

        [IncludeToGrid(Order = 6)]
        public string GroupName { get { return this.Group?.Name; } }
        public SubhireEquipmentGroupDto Group { get; set; }
        [IncludeToGrid(Order = 7, IsDisplay = false)]
        public int EquipmentId { get; set; }

        [IncludeToGrid(Order = 8)]
        public string EquipmentName { get { return this.Equipment?.Name; } }
        public EquipmentDto Equipment { get; set; }

        [IncludeToGrid(Order = 9,IsDisplay = false)]
        public int? ParentId { get; set; } // for equipment kit type

        public ICollection<SubhireEquipmentDto> Children { get; set; }

        [IncludeToGrid(Order = 10, IsDisplay = false)]
        public string Name { get; set; }

        [IncludeToGrid(Order = 11)]
        public int Quantity { get; set; }

        [IncludeToGrid(Order = 12)]
        public decimal Price { get; set; }

        [IncludeToGrid(Order = 13)]
        public decimal Discount { get; set; }

        [IncludeToGrid(Order = 14)]
        public decimal Factor { get; set; }

        [IncludeToGrid(Order = 15)]
        public string Remark { get; set; }

        [IncludeToGrid(Order = 16)]
        public decimal TotalPrice
        {
            get
            {
                return Price.TotalPriceCalculation(Discount, Factor, Quantity);
            }
            set { }
        }
    }
}
