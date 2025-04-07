using Prorent24.Entity.Base;
using Prorent24.Entity.Contact;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Equipment
{
    [Table("dbo_equipment_suppliers")]
    public class EquipmentSupplierEntity : BaseEntity
    {
        public int EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public virtual EquipmentEntity Equipment { get; set; }
        public decimal Price { get; set; }
        public string Details { get; set; }
        public int ContactId { get; set; }
        [ForeignKey("ContactId")]
        public virtual ContactEntity Contact { get; set; }
    }
}
