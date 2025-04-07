using Prorent24.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Project
{
    [Table("dbo_project_times")]
    public class ProjectTimeEntity: BaseEntity
    {
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual ProjectEntity Project { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime From { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime Until { get; set; }

        public bool DisplayQuotation { get; set; }

        public bool DisplayContract { get; set; }

        public bool DisplayPackSlip { get; set; }

        public bool DefaultUsagePeriod { get; set; }

        public bool DefaultPlanPeriod { get; set; }
    }
}
