using Prorent24.Entity.Base;
using Prorent24.Enum.General;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.General
{
    [Table("dbo_presets")]
    public class PresetEntity : BaseEntity
    {
        public ModuleTypeEnum ModuleType { get; set; }
        public string Name { get; set; }
        public string Filters { get; set; }
    }
}
