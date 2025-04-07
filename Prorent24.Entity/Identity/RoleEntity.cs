using Microsoft.AspNetCore.Identity;
using Prorent24.Entity.Configuration.Roles;
using Prorent24.Entity.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity
{
    public class Role : IdentityRole
    {
        [Column(TypeName = "BOOLEAN")]
        public bool IsDefault { get; set; } = false;
        //public List<UserRole> UserRoles { get; set; }
        public decimal? Rate { get; set; }
        public List<RolePermissionEntity> RolePermissions { get; set; }
    }
}
