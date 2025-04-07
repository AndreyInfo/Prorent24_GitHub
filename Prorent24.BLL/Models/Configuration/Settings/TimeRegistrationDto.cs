using Prorent24.Enum.General;

namespace Prorent24.BLL.Models.Configuration.Settings
{
    public class TimeRegistrationSettingsDto
    {
        public int DaysBackNumber { get; set; }
        public int DefaultBrake { get; set; }
        public TimeUnitTypeEnum FrequencyUnitType { get; set; }
    }
}
