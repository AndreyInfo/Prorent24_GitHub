using Prorent24.Enum.Directory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Directory
{
    public class DirectoryDto : BaseDto<int>
    {
        public DirectoryTypeEnum Type { get; set; }

        public bool IsActive { get; set; }

        public int? ParentId { get; set; }

        public List<DirectoryLocDto> Locs { get; set; }

        public string Key { get; set; }

    }
}
