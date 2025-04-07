using Prorent24.Entity.Base;
using Prorent24.Enum.Directory;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Directory
{
    [Table("sys_directory")]
    public class DirectoryEntity: BaseEntity
    {
        public DirectoryTypeEnum Type { get; set; }

        [Column(TypeName = "BOOLEAN")]
        public bool IsActive { get; set; }

        public int? ParentId { get; set; }

        public virtual ICollection<DirectoryLocEntity> Locs { get; set; }

        public string Key { get; set; }
    }
}
