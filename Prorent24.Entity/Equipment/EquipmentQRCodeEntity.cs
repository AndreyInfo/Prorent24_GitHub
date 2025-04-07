using Prorent24.Entity.Base;
using Prorent24.Entity.Equipment;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Equipment
{
    [Table("dbo_equipment_qr_codes")]
    public class EquipmentQRCodeEntity : BaseEntity
    {
        public string Code { get; set; }
        public string BarCode { get; set; }
        public int? EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public virtual EquipmentEntity Equipment { get; set; }
        public int? SerialNumberId { get; set; }
        [ForeignKey("SerialNumberId")]
        public virtual EquipmentSerialNumberEntity SerialNumber { get; set; }
    }
}
