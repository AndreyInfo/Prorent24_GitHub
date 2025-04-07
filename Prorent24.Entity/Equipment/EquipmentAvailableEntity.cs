using Prorent24.Entity.Base;
using Prorent24.Enum.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Equipment
{
    [Table("dbo_equipment_available")]
    public class EquipmentAvailableEntity : BaseEntity
    {
        public EquipmentAvailableEnum Type { get; set; }
        public int EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public virtual EquipmentEntity Equipment { get; set; }
        public int ReservedQuantity { get; set; }
        public DateTime ReservedFrom { get; set; }
        public DateTime ReservedUntil { get; set; }
        public bool Reserved { get; set; }
    }
}