using Prorent24.Entity.Base;
using Prorent24.Entity.Contact;
using Prorent24.Entity.CrewMember;
using Prorent24.Entity.General;
using Prorent24.Entity.Project;
using Prorent24.Entity.Tasks;
using Prorent24.Enum.Subhire;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Subhire
{
    [Table("dbo_subhires")]
    public class SubhireEntity : BaseEntity
    {
        public string Name { get; set; }

        public string Number { get; set; }

        public SubhireStatusEnum Status { get; set; }

        public int? SupplierContactId { get; set; }

        [ForeignKey("SupplierContactId")]
        public virtual ContactEntity SupplierContact { get; set; }

        public int? SupplierContactPersonId { get; set; }

        [ForeignKey("SupplierContactPersonId")]
        public virtual ContactPersonEntity SupplierContactPerson { get; set; }

        public int? LocationContactId { get; set; }

        [ForeignKey("LocationContactId")]
        public virtual ContactEntity LocationContact { get; set; }

        public int? LocationContactPersonId { get; set; }

        [ForeignKey("LocationContactPersonId")]
        public virtual ContactPersonEntity LocationContactPerson { get; set; }

        #region Details
        public string AccountManagerId { get; set; }

        [ForeignKey("AccountManagerId")]
        public CrewMemberEntity AccountManager { get; set; }

        public string Reference { get; set; }

        public decimal EquipmentCost { get; set; }

        public decimal AdditionalCost { get; set; }

        public decimal TotalCost { get; set; }

        public SubhireTypeEnum Type { get; set; }

        public string Remark { get; set; }
        #endregion

        #region TimeSchedule
        [Column(TypeName = "DATETIME")]
        public DateTime? UsagePeriodStart { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? UsagePeriodEnd { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime PlanningPeriodStart { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime PlanningPeriodEnd { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? DeliveryCollectionStart { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? DeliveryCollectionEnd { get; set; }
        #endregion

        // 
        public virtual ICollection<NoteEntity> Notes { get; set; }
        public virtual ICollection<TagBondEntity> Tags { get; set; }
        public virtual ICollection<TaskEntity> Tasks { get; set; }
        public virtual ICollection<FileEntity> Files { get; set; }

        public virtual ICollection<SubhireProjectEntity> Projects { get; set; }

    }
}
