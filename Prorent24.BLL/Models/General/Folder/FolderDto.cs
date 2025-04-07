using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using Prorent24.Enum.General;
using System.Collections.Generic;

namespace Prorent24.BLL.Models.General.Folder
{
    public class FolderDto : BaseDto<int>
    {
        public ModuleTypeEnum ModuleType { get; set; }
        [IncludeToGrid(IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public List<FolderDto> Childs { get; set; }
        public int Order { get; set; }
        public bool Selected { get; set; }
    }
}
