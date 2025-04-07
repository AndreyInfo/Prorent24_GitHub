using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services
{
    public interface IPermissionService
    {
        Task<FunctionPermissionDto> GetFunctionPermissions(ModuleTypeEnum module, PermissionFieldEnum? function = null);
        string GetUserId();
        Task<FunctionPermissionDto> GetModulePermission(ModuleTypeEnum module);
        Task<UserRoleModulePermissionDto> GetPermissions(ModuleTypeEnum module);

    }
}
