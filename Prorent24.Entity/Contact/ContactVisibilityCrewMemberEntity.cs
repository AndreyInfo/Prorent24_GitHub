using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Contact
{
    [Table("dbo_contact_visibility_crew_members")]
    public class ContactVisibilityCrewMemberEntity
    {
        [Key]
        public virtual int Id { get; set; }

        public string CrewMemberId { get; set; }

        [ForeignKey("CrewMemberId")]
        public User CrewMember { get; set; }

        public int ContactId { get; set; }
        [ForeignKey("ContactId")]
        public virtual ContactEntity Contact { get; set; }
    }
}
