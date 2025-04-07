using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Project;
using Prorent24.WebApp.Models.CrewMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Project
{
    public class ProjectPlanningViewModel
    {
        public int Id { get; set; }

        public int FunctionId { get; set; }

        public string FunctionName { get; set; }

        //public int FunctionQuantity { get; set; }
        //public int Quantity { get; set; }
        //public DateTime FunctionGroupStartPlan { get; set; }
        //public int FunctionTimeBefore { get; set; }
        //public int FunctionTimeAfter { get; set; }
        //public string FunctionPlannerRemark { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectFunctionTypeEnum FunctionType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectFunctionTypeEnum Type { get; set; }

        public string EntityId { get; set; }

        public string Name { get; set;}

        public bool VisibleToCrewMember { get; set; } = true;

        public bool? ProjectLeader { get; set; } = false;

        //вирішила рознести структуру, коли вибираєш CrewRate - то далі вибираєш який саме, якщо EstimatedCosts - то вносиш Costs
        [JsonConverter(typeof(StringEnumConverter))]
        public PlanningCrewMemberRateEnum? RateType { get; set; } = PlanningCrewMemberRateEnum.CrewRate;

        public int? CrewMemberRateId { get; set; }

        public CrewMemberRateViewModel CrewMemberRate { get; set; }

        public decimal? Costs { get; set; }

        public decimal? PlannedCosts { get; set; }

        public decimal? ActualCosts { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PlanningTransportTypeEnum TransportType { get; set; } = PlanningTransportTypeEnum.NoTransport; // for crew - TravelTime, for vehicle - Transport

        public DateTime? PlanFrom { get; set; }

        public DateTime? PlanUntil { get; set; }

        public string Remark { get; set; }

    }
}
