using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
    public class ProjectPlanningGridDto : BaseDto<int>
    {
        public int? ParentId { get; set; }

        public ProjectFunctionDto ProjectFunction { get; set; }

        [IncludeToGrid(Order = 5, IsDisplay = false)]
        public int? FunctionId => ProjectFunction?.Id;

        [IncludeToGrid(Order = 6, IsDisplay = false)]
        public string FunctionName => ProjectFunction?.InternalName;

        [IncludeToGrid(Order = 7)]
        public string Name
        {
            get
            {
                string _name = "";
                if (ParentId.HasValue)
                {
                    switch (FunctionType)
                    {
                        case ProjectFunctionTypeEnum.Crew:
                            _name = this.EntityName;
                            break;
                        case ProjectFunctionTypeEnum.Transport:
                            _name = this.EntityName;
                            break;
                        default:
                            _name = this.EntityName;
                            break;
                    }
                }
                else
                {
                    _name = ProjectFunction?.InternalName;
                }

                return _name;
            }
        }

        [IncludeToGrid(Order = 8, IsDisplay = false, KeyType = "string")]
        public int Quantity { get; set; }

        [IncludeToGrid(Order = 9, IsDisplay = false)]
        public string EntityId { get; set; }

        [IncludeToGrid(Order = 10, IsDisplay = false)]
        public string EntityName { get; set; }

        [IncludeToGrid(Order = 11, IsDisplay = true)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectFunctionTypeEnum? FunctionType { get; set; } 

        [IncludeToGrid(Order = 12, IsDisplay = false)]
        public DateTime? FunctionPlanFrom => ProjectFunction?.PlanFrom;

        [IncludeToGrid(Order = 13, IsDisplay = false)]
        public DateTime? FunctionPlanUntil => ProjectFunction?.PlanUntil;

        [IncludeToGrid(Order = 14, IsDisplay = false, KeyType = "string")]
        public int? FunctionQuantity { get; set; }

        [IncludeToGrid(Order = 15, KeyType = "date")]
        public DateTime? PlanFrom { get; set; }
        [IncludeToGrid(Order = 16, KeyType = "date")]
        public DateTime? PlanUntil { get; set; }

        [IncludeToGrid(Order = 17, KeyType = "yes_or_no")]
        public bool? VisibleToCrewMember { get; set; }

        [IncludeToGrid(Order = 18, KeyType = "yes_or_no")]
        public bool? ProjectLeader { get; set; }

        [IncludeToGrid(Order = 19)]
        public string Remark { get; set; }

        [IncludeToGrid(Order = 20, IsEntity = true, EntityKey = "childrens", IsDisplay = false)]
        public List<ProjectPlanningGridDto> Childrens { get; set; } = new List<ProjectPlanningGridDto>();

        [IncludeToGrid(Order = 21, IsDisplay = false)]
        public bool IsAvailability { get; set; } = true;

    }
}
