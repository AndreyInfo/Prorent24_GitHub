using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Directory;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.WebApp.Models.General.Period
{
    public class PeriodViewModel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public PeriodTypeEnum PeriodType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DurationTimeEnum DurationTime { get; set; }

        public int Duration { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TimeUnitTypeEnum TimeUnit { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
