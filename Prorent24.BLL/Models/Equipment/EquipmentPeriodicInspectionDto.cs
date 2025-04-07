using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Equipment
{
    public class EquipmentPeriodicInspectionDto: BaseDto<int>
    {
        [IncludeToGrid(Order = 5, ColumnGroup = ColumnGroupEnum.General, IsDisplay = false)]
        public int EquipmentId { get; set; }

        [IncludeToGrid(Order = 6, ColumnGroup = ColumnGroupEnum.General, IsDisplay = false)]
        public int PeriodicInspectionId { get; set; }

        [IncludeToGrid(Order = 7, ColumnGroup = ColumnGroupEnum.General)]
        public string Name { get; set; }

        [IncludeToGrid(Order = 8, ColumnGroup = ColumnGroupEnum.General)]
        public int Period { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 9, ColumnGroup = ColumnGroupEnum.General)]
        public TimeUnitTypeEnum Frequency { get; set; }

        [IncludeToGrid(Order = 10, ColumnGroup = ColumnGroupEnum.General)]
        public string Description { get; set; }

        [IncludeToGrid(Order = 11, ColumnGroup = ColumnGroupEnum.General)]
        public bool Active { get; set; }
    }
}
