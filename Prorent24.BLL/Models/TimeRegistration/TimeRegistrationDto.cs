using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using Prorent24.Enum.General;
using Prorent24.Enum.TimeRegistration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Prorent24.BLL.Models.TimeRegistration
{
    public class TimeRegistrationDto : BaseDto<int>
    {
        public string Name { get; set; }
        public string CrewMemberId { get; set; }

        [IncludeToGrid(Order = 5, ColumnName ="fio",TreeColumn = true, TreeColumnOrder = 1, ColumnGroup = ColumnGroupEnum.General)]
        public string CrewMember { get; set; }

        [IncludeToGrid(Order = 6, ColumnName = "timestart", TreeColumn = true, TreeColumnOrder = 2, ColumnGroup = ColumnGroupEnum.General)]
        public DateTime From { get; set; }

        [IncludeToGrid(Order = 7, ColumnName = "timeend", TreeColumn = true, TreeColumnOrder = 3, ColumnGroup = ColumnGroupEnum.General)]
        public DateTime Until { get; set; }

        [IncludeToGrid(Order = 8, ColumnGroup = ColumnGroupEnum.General)]
        public decimal? Distance { get; set; }

        [IncludeToGrid(Order = 9, TreeColumn = true, TreeColumnOrder = 4, ColumnGroup = ColumnGroupEnum.General)]
        public int BreakDuration { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 10, ColumnGroup = ColumnGroupEnum.General)]
        public TimeUnitTypeEnum BreakTimeUnit { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 11, ColumnName = "typeofregistration", TreeColumn = true, TreeColumnOrder = 5, ColumnGroup = ColumnGroupEnum.General)]
        public HourRegistrationTypeEnum HourRegistrationType { get; set; }

        [IncludeToGrid(Order = 12, ColumnGroup = ColumnGroupEnum.General)]
        public int TravelDuration { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 13, ColumnGroup = ColumnGroupEnum.General)]
        public TimeUnitTypeEnum TravelTimeUnit { get; set; }

        [IncludeToGrid(Order = 14, ColumnGroup = ColumnGroupEnum.General)]
        public bool Lunch { get; set; }

        [IncludeToGrid(Order = 15, ColumnName = "comment", TreeColumn = true, TreeColumnOrder = 6, ColumnGroup = ColumnGroupEnum.General)]
        public string Remark { get; set; }

        public List<TimeRegistrationActivityDto> Activities { get; set; }
        public List<CrewMemberShortDto> CrewMembers { get; set; }

        #region Additional Fields Grid

        [IncludeToGrid(Order = 16, IsDisplay = false)]
        public string FromGroupName => From.GetMonthName();

        //[IncludeToGrid(Order = 1, DisplayName = "Month")]
        public string MonthGrid
        {
            get
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(From.Month);
            }
        }

        [IncludeToGrid(Order = 17, DisplayName = "DurationText", ColumnGroup = ColumnGroupEnum.General)]
        public string DurationGrid
        {
            get
            {
                return this.GetDuration();
            }
        }

        [IncludeToGrid(Order = 18, DisplayName = "TravelDurationText", ColumnGroup = ColumnGroupEnum.General)]
        public string TravelDurationGrid
        {
            get
            {
                return this.GetTravelDuration();
            }
        }

        [IncludeToGrid(Order = 19, DisplayName = "BreakDurationText", ColumnGroup = ColumnGroupEnum.General)]
        public string BreakDurationGrid
        {
            get
            {
                return this.GetBreakDuration();
            }
        }

        #endregion
    }
}
