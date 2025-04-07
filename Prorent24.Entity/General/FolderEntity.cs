using Prorent24.Entity.Base;
using Prorent24.Enum.General;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.General
{
    [Table("dbo_folders")]
    public class FolderEntity: BaseEntity
    {
        public ModuleTypeEnum ModuleType { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public bool IsSystem { get; set; }

        [ForeignKey("ParentId")]
        public ICollection<FolderEntity> Childs { get; set; }
        public int Order { get; set; }
    }
}
