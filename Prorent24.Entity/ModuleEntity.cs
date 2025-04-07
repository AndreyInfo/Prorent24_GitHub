using Prorent24.Entity.Base;
using Prorent24.Enum.General;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity
{
    [Table("sys_modules")]
    public class ModuleEntity : BaseEntity
    {
        public ModuleTypeEnum ModuleType { get; set; }
        public string Name { get; set; }
        public short Order { get; set; }
    }
}
