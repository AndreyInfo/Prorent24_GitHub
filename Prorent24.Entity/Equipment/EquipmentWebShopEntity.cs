using Prorent24.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Equipment
{
    [Table("dbo_equipment_webshop")]
    public class EquipmentWebShopEntity : BaseEntity
    {
        public int EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public virtual EquipmentEntity Equipment { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool InWebshop { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string SEOTitle { get; set; }
        public string SEOKeyword { get; set; }
        public string SEODescription { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool FeaturedProduct { get; set; }
    }
}
