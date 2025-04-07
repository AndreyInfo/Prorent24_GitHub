using Prorent24.Enum.General;

namespace Prorent24.BLL.Models.General.Filter
{
    public class SavedFilterDto : BaseDto<int>
    {
        public ModuleTypeEnum ModuleType { get; set; }
        public FilterGroupTypeEnum FilterGroupType { get; set; }
        public FilterTypeEnum FilterType { get; set; }
        public string Text { get; set; }
        public SavedFilterValueDto Value { get; set; }
    }
}
