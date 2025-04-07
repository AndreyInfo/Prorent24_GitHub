using Prorent24.Entity.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Configuration.Financial
{
    [Table("sys_factor_groups")]
    public class FactorGroupEntity : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "BOOLEAN")]
        public bool IsSystem { get; set; }

        public virtual List<FactorGroupOptionEntity> FactorGroupOptions { get; set; }

    }
}
