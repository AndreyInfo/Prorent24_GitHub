using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Configuration.AccountConfiguration
{
    public class UserRoleModulePermissionDto
    {
        public ModuleTypeEnum ModuleType { get; set; }
        public bool IsAllowed { get; set; }
        public List<UserRoleFunctionPermissionDto> Functions { get; set; }
    }
}
