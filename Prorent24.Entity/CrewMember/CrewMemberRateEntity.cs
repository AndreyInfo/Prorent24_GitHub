using Prorent24.Entity.Base;
using Prorent24.Enum.General;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.CrewMember
{
    [Table("dbo_crew_member_rates")]
    public class CrewMemberRateEntity : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public string CrewMemberId { get; set; }

        //[InverseProperty("DefaultRate")]
        [ForeignKey("CrewMemberId")]
        public virtual User User { get; set; }

        public decimal HourlyRate { get; set; }

        public decimal DailyRate { get; set; }

        public int MaxNumberOfTimeUnit { get; set; }

        public TimeUnitTypeEnum TimeUnit { get; set; }
    }
}
