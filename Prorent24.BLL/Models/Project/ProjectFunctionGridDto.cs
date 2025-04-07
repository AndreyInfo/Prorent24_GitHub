using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Project
{
    [GridOptions(HierarchyRoot = "childrens")]
    public class ProjectFunctionGridDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5, IsDisplay = false)]
        public int? ProjectId { get; set; }

        [IncludeToGrid(Order = 6, IsDisplay = false)]
        public int? ParentFunctionId { get; set; }
        
        [IncludeToGrid(Order = 7, IsDisplay = false)]
        public int GroupId { get; set; }

        [IncludeToGrid(Order = 8)]
        public string GroupName { get; set; }
        
        [IncludeToGrid(Order = 9)]
        public string PeriodOfUse
        {
            get
            {
                return string.Concat(UseFrom.HasValue ? UseFrom.Value.ToString() : "---", " - ", UseUntil.HasValue ? UseUntil.Value.ToString() : "---");
            }
        }
        
        [IncludeToGrid(Order = 10)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectFunctionTypeEnum? Type { get; set; }
        
        [IncludeToGrid(Order = 11, IsDisplay = false)]
        public int FunctionId { get; set; }
        [IncludeToGrid(Order = 12)]
        public string FunctionName { get; set; }

        [IncludeToGrid(Order = 13)]
        public bool? IncludeInPrice { get; set; }

        [IncludeToGrid(Order = 14)]
        public bool? ShowInPlaner { get; set; }

        [IncludeToGrid(Order = 15)]
        public int? Quantity { get; set; }

        [IncludeToGrid(Order = 16, IsDisplay = false)]
        public DateTime? UseFrom { get; set; }

        [IncludeToGrid(Order = 17, IsDisplay = false)]
        public DateTime? UseUntil { get; set; }

        [IncludeToGrid(Order = 18, IsDisplay = false)]
        public DateTime? PlanFrom { get; set; }

        [IncludeToGrid(Order = 19, IsDisplay = false)]
        public DateTime? PlanUntil { get; set; }

        [IncludeToGrid(Order = 20)]
        public decimal? TotalCosts { get; set; }

        [IncludeToGrid(Order = 21)]
        public decimal? TotalPrice { get; set; }

        [IncludeToGrid(Order = 22, IsSystem = true, IsEntity = true, EntityKey = "childrens", IsDisplay = false)]
        public List<ProjectFunctionGridDto> Childrens { get; set; } = new List<ProjectFunctionGridDto>();


        [IncludeToGrid(Order = 23)]
        public decimal RentalHourRate { get; set; }

        [IncludeToGrid(Order = 24)]
        public decimal RentalFixed { get; set; }

        [IncludeToGrid(Order = 25)]
        public decimal SubhireHourRate { get; set; }

        [IncludeToGrid(Order = 26)]
        public decimal SubhireFixed { get; set; }



    }

}
