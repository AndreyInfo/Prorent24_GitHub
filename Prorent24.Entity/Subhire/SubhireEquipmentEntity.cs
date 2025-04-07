using Prorent24.Entity.Base;
using Prorent24.Entity.Equipment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Subhire
{
    [Table("dbo_subhire_equipments")]
    public class SubhireEquipmentEntity : BaseEntity
    {
        public int? SubhireId { get; set; }

        [ForeignKey("SubhireId")]
        public virtual SubhireEntity Subhire { get; set; }
        public int SubhireEquipmentGroupId { get; set; }

        [ForeignKey("SubhireEquipmentGroupId")]
        public virtual SubhireEquipmentGroupEntity SubhireEquipmentGroup { get; set; }

        public int EquipmentId { get; set; }

        [ForeignKey("EquipmentId")]
        public virtual EquipmentEntity Equipment { get; set; }

        public int? ParentId { get; set; } // for equipment kit type

        [ForeignKey("ParentId")]
        public virtual SubhireEquipmentEntity ParentSubhireEquipment { get; set; }

        [InverseProperty("ParentSubhireEquipment")]
        public ICollection<SubhireEquipmentEntity> Children { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal Factor { get; set; }

        public string Remark { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
