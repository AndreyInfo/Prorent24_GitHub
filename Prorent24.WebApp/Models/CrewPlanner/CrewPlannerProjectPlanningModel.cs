using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.CrewPlanner
{
    public class CrewPlannerProjectPlanningModel
    {
        public int FunctionId { get; set; }

        public string FunctionName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectFunctionTypeEnum Type { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public List<PlanningEntityModel> PlanningEntities { get; set; }

    }

    public class PlanningEntityModel
    {
        public string EntityId { get; set; }
        public string Name { get; set; }

    }

}
