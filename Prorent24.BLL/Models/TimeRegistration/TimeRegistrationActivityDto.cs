using Prorent24.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.TimeRegistration
{
    public class TimeRegistrationActivityDto : BaseDto<int>
    {
        [IncludeToGrid]
        public string Name { get; set; }
        [IncludeToGrid]
        public int Duration { get; set; }
        public string CrewMemberId { get; set; }
    }
}
