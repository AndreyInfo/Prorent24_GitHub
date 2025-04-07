using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Models.TimeRegistration;
using Prorent24.Enum.General;
using Prorent24.Enum.TimeRegistration;
using Prorent24.WebApp.Models.CrewMember;
using System;
using System.Collections.Generic;

namespace Prorent24.WebApp.Models.TimeRegistration
{
    /// <summary>
    /// Time registration view model
    /// </summary>
    public class TimeRegistrationViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CrewMemberId { get; set; }
        public string CrewMember { get; set; }

        public DateTime From { get; set; }

        public DateTime Until { get; set; }

        public decimal? Distance { get; set; }

        public int BreakDuration { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TimeUnitTypeEnum BreakTimeUnit { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HourRegistrationTypeEnum HourRegistrationType { get; set; }

        public int TravelDuration { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TimeUnitTypeEnum TravelTimeUnit { get; set; }

        public bool Lunch { get; set; }

        public string Remark { get; set; }

        public List<TimeRegistrationActivityViewModel> Activities { get; set; }

        public List<string> CrewMembers { get; set; }
    }
}
