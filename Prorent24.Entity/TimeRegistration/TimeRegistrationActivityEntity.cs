using Prorent24.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.TimeRegistration
{
    [Table("dbo_time_registration_activities")]
    public class TimeRegistrationActivityEntity: BaseEntity
    {
        public string Name { get; set; }
        public int TimeRegistrationId { get; set; }
        [ForeignKey("TimeRegistrationId")]
        public virtual TimeRegistrationEntity TimeRegistration { get; set; }
        public int Duration { get; set; }
    }
}
