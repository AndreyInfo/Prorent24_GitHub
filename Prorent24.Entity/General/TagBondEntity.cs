using Prorent24.Entity.Base;
using Prorent24.Entity.Contact;
using Prorent24.Entity.CrewMember;
using Prorent24.Entity.Equipment;
using Prorent24.Entity.Invoice;
using Prorent24.Entity.Maintenance;
using Prorent24.Entity.Project;
using Prorent24.Entity.Subhire;
using Prorent24.Entity.Tasks;
using Prorent24.Entity.Vehicle;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.General
{
    [Table("dbo_tag_bonds")]
    public class TagBondEntity : BaseEntity
    {
        public int TagDirectoryId { get; set; }

        [ForeignKey("TagDirectoryId")]
        public virtual TagDirectoryEntity TagDirectory { get; set; }

        public string CrewMemberId { get; set; }
        [ForeignKey("CrewMemberId")]
        public virtual CrewMemberEntity CrewMembers { get; set; }

        public int? ContactId { get; set; }
        [ForeignKey("ContactId")]
        public virtual ContactEntity Contact { get; set; }

        public int? VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        public virtual VehicleEntity Vehicle { get; set; }

        public int? TaskId { get; set; }
        [ForeignKey("TaskId")]
        public virtual TaskEntity Task { get; set; }

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

        public int? InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public virtual InvoiceEntity Invoice { get; set; }
    }
}
