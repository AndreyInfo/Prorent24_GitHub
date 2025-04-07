using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.DAL.Models.Project
{
    public class ProjectPlanningFilterModel
    {
        public ProjectFunctionTypeEnum Type { get; set; }

        public int? FunctionGroupId { get; set; }

        public int ProjectId { get; set; }
    }
}
