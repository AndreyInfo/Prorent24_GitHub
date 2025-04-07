using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Project;
using Prorent24.WebApp.Models.Configuration.Financials.Vat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Project
{
    public class ProjectFunctionViewModel
    {
        public int Id { get; set; }

        public int? ParentFunctionId { get; set; }

        public int? ProjectId { get; set; }

        public int? GroupId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectFunctionTypeEnum Type { get; set; }

        public string ExternalName { get; set; }

        public string InternalName { get; set; }

        public int TimeBefore { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TimeTypeEnum TimeBeforeType { get; set; }

        public int TimeAfter { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TimeTypeEnum TimeAfterType { get; set; }

        public bool TakeTimeFromLocation { get; set; }

        public int Duration { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TimeTypeEnum DurationType { get; set; }

        public int Break { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TimeTypeEnum BreakType { get; set; }

        public decimal RentalHourRate { get; set; }

        public decimal RentalFixed { get; set; }

        public decimal SubhireHourRate { get; set; }

        public decimal SubhireFixed { get; set; }

        public int? VatClassId { get; set; }

        public VatClassViewModel VatClass { get; set; }

        public bool IncludeInPrice { get; set; }

        public bool ShowInPlaner { get; set; }

        public string CustomerRemark { get; set; }

        public string PlannerRemark { get; set; }

        public string CrewMemberRemark { get; set; }

        public int? Quantity { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EntryTimeTypeEnum? PlanFromTimeType { get; set; }

        public DateTime? PlanFrom { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EntryTimeTypeEnum? PlanUntilTimeType { get; set; }

        public DateTime? PlanUntil { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EntryTimeTypeEnum? UseFromTimeType { get; set; }

        public DateTime? UseFrom { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EntryTimeTypeEnum? UseUntilTimeType { get; set; }

        public DateTime? UseUntil { get; set; }

        public int? TimeFrameId { get; set; }

        public ProjectTimeViewModel ProjectTime { get; set; }

        public decimal Distance { get; set; }

        public decimal? TotalIncVat { get; set; }
    }
}
