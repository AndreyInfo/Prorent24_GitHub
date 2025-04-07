using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Configuration.AccountConfiguration
{
    public class RolePermissionDto
    {
        public int Id { get; set; }

        public string RoleId { get; set; }

        public int PermissionDirectoryId { get; set; }
        
        public PermissionDirectoryDto PermissionDirectory { get; set; }
    
        public string Value { get; set; }
    }
}
