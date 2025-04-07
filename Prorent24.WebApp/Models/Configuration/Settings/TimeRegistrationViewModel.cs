using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.General;

namespace Prorent24.WebApp.Models.Configuration.Settings
{
    public class TimeRegistrationSettingsViewModel
    {
        public int DaysBackNumber { get; set; }
        public int DefaultBrake { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TimeUnitTypeEnum FrequencyUnitType { get; set; }
    }
}
