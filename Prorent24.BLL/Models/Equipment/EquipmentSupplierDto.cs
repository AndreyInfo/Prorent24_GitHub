using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;

namespace Prorent24.BLL.Models.Equipment
{
    public class EquipmentSupplierDto : BaseDto<int>
    {
        public int EquipmentId { get; set; }
        //[IncludeToGrid(ColumnGroup = ColumnGroupEnum.General)]
        public string Equipment { get; set; }

        [IncludeToGrid(Order = 5, ColumnGroup = ColumnGroupEnum.General)]
        public decimal Price { get; set; }

        [IncludeToGrid(Order = 6, ColumnGroup = ColumnGroupEnum.General)]
        public string Details { get; set; }

        [IncludeToGrid(Order = 7, ColumnGroup = ColumnGroupEnum.General, IsDisplay = false)]
        public int ContactId { get; set; }

        [IncludeToGrid(Order = 8, ColumnGroup = ColumnGroupEnum.General)]
        public string ContactName { get; set; }
    }
}
