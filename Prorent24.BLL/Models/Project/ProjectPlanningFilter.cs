using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Project
{
    public class ProjectPlanningFilter
    {
        public ProjectFunctionTypeEnum Type { get; set; }

        public int? FunctionGroupId { get; set; }

        public int ProjectId { get; set; }
    }
}
