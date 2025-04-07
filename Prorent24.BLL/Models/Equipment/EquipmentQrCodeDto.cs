using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using Prorent24.Enum.Entity;

namespace Prorent24.BLL.Models.Equipment
{
    public class EquipmentQRCodeDto : BaseDto<int>
    {
        public int? EquipmentId { get; set; }
        public int? EquipmentSerialNumberId { get; set; }
        //public int LinkedId { get; set; }
        //public EntityEnum LinkedType { get; set; }
        [IncludeToGrid(Order = 5, ColumnGroup = ColumnGroupEnum.General)]
        public string Code { get; set; }
        //[IncludeToGrid(ColumnGroup = ColumnGroupEnum.General, IsDisplay = false)]
        public string Image { get; set; }
    }
}
