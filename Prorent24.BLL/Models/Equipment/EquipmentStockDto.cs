using Prorent24.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Equipment
{
    public class EquipmentStockDto : BaseDto<int>
    {
        public int EquipmentId { get; set; }
        public EquipmentDto Equipment { get; set; }

        [IncludeToGrid(Order = 5)]
        public int Quantity { get; set; }

        [IncludeToGrid(Order = 6)]
        public string Description { get; set; }

        [IncludeToGrid(Order = 7)]
        public string Details { get; set; }

        [IncludeToGrid(Order = 8, IsDisplay = false, IncludeType = "dateshort", KeyType = "date")]
        public DateTime? DeliveryDate { get; set; }

        [IncludeToGrid(Order = 9)]
        public string Date { get; set; } //==DeliveryDate
    }
}
