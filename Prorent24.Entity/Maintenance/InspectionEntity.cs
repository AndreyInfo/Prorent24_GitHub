using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration.Settings;
using Prorent24.Entity.General;
using Prorent24.Entity.Tasks;
using Prorent24.Enum.Maintenance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Maintenance
{
    [Table("dbo_inspections")]
    public class InspectionEntity: BaseEntity
    {
        public InspectionStatusEnum Status { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int? PeriodicInspectionId { get; set; }
        [ForeignKey("PeriodicInspectionId")]
        public virtual PeriodicInspectionEntity PeriodicInspection { get; set; }

        public virtual ICollection<NoteEntity> Notes { get; set; }
        public virtual ICollection<TagBondEntity> Tags { get; set; }
        public virtual ICollection<TaskEntity> Tasks { get; set; }
        public virtual ICollection<FileEntity> Files { get; set; }
    }
}
