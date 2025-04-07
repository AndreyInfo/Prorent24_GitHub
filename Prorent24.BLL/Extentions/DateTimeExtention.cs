using Prorent24.Enum.General;
using System;
using System.Globalization;

namespace Prorent24.BLL.Extentions
{
    public static class DateTimeExtention
    {
        /// <summary>
        /// Get date with short date (monday 27-05-2019)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetDateNameWithShortDate(this DateTime dateTime)
        {
            CultureInfo culture = new CultureInfo("ru-RU");
            string day = culture.DateTimeFormat.GetDayName(dateTime.DayOfWeek);

            return string.Concat(day, " ", dateTime.ToShortDateString());
        }

        public static string GetMonthName(this DateTime dateTime)
        {
            CultureInfo culture = new CultureInfo("ru-RU");
            string month = culture.DateTimeFormat.GetMonthName(dateTime.Month);

            return month;
        }

        public static DateTime AddAndGetDateByTimeUnitTypeEnum(this DateTime date, TimeUnitTypeEnum type,int duration)
        {
            if (type == TimeUnitTypeEnum.Days)
            {
                date = date.AddDays(duration);
            }
            else if (type == TimeUnitTypeEnum.Hours)
            {
                date = date.AddHours(duration);
            }
            else if (type == TimeUnitTypeEnum.Minutes)
            {
                date = date.AddMinutes(duration);
            }
            else if (type == TimeUnitTypeEnum.Months)
            {
                date = date.AddMonths(duration);
            }
            else if (type == TimeUnitTypeEnum.Years)
            {
                date = date.AddYears(duration);
            }
            else if(type == TimeUnitTypeEnum.Weeks)
            {
                date = date.AddDays(duration * 7);
            }
            return date;
        }
    }
}
