using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Entity.Equipment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Project
{
    [Table("dbo_project_equipments")]
    public class ProjectEquipmentEntity : BaseEntity
    {
        public int? ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual ProjectEntity Project  {get;set;}

        public int ProjectEquipmentGroupId { get; set; }

        [ForeignKey("ProjectEquipmentGroupId")]
        public virtual ProjectEquipmentGroupEntity ProjectEquipmentGroup { get; set; }

        public int? EquipmentId { get; set; }

        [ForeignKey("EquipmentId")]
        public virtual EquipmentEntity Equipment { get; set; }

        public int? ParentId { get; set; } // for equipment kit type

        [ForeignKey("ParentId")]
        public virtual ProjectEquipmentEntity ParentProjectEquipment { get; set; }

        [InverseProperty("ParentProjectEquipment")]
        public ICollection<ProjectEquipmentEntity> Children { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal Factor { get; set; }

        public string Remark { get; set; }

        public decimal TotalPrice { get; set; }

        public int? VatClassId { get; set; }

        [ForeignKey("VatClassId")]
        public virtual VatClassEntity VatClass { get; set; }
    }
}
