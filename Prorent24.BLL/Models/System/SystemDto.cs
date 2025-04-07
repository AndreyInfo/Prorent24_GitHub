using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using Prorent24.Enum.Entity;
using System;

namespace Prorent24.BLL.Models.System
{
    public class SystemDto
    {
        [IncludeToGrid]
        public int Id { get; set; }

        [IncludeToGrid(IsDisplay = false, IsEntity = false, ColumnGroup = ColumnGroupEnum.System)]
        public DateTime LastChange { get; set; }

        [IncludeToGrid(IsEntity = true, EntityKey = "CreationUser", EntityKeyType = "UserEntity")]
        public EntityEnum CreatedBy { get; set; }
    }
}
