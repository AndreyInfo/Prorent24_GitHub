using Prorent24.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Equipment
{
    [Table("dbo_equipment_serial_numbers")]
    public class EquipmentSerialNumberEntity : BaseEntity
    {
        public int EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public virtual EquipmentEntity Equipment { get; set; } 
        public string SerialNumber { get; set; }
        public string InternalReference { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime? PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool CalculateBookValueAutomatically { get; set; }
        // CalculateBookValueAutomatically == true
        public decimal DepreciationPerMonth { get; set; }
        // CalculateBookValueAutomatically == false
        public decimal BookValue { get; set; }
        public string Remark { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool Active { get; set; }
        public string SuppliersInfo { get; set; }
    }
}
