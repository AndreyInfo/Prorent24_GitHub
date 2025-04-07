using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Project
{
    [GridOptions(HierarchyRoot = "childrens")]
    public class ProjectFunctionDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5, IsDisplay =false)]
        public int? ProjectId { get; set; }

        [IncludeToGrid(Order = 6, IsDisplay = false)]
        public int? ParentFunctionId { get; set; }

        public int? ProjectFunctionGroupId { get; set; }

        [IncludeToGrid(Order = 7)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectFunctionTypeEnum Type { get; set; }

        public string ExternalName { get; set; }

        [IncludeToGrid(Order = 8)]
        public string InternalName { get; set; }

        public int TimeBeforeInMinutes { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TimeTypeEnum TimeBeforeType { get; set; }

        public int TimeAfterInMinutes { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TimeTypeEnum TimeAfterType { get; set; }

        public bool TakeTimeFromLocation { get; set; }

        public int DurationInMinutes { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]

        public TimeTypeEnum DurationType { get; set; }

        public int BreakInMinutes { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TimeTypeEnum BreakType { get; set; }

        public decimal RentalHourRate { get; set; }

        public decimal RentalFixed { get; set; }

        public decimal SubhireHourRate { get; set; }

        public decimal SubhireFixed { get; set; }

        public int? VatClassId { get; set; }

        public VatClassDto VatClass { get; set; }

       // [IncludeToGrid]
        public bool IncludeInPrice { get; set; }

       // [IncludeToGrid]
        public bool ShowInPlaner { get; set; }

        public string CustomerRemark { get; set; }

        public string PlannerRemark { get; set; }

        public string CrewMemberRemark { get; set; }

        [IncludeToGrid(Order = 9)]
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
        
        public ProjectTimeDto ProjectTime { get; set; }

        [IncludeToGrid(Order = 10)]
        public decimal TotalCosts { get; set; }

        [IncludeToGrid(Order = 11)]
        public decimal TotalPrice { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectFunctionAddTimeEnum GlobalAddTimeType { get; set; }

        public decimal Distance { get; set; } = 0;

        [IncludeToGrid(Order = 12, IsEntity = true, EntityKey = "childrens", IsDisplay = false)]
        public List<ProjectPlanningDto> Childrens { get; set; } = new List<ProjectPlanningDto>();

        #region ADDITIONAL FIELDS FOR GRID

        [IncludeToGrid(Order = 13)]
        public string PeriodOfUse
        {
            get
            {
                return string.Concat(UseFrom.HasValue ? UseFrom.Value.ToShortDateString() : "---", " - ", UseUntil.HasValue ? UseUntil.Value.ToShortDateString() : "---");
            }
        }

        #endregion

        public decimal? TotalIncVat { get; set; }
    }
}
