using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Equipment
{
    public class EquipmentStockViewModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        //public virtual EquipmentEntity Equipment { get; set; }
        public int Quantity { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string Date { get; set; }

    }
}
