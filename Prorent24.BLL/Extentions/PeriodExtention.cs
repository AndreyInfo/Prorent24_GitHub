using Prorent24.BLL.Models.General.Period;
using Prorent24.Entity.Tasks;
using Prorent24.Entity.TimeRegistration;
using Prorent24.Enum.Directory;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Extentions
{
    public static class PeriodExtention
    {
        public static IQueryable<TaskEntity> FilterByPeriod(this IQueryable<TaskEntity> entities, PeriodDto period)
        {
            if (period.PeriodType == PeriodTypeEnum.Relative)
            {
                switch (period.DurationTime)
                {
                    case DurationTimeEnum.Today:
                        {
                            entities = entities.Where(x => x.DeadLine == DateTime.UtcNow);
                            break;
                        }
                    case DurationTimeEnum.Past:
                        {
                            DateTime date = DateTime.UtcNow;

                            if (period.TimeUnit == TimeUnitTypeEnum.Days)
                            {
                                date = date.AddDays(-period.Duration);
                            }
                            else if (period.TimeUnit == TimeUnitTypeEnum.Months)
                            {
                                date = date.AddMonths(-period.Duration);
                            }
                            if (period.TimeUnit == TimeUnitTypeEnum.Weeks)
                            {
                                date = date.AddDays(-period.Duration*7);
                            }
                            entities = entities.Where(x => x.DeadLine < DateTime.UtcNow && x.DeadLine > date);
                            break;
                        }
                    case DurationTimeEnum.Next:
                        {
                            DateTime date = DateTime.UtcNow;

                            if (period.TimeUnit == TimeUnitTypeEnum.Days)
                            {
                                date = date.AddDays(period.Duration);
                            }
                            else if (period.TimeUnit == TimeUnitTypeEnum.Months)
                            {
                                date = date.AddMonths(period.Duration);
                            }
                            if (period.TimeUnit == TimeUnitTypeEnum.Weeks)
                            {
                                date = date.AddDays(period.Duration*7);
                            }

                            entities = entities.Where(x => x.DeadLine > DateTime.UtcNow && x.DeadLine < date);
                            break;
                        }
                }
            }
            else if(period.PeriodType == PeriodTypeEnum.Date)
            {
                entities = entities.Where(x => x.DeadLine.Date == period.FromDate.Value.Date);
            }

            return entities;
        }

        public static IQueryable<TimeRegistrationEntity> FilterByPeriod(this IQueryable<TimeRegistrationEntity> entities, PeriodDto period)
        {
            if (period.PeriodType == PeriodTypeEnum.Period)
            {
                switch (period.DurationTime)
                {
                    case DurationTimeEnum.Today:
                        {
                            entities = entities.Where(x => x.CreationDate.Date == DateTime.UtcNow.Date);
                            break;
                        }
                    case DurationTimeEnum.Past:
                        {
                            DateTime date = DateTime.UtcNow;

                            if (period.TimeUnit == TimeUnitTypeEnum.Hours)
                            {
                                date = date.AddHours(-period.Duration);
                            }
                            else if (period.TimeUnit == TimeUnitTypeEnum.Days)
                            {
                                date = date.AddDays(-period.Duration);
                            }
                            else if (period.TimeUnit == TimeUnitTypeEnum.Months)
                            {
                                date = date.AddMonths(-period.Duration);
                            }
                            if (period.TimeUnit == TimeUnitTypeEnum.Weeks)
                            {
                                date = date.AddDays(-period.Duration*7);
                            }
                            entities = entities.Where(x => (x.From < DateTime.UtcNow && x.From > date) || (x.Until < DateTime.UtcNow && x.Until > date));
                            break;
                        }
                    case DurationTimeEnum.Next:
                        {
                            DateTime date = DateTime.UtcNow;

                            if (period.TimeUnit == TimeUnitTypeEnum.Hours)
                            {
                                date = date.AddHours(period.Duration);
                            }
                            else if (period.TimeUnit == TimeUnitTypeEnum.Days)
                            {
                                date = date.AddDays(period.Duration);
                            }
                            else if (period.TimeUnit == TimeUnitTypeEnum.Months)
                            {
                                date = date.AddMonths(period.Duration);
                            }
                            if (period.TimeUnit == TimeUnitTypeEnum.Weeks)
                            {
                                date = date.AddDays(period.Duration*7);
                            }

                            entities = entities.Where(x => (x.From > DateTime.UtcNow && x.From < date)|| (x.Until > DateTime.UtcNow && x.Until < date));
                            break;
                        }
                }
            }
            else if (period.PeriodType == PeriodTypeEnum.FromToUntil)
            {
                if (period.FromDate.HasValue && period.ToDate.HasValue)
                {
                    entities = entities.Where(x => x.From.Date >= period.FromDate.Value && x.Until.Date <= period.ToDate.Value);
                }
                else if (period.FromDate.HasValue)
                {
                    entities = entities.Where(x => x.From.Date >= period.FromDate.Value);
                }
                else if (period.ToDate.HasValue)
                {
                    entities = entities.Where(x => x.Until.Date <= period.ToDate.Value);
                }
            }

            return entities;
        }
    }
}
