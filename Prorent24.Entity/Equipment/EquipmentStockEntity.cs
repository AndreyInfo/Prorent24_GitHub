using Prorent24.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Equipment
{
    [Table("dbo_equipment_stocks")]
    public class EquipmentStockEntity : BaseEntity
    {
        public int EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public virtual EquipmentEntity Equipment { get; set; }
        public int Quantity { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
    }
}
