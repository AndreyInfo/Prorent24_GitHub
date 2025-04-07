using Prorent24.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Project
{
    [Table("dbo_project_equipment_groups")]
    public class ProjectEquipmentGroupEntity : BaseEntity
    {
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual ProjectEntity Project { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? StartPlanPeriod { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? EndPlanPeriod { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? StartUsePeriod { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? EndUsePeriod { get; set; }

        public ICollection<ProjectEquipmentEntity> Equipments { get; set; }
    }
}
