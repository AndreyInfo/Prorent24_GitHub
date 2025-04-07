using Prorent24.Entity.Base;
using Prorent24.Enum.General;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Configuration.Roles
{
    [Table("sys_permissions")]
    public class PermissionDirectoryEntity : BaseEntity
    {
        public string PermissionName { get; set; }

        public int? ParentPermissionId { get; set; }

        public ModuleTypeEnum? ModuleType { get; set; }

        [ForeignKey("ParentPermissionId")]
        public PermissionDirectoryEntity Parent { get; set; }

        public PermissionValueTypeEnum ValueTypeId { get; set; }

        //public int? DependencePermissionId { get; set; } //activate module

        //[ForeignKey("DependencePermissionId")]
        //public PermissionDirectoryEntity Dependence { get; set; }

        [InverseProperty("Parent")]
        public ICollection<PermissionDirectoryEntity> Children { get; set; }

        public PermissionFieldEnum? PermissionField { get; set; }
    }
}
