using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Equipment
{
    public class EquipmentAccessoryDto : BaseDto<int>
    {
        public int EquipmentId { get; set; }

        [IncludeToGrid(Order = 5, ColumnGroup = ColumnGroupEnum.General, IsDisplay = false)]
        public string Equipment { get; set; }

        [IncludeToGrid(Order = 6, ColumnGroup = ColumnGroupEnum.General, IsDisplay = false)]
        public int AccessoryId { get; set; }

        [IncludeToGrid(Order = 7, ColumnGroup = ColumnGroupEnum.General)]
        public string AccessoryName { get; set; }

        [IncludeToGrid(Order = 8, ColumnGroup = ColumnGroupEnum.General, IsEntity = true, IsDisplay = false)]
        public EquipmentDto Accessory { get; set; }

        [IncludeToGrid(Order = 9, ColumnGroup = ColumnGroupEnum.General)]
        public int Quantity { get; set; }

        [IncludeToGrid(Order = 10, ColumnGroup = ColumnGroupEnum.General)]
        public bool Automatic { get; set; }

        [IncludeToGrid(Order = 11, ColumnGroup = ColumnGroupEnum.General)]
        public bool SkipIfAlreadyPresent { get; set; }

        [IncludeToGrid(Order = 12, ColumnGroup = ColumnGroupEnum.General)]
        public bool Free { get; set; } = false;
    }
}
