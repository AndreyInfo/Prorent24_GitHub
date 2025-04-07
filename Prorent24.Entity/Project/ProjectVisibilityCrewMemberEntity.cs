using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Project
{
    [Table("dbo_project_visibility_crew_members")]
    public class ProjectVisibilityCrewMemberEntity
    {
        [Key]
        public virtual int Id { get; set; }

        public string CrewMemberId { get; set; }

        [ForeignKey("CrewMemberId")]
        public User CrewMember { get; set; }

        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual ProjectEntity Project { get; set; }
    }
}
