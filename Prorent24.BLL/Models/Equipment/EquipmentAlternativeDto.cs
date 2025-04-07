using Prorent24.Common.Attributes;
using Prorent24.Entity.Equipment;
using Prorent24.Enum.Directory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Equipment
{
    public class EquipmentAlternativeDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5, ColumnGroup = ColumnGroupEnum.General, IsDisplay = false)]
        public int EquipmentId { get; set; }

        [IncludeToGrid(Order = 6, ColumnGroup = ColumnGroupEnum.General, IsDisplay = false)]
        public string Equipment { get; set; }

        [IncludeToGrid(Order = 7, ColumnGroup = ColumnGroupEnum.General, IsDisplay = false)]
        public int AlternativeId { get; set; }

        [IncludeToGrid(Order = 8, ColumnGroup = ColumnGroupEnum.General)]
        public string AlternativeName { get { return Alternative?.Name; } }

        [IncludeToGrid(Order = 9, ColumnGroup = ColumnGroupEnum.General)]
        public string Code { get { return Alternative?.Code; } }

        [IncludeToGrid(Order = 10, ColumnGroup = ColumnGroupEnum.General, IsEntity = true, IsDisplay = false)]
        public EquipmentDto Alternative { get; set; }
    }
}
