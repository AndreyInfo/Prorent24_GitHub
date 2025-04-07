using Prorent24.BLL.Models.General.Filter;
using Prorent24.Common.Models.Filters;
using Prorent24.Enum.General;
using System.Collections.Generic;

namespace Prorent24.BLL.Models.General.Preset
{
    public class PresetDto : BaseDto<int>
    {
        public string Name { get; set; }
        public ModuleTypeEnum ModuleType { get; set; }
        public bool IsDefault { get; set; }
        public List<SelectedFilter> Filters { get; set; }
        public bool Selected { get; set; }
    }
}
