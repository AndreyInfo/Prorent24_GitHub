using Prorent24.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Prorent24.Entity.Configuration.Roles
{
    [Table("sys_role_permissions")]
    public class RolePermissionEntity : BaseEntity
    {
        public string RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        public int PermissionDirectoryId { get; set; }

        [ForeignKey("PermissionDirectoryId")]
        public PermissionDirectoryEntity PermissionDirectory { get; set; }
        //строка, щоразу вибирати
        public string Value { get; set; }
        //public System.Enum ValueEnum { get; set; }
    }
}
