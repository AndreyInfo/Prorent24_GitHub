using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using System;

namespace Prorent24.BLL.Models
{
    public class BaseDto<TKEy>
    {
        [IncludeToGrid(Order = 2, IsSystem = true, IsDisplay = false, ColumnGroup = ColumnGroupEnum.System)]
        public bool View { get; set; } = true;

        [IncludeToGrid(Order = 3, IsSystem = true, IsDisplay = false, ColumnGroup = ColumnGroupEnum.System)]
        public bool Edit { get; set; } = true;

        [IncludeToGrid(Order = 4, IsSystem = true, IsDisplay = false, ColumnGroup = ColumnGroupEnum.System)]
        public bool Delete { get; set; } = true;

        [IncludeToGrid(Order = 1, IsSystem = true, IsKey = true, IsDisplay = false, ColumnGroup = ColumnGroupEnum.System)]
        public TKEy Id { get; set; }

        [IncludeToGrid(Order = 5, IsSystem = true, IsDisplay = false, ColumnGroup = ColumnGroupEnum.System)]
        public DateTime CreationDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
