using Prorent24.Entity.Base;
using Prorent24.Enum.General;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.General
{
    [Table("dbo_saved_filters")]
    public class SavedFilterEntity : BaseEntity
    {
        public ModuleTypeEnum ModuleType { get; set; }
        public FilterGroupTypeEnum FilterGroupType { get; set; }
        public FilterTypeEnum FilterType { get; set; }
        public string Text { get; set; }
        public string Value { get; set; } //Json
    }
}
