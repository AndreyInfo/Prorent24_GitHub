using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Project
{
    public class ProjectPlanningDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5)]
        public int FunctionId { get; set; }

        [IncludeToGrid(Order = 6)]
        public string FunctionName { get; set; }
      
        public ProjectFunctionDto ProjectFunction { get; set; }
        //public int FunctionQuantity { get; set; }
        //public int Quantity { get; set; }
        //public DateTime FunctionGroupStartPlan { get; set; }
        //public int FunctionTimeBefore { get; set; }
        //public int FunctionTimeAfter { get; set; }
        //public string FunctionPlannerRemark { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectFunctionTypeEnum Type { get; set; }

        [IncludeToGrid(Order = 7)]
        public string EntityId { get; set; }

        [IncludeToGrid(Order = 8)]
        public string EntityName { get; set; }

        [IncludeToGrid(Order = 9)]
        public bool VisibleToCrewMember { get; set; }

        [IncludeToGrid(Order = 10)]
        public bool? ProjectLeader { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PlanningCrewMemberRateEnum? RateType { get; set; }

        public int? CrewMemberRateId { get; set; }

        public CrewMemberRateDto CrewMemberRate { get; set; }

        public decimal? CrewMemberHourlyRate { get; set; }


        public decimal? Costs { get; set; }

        public decimal? PlannedCosts { get; set; }

        public decimal? ActualCosts { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PlanningTransportTypeEnum TransportType { get; set; } // for crew - TravelTime, for vehicle - Transport

        public DateTime? PlanFrom { get; set; }
  
        public DateTime? PlanUntil { get; set; }

        public string Remark { get; set; }
    }
}
