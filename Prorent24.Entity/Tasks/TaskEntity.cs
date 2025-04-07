using Prorent24.Entity.Base;
using Prorent24.Entity.Contact;
using Prorent24.Entity.CrewMember;
using Prorent24.Entity.Equipment;
using Prorent24.Entity.General;
using Prorent24.Entity.Invoice;
using Prorent24.Entity.Maintenance;
using Prorent24.Entity.Project;
using Prorent24.Entity.Subhire;
using Prorent24.Entity.Vehicle;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Tasks
{
    [Table("dbo_tasks")]
    public class TaskEntity : BaseEntity
    {
        public string Task { get; set; }
        public DateTime DeadLine { get; set; }
        public bool IsPublic { get; set; }
        public string Description { get; set; }

        public DateTime? CompletedDate { get; set; }

        // public virtual ICollection<User> CrewMembers { get; set; }

        public string CompleatedBy { get; set; }
        [ForeignKey("CompleatedBy")]
        public virtual User CompleatedByMember { get; set; }

        public string AssignTo { get; set; }
        [ForeignKey("AssignTo")]
        public virtual User AssignToMember { get; set; }

        public ModuleTypeEnum BelongsTo { get; set; }

        public string CrewMemberId { get; set; }
        [ForeignKey("CrewMemberId")]
        public virtual CrewMemberEntity CrewMember { get; set; }

        public int? ContactId { get; set; }
        [ForeignKey("ContactId")]
        public virtual ContactEntity Contact { get; set; }

        public int? VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        public virtual VehicleEntity Vehicle { get; set; }

        public int? EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public virtual EquipmentEntity Equipment { get; set; }

        public int? InspectionId { get; set; }
        [ForeignKey("InspectionId")]
        public virtual InspectionEntity Inspection { get; set; }
        public int? RepairId { get; set; }
        [ForeignKey("RepairId")]
        public virtual RepairEntity Repair { get; set; }

        public int? SubhireId { get; set; }
        [ForeignKey("SubhireId")]
        public virtual SubhireEntity Subhire { get; set; }

        public int? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual ProjectEntity Project { get; set; }

        public int? InvoiceEntityId { get; set; }
        [ForeignKey("InvoiceEntityId")]
        public virtual InvoiceEntity Invoice { get; set; }

        public virtual ICollection<TaskEntity> Tasks { get; set; }
        public virtual ICollection<TagBondEntity> Tags { get; set; }
        public virtual ICollection<NoteEntity> Notes { get; set; }
        public virtual ICollection<FileEntity> Files { get; set; }
        // tasks visible for
        public virtual ICollection<TaskVisibilityCrewMemberEntity> CrewMembers { get; set; }

        public virtual ICollection<TaskExecutorCrewMemberEntity> Executors { get; set; }
    }
}
