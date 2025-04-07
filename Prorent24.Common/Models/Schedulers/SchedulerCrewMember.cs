using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Common.Models.Schedulers
{
    public class SchedulerCrewMember
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Subject { get; set; }
        public string Calendar { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
