using Prorent24.Entity.Base;
using Prorent24.Entity.Equipment;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Maintenance
{
    [Table("dbo_inspection_serial_numbers")]
    public class InspectionSerialNumberEntity : BaseEntity
    {
        public int InspectionId { get; set; }
        [ForeignKey("InspectionId")]
        public virtual InspectionEntity Inspection { get; set; }
        public int EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public virtual EquipmentEntity Equipment { get; set; }
        public int SerialNumberId { get; set; }
        [ForeignKey("SerialNumberId")]
        public virtual EquipmentSerialNumberEntity SerialNumber { get; set; }
    }
}
