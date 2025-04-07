using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Common.Attributes;
using Prorent24.Enum.General;

namespace Prorent24.BLL.Models.CrewMember
{
    public class CrewMemberRateDto
    {
        [IncludeToGrid(Order = 1, IsDisplay = false, IsKey = true)]
        public int Id { get; set; }

        public string CrewMemberId { get; set; }

        [IncludeToGrid(Order = 2)]
        public string Name { get; set; }

        [IncludeToGrid(Order = 3)]
        public decimal HourlyRate { get; set; }

        [IncludeToGrid(Order = 4)]
        public decimal DailyRate { get; set; }

        [IncludeToGrid(Order = 5, IsDisplay = false)]
        public int MaxNumberOfTimeUnit { get; set; }

        [IncludeToGrid(Order = 6, IsDisplay = false)]
        [JsonConverter(typeof(StringEnumConverter))]
        public TimeUnitTypeEnum TimeUnit { get; set; }

        public bool IsDefaultRate { get; set; }
    }
}
