using Prorent24.Entity.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Prorent24.Enum.General;
using Prorent24.Enum.TimeRegistration;
using Prorent24.Entity.CrewMember;
using System.Collections.Generic;
using Prorent24.Entity.Tasks;

namespace Prorent24.Entity.TimeRegistration
{
    [Table("dbo_time_registrations")]
    public class TimeRegistrationEntity: BaseEntity
    {
        public string Name { get; set; }

        public string CrewMemberId { get; set; }
        [ForeignKey("CrewMemberId")]
        public virtual CrewMemberEntity CrewMember { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime From { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime Until { get; set; }

        [Column(TypeName = "DECIMAL")]
        public decimal? Distance { get; set; }
        [Column(TypeName = "INTEGER")]
        public int BreakDuration { get; set; }
        public TimeUnitTypeEnum BreakTimeUnit { get; set; }

        public HourRegistrationTypeEnum HourRegistrationType { get; set; }

        [Column(TypeName = "INTEGER")]
        public int TravelDuration { get; set; }
        public TimeUnitTypeEnum TravelTimeUnit { get; set; }

        [Column(TypeName = "BOOLEAN")]
        public bool Lunch { get; set; }

        public string Remark { get; set; }

        public virtual List<TimeRegistrationActivityEntity> Activities { get; set; }
    }
}
