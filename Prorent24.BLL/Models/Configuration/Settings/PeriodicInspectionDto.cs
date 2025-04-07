using Prorent24.Common.Attributes;
using Prorent24.Enum.General;

namespace Prorent24.BLL.Models.Configuration.Settings
{
    public class PeriodicInspectionDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5)]
        public string Name { get; set; }

        [IncludeToGrid(Order = 6)]
        public string Description { get; set; }

        [IncludeToGrid(Order = 7)]
        public int FrequencyInterval { get; set; }

        [IncludeToGrid(Order = 8)]
        public TimeUnitTypeEnum FrequencyUnitType { get; set; }
    }
}
