using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Equipment
{
    [Table("dbo_equipment_periodic_inspections")]
    public class EquipmentPeriodicInspectionEntity : BaseEntity
    {
        public int EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public virtual EquipmentEntity Equipment { get; set; }
        public int PeriodicInspectionId { get; set; }
        [ForeignKey("PeriodicInspectionId")]
        public virtual PeriodicInspectionEntity PeriodicInspection { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool Active { get; set; }
    }
}
