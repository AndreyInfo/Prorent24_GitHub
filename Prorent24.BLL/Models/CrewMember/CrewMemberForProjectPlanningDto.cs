using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.CrewMember
{
    public class CrewMemberForProjectPlanningDto
    {
        [IncludeToGrid(Order = 1, IsDisplay = false, IsKey = true, KeyType = "string")]
        public string Id { get; set; }

        [IncludeToGrid(Order = 2)]
        public string Name { get; set; }

        [IncludeToGrid(Order = 3)]
        public string FolderName { get; set; }

        [IncludeToGrid(Order = 4, IsDisplay = false)]
        public List<ProjectPlannindPeriod> ProjectPeriods { get; set; }

    }

    public class ProjectPlannindPeriod
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectStatusEnum Status { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}
