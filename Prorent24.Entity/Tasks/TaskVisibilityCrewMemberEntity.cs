using Prorent24.Entity.CrewMember;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Tasks
{
    [Table("dbo_task_visibility_crew_members")]
    public class TaskVisibilityCrewMemberEntity
    {
        //[Key]
        //public virtual int Id { get; set; }
        public string CrewMemberId { get; set; }
        [ForeignKey("CrewMemberId")]
        public User CrewMember { get; set; }

        public int TaskId { get; set; }
        [ForeignKey("TaskId")]
        public virtual TaskEntity Task { get; set; }
    }
}
