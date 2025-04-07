using Prorent24.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Configuration.Financial
{
    [Table("sys_discount_groups")]
    public class DiscountGroupEntity : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
        
    }
}
