using Prorent24.Common.Attributes;
using System.Collections.Generic;

namespace Prorent24.BLL.Models.Configuration.AccountConfiguration
{
    public class UserRoleDto
    {
        [IncludeToGrid(Order = 5, IsKey = true, IsDisplay = false, KeyType = "string")]
        public string Id { get; set; }

        [IncludeToGrid(Order = 6)]
        public string Name { get; set; }
        
        public decimal? Rate { get; set; }

        public List<RolePermissionDto> RolePermissions { get; set; }
    }
}
