using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.CrewPlanner;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Common.Models.WorkPanel
{
    public class WorkPanelTimeLineModel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public CrewPlannerActionType Action { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
    }
}
