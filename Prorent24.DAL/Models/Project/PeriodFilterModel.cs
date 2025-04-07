using Prorent24.Enum.Directory;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.DAL.Models.Project
{
    public class PeriodFilterModel
    {
        public PeriodTypeEnum PeriodType { get; set; }
        public DurationTimeEnum DurationTime { get; set; }
        public int Duration { get; set; }
        public TimeUnitTypeEnum TimeUnit { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
