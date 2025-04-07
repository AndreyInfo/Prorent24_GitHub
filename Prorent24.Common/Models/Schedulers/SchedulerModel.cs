using System;

namespace Prorent24.Common.Models.Schedulers
{
    public class SchedulerModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Background { get; set; }
        public bool IsAvailable { get; set; }
        public string Comment { get; set; }
    }
}
