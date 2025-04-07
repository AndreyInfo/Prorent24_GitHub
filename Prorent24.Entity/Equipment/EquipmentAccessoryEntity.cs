using Prorent24.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Equipment
{
    [Table("dbo_equipment_accessories")]
    public class EquipmentAccessoryEntity : BaseEntity
    {
        public int EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public virtual EquipmentEntity Equipment { get; set; }

        public int AccessoryId { get; set; } 
        [ForeignKey("AccessoryId")]
        public EquipmentEntity Accessory { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool Automatic { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool SkipIfAlreadyPresent { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool Free { get; set; } = false;
    }
}
