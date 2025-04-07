using Prorent24.Entity.Base;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Project
{
    [Table("dbo_project_function_groups")]
    public class ProjectFunctionGroupEntity : BaseEntity
    {
        public string Name { get; set; }
        // project or subproject the same
        public int? SubprojectId { get; set; }
        public bool IsDuration { get; set; }
        public int Duration { get; set; } // in Minutes
        [Column(TypeName = "BOOLEAN")]
        public bool IsUsagePeriodDefined { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool IsPlanningPeriodDefined { get; set; }
        // Usage begins at the beginning of the use period

        [Column(TypeName = "BOOLEAN")]
        public bool UsageBeginsAtBeginningUsePeriod { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool UsageEndsAtEndingUsePeriod { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool UsageBeginsAtBeginningPlanPeriod { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool UsageEndsAtEndiingPlanPeriod { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? PlanFrom { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime? PlanUntil{ get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? UseFrom { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime? UseUntil { get; set; }

        public ProjectFunctionAddTimeEnum GlobalAddTimeType { get; set; }

        public int DurationInMinutes { get; set; }

        public TimeTypeEnum DurationType { get; set; }

    }
}
