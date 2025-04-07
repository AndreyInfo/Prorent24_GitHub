using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.General;

namespace Prorent24.WebApp.Models.CrewMember
{
    public class CrewMemberRateViewModel
    {
        public int Id { get; set; }

        public string CrewMemberId { get; set; }

        public string Name { get; set; }

        public decimal HourlyRate { get; set; }

        public decimal DailyRate { get; set; }

        public int MaxNumberOfTimeUnit { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TimeUnitTypeEnum TimeUnit { get; set; }

        public bool IsDefaultRate { get; set; }
    }
}
