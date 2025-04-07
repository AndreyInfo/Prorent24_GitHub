using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration.Settings;
using Prorent24.Entity.Contact;
using Prorent24.Entity.CrewMember;
using Prorent24.Entity.General;
using Prorent24.Entity.Subhire;
using Prorent24.Entity.Tasks;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Project
{
    [Table("dbo_projects")]
    public class ProjectEntity : BaseEntity
    {
        public string Name { get; set; }

        public string Number { get; set; }

        public ProjectStatusEnum Status { get; set; }

        public int? ClientContactId { get; set; }

        [ForeignKey("ClientContactId")]
        public virtual ContactEntity ClientContact { get; set; }

        public int? ClientContactPersonId { get; set; }

        [ForeignKey("ClientContactPersonId")]
        public virtual ContactPersonEntity ClientContactPerson { get; set; }

        public int? LocationContactId { get; set; }

        [ForeignKey("LocationContactId")]
        public virtual ContactEntity LocationContact { get; set; }

        public int? LocationContactPersonId { get; set; }

        [ForeignKey("LocationContactPersonId")]
        public virtual ContactPersonEntity LocationContactPerson { get; set; }

        #region Details
        public int? TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual ProjectTypeEntity Type { get; set; }

        public string AccountManagerId { get; set; }

        [ForeignKey("AccountManagerId")]
        public virtual CrewMemberEntity CrewMember { get; set; }

        public string Color { get; set; }

        public string Reference { get; set; }
        #endregion
        
        public virtual ICollection<ProjectTimeEntity> Times { get; set; }
        
        public virtual ICollection<NoteEntity> Notes { get; set; }
        public virtual ICollection<TagBondEntity> Tags { get; set; }
        public virtual ICollection<TaskEntity> Tasks { get; set; }
        public virtual ICollection<FileEntity> Files { get; set; }

        //public virtual ICollection<SubhireEntity> Subhires { get; set; }

        public string Description { get; set; } 

        public bool? IsPublic { get; set; }
        // vehicle visible for
        public virtual ICollection<ProjectVisibilityCrewMemberEntity> CrewMembers { get; set; }
    }
}
