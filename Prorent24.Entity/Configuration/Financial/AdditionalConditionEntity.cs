using Prorent24.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Configuration.Financial
{
    [Table("sys_additional_conditions")]
    public class AdditionalConditionEntity : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public string Text { get; set; }
    }
}
