using Prorent24.Entity.Base;
using Prorent24.Entity.CrewMember;
using Prorent24.Entity.Vehicle;
using Prorent24.Enum.CrewPlanner;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.CrewPlanner
{
    [Table("dbo_crew_planners")]
    public class CrewPlannerEntity : BaseEntity
    {
        public string CrewMemberId { get; set; }

        [ForeignKey("CrewMemberId")]
        public virtual CrewMemberEntity CrewMember { get; set; }

        public int? VehicleId { get; set; }

        [ForeignKey("VehicleId")]
        public virtual VehicleEntity Vehicle { get; set; }

        public CrewPlannerActionType Action { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime From { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime Until { get; set; }

        public string Comment { get; set; }
    }
}
