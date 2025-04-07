using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Equipment
{
    public class EquipmentStockCorrectDto
    {
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
    }
}
