using Prorent24.Entity.Base;
using Prorent24.Entity.Project;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Equipment
{
    [Table("dbo_equipments_reserved")]
    public class EquipmentReservedEntity : BaseEntity
    {
        public int ProjectEquipmentId { get; set; }

        [ForeignKey("ProjectEquipmenId")]
        public virtual ProjectEquipmentEntity ProjectEquipmentEntity { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime From { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime Until { get; set; }

        public int Quantity { get; set; }
    }
}
