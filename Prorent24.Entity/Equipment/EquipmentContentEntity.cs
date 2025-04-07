using Prorent24.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Equipment
{
    [Table("dbo_equipment_contents")]
    public class EquipmentContentEntity : BaseEntity
    {
        public int EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public virtual EquipmentEntity Equipment { get; set; }
        public int ContentId { get; set; }
        [ForeignKey("ContentId")]
        public EquipmentEntity Content { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool UnfoldFinancialDocuments { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool UnfoldPackingSlip { get; set; }
    }
}
