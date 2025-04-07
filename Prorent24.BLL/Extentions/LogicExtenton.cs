using Prorent24.BLL.Models.TimeRegistration;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Extentions
{
    public static class MathExtenton
    {
        #region CrewMembers

        public static string GetDuration(this TimeRegistrationDto dto)
        {
            long different = (dto.Until.Ticks - dto.From.Ticks);
            DateTime result = new DateTime(GetTimeFromTimeUnit(0, different).Ticks);
            return new DateTime(different).AddDateTimeFromTimeUnit(dto.TravelTimeUnit, dto.TravelDuration).ToString("HH:mm");
        }

        public static string GetTravelDuration(this TimeRegistrationDto dto)
        {
            DateTime result = new DateTime(GetTimeFromTimeUnit(dto.TravelTimeUnit, dto.TravelDuration).Ticks);
            return result.ToString("HH:mm");
        }

        public static string GetBreakDuration(this TimeRegistrationDto dto)
        {
            DateTime result = new DateTime(GetTimeFromTimeUnit(dto.BreakTimeUnit, dto.BreakDuration).Ticks);
            return result.ToString("HH:mm");
        }

        #endregion

        #region Common

        public static TimeSpan GetTimeFromTimeUnit(TimeUnitTypeEnum timeUnit, long value)
        {
            switch (timeUnit)
            {
                case Enum.General.TimeUnitTypeEnum.Days:
                    {
                        return TimeSpan.FromDays(value);
                    }
                case Enum.General.TimeUnitTypeEnum.Hours:
                    {
                        return TimeSpan.FromHours(value);
                    }
                case Enum.General.TimeUnitTypeEnum.Minutes:
                    {
                        return TimeSpan.FromMinutes(value);
                    }
                case Enum.General.TimeUnitTypeEnum.Weeks:
                    {
                        return TimeSpan.FromDays(value*7);
                    }
                default:
                    {
                        return TimeSpan.FromTicks(value);
                    }
            }
        }

        public static DateTime AddDateTimeFromTimeUnit(this DateTime result, TimeUnitTypeEnum timeUnit, int value)
        {
            switch (timeUnit)
            {
                case Enum.General.TimeUnitTypeEnum.Days:
                    {
                        result = result.AddDays(value);
                        break;
                    }
                case Enum.General.TimeUnitTypeEnum.Hours:
                    {
                        result = result.AddHours(value);
                        break;
                    }
                case Enum.General.TimeUnitTypeEnum.Minutes:
                    {
                        result = result.AddMinutes(value);
                        break;
                    }
                case Enum.General.TimeUnitTypeEnum.Months:
                    {
                        result = result.AddMonths(value);
                        break;
                    }
                case Enum.General.TimeUnitTypeEnum.Years:
                    {
                        result = result.AddYears(value);
                        break;
                    }
                case Enum.General.TimeUnitTypeEnum.Weeks:
                    {
                        result = result.AddDays(value*7);
                        break;
                    }
                default:
                    {
                        result = result.AddHours(value);
                        break;
                    }
            }

            return result;
        }

        #endregion
    }
}
