using Prorent24.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Configuration.Financial
{
    [Table("sys_factor_group_options")]
    public class FactorGroupOptionEntity:BaseEntity
    {
        public int FactorGroupId { get; set; }

        public FactorGroupEntity FactorGroup { get; set; }

        public int NumberOfDaysFrom { get; set; }

        public int NumberOfDaysTo { get; set; }

        public decimal Factor { get; set; }
    }
}
