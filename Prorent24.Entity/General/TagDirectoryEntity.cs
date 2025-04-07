using Prorent24.Entity.Base;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.General
{
    [Table("dbo_tag_direcories")]
    public class TagDirectoryEntity: BaseEntity
    {
        public string Name { get; set; }

        public string LowerName { get; set; }

        public ModuleTypeEnum BelongsTo { get; set; }
    }
}
