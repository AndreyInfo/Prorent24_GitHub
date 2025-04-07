using Prorent24.Common.Attributes;
using System;

namespace Prorent24.BLL.Models.Project
{
    public class ProjectFunctionGroupDto : BaseDto<int>
    {
        public int? ProjectId { get; set; }
        public int? SubprojectId { get; set; }

        [IncludeToGrid(Order = 5, IsDisplay = true)]
        public string Name { get; set; }


        public DateTime? PlanFrom { get; set; }

        public DateTime? PlanUntil { get; set; }


        public DateTime? UseFrom { get; set; }

        public DateTime? UseUntil { get; set; }

    }
}
