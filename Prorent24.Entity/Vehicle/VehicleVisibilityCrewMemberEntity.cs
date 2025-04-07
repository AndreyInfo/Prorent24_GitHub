using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Vehicle
{
    [Table("dbo_vehicle_visibility_crew_members")]
    public class VehicleVisibilityCrewMemberEntity
    {
        [Key]
        public virtual int Id { get; set; }
        public string CrewMemberId { get; set; }
        [ForeignKey("CrewMemberId")]
        public User CrewMember { get; set; }

        public int VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        public virtual VehicleEntity Vehicle { get; set; }
    }

}
