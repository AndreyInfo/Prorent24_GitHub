using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.CrewPlanner;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;

namespace Prorent24.WebApp.Models.CrewPlanner
{
    public class CrewPlannerViewModel
    {
        public int Id { get; set; }
         
        public List<string> FunctionIds { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectFunctionTypeEnum Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CrewPlannerActionType Action { get; set; }

        public DateTime From { get; set; }

        public DateTime Until { get; set; }

        public string Comment { get; set; }
    }
}
