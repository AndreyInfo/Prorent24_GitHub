using Prorent24.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Equipment
{
    [Table("dbo_equipment_alternatives")]
    public class EquipmentAlternativeEntity: BaseEntity
    {
        public int EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public virtual EquipmentEntity Equipment { get; set; }
        public int AlternativeId { get; set; }
        [ForeignKey("AlternativeId")]
        public EquipmentEntity Alternative { get; set; }
    }
}
