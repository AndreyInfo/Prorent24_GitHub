using Prorent24.Enum.Directory;
using Prorent24.Enum.General;
using System;

namespace Prorent24.BLL.Models.General.Period
{
    public class PeriodDto
    {
        public PeriodTypeEnum PeriodType { get; set; }
        public DurationTimeEnum DurationTime { get; set; }
        public int Duration { get; set; }
        public TimeUnitTypeEnum TimeUnit { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
