using Prorent24.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Subhire
{
    [Table("dbo_subhire_equipment_groups")]
    public class SubhireEquipmentGroupEntity : BaseEntity
    {
        public int SubhireId { get; set; }

        [ForeignKey("SubhireId")]
        public virtual SubhireEntity Subhire { get; set; }

        public string Name { get; set; }

        //public decimal TotalPrice { get; set; }

        public ICollection<SubhireEquipmentEntity> Equipments { get; set; }
    }
}
