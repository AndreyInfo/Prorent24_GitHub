using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.General;

namespace Prorent24.WebApp.Models.Configuration.Settings
{
    public class PeriodicInspectionViewModel
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FrequencyInterval { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public TimeUnitTypeEnum FrequencyUnitType { get; set; }
    }
}
