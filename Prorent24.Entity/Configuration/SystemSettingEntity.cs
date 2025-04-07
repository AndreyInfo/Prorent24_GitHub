using Prorent24.Entity.Base;
using Prorent24.Enum.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Configuration
{
    [Table("sys_system_settings")]
    public class SystemSettingEntity //: BaseEntity
    {
        [Key]
        public SystemSettingsTypeEnum Type { get; set; }
        public string Alias { get; set; }
        public string Value { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? LastModifiedDate { get; set; }

        public string LastModifiedUserId { get; set; }
        [ForeignKey("LastModifiedUserId")]
        public virtual User LastModifiedUser { get; set; }
    }
}
