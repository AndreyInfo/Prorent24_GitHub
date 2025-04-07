using Prorent24.Entity.Base;
using Prorent24.Entity.CrewMember;
using Prorent24.Entity.Vehicle;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Project
{

    [Table("dbo_project_plannings")]
    public class ProjectPlanningEntity : BaseEntity
    {
        public int FunctionId { get; set; }

        [ForeignKey("FunctionId")]
        public virtual ProjectFunctionEntity Function { get; set; }

        public string CrewMemberId { get; set; }

        [ForeignKey("CrewMemberId")]
        public virtual CrewMemberEntity CrewMember { get; set; }

        public int? VehicleId { get; set; }

        [ForeignKey("VehicleId")]
        public virtual VehicleEntity Vehicle { get; set; }

        public bool VisibleToCrewMember { get; set; }

        public bool? ProjectLeader { get; set; }

        public PlanningCrewMemberRateEnum? RateType { get; set; }

        public int? CrewMemberRateId { get; set; }

        public virtual CrewMemberRateEntity CrewMemberRate { get; set; }

        public decimal? CrewMemberHourlyRate { get; set; }

        public decimal? Costs { get; set; }

        public decimal? PlannedCosts { get; set; }

        public decimal? ActualCosts { get; set; }

        public PlanningTransportTypeEnum TransportType { get; set; } // for crew - TravelTime, for vehicle - Transport

        [Column(TypeName = "DATETIME")]
        public DateTime? PlanFrom { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime? PlanUntil { get; set; }

        public string Remark { get; set; }

    }
}
