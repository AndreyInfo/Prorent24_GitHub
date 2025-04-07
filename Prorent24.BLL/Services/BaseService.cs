using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Transfers.General;
using Prorent24.Common.Hubs;
using Prorent24.Common.Models.Columns;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.Entity.General;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services
{
    public class BaseService : IPermissionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        internal readonly IUserRoleStorage _userRoleStorage;
        internal readonly IColumnStorage _columnStorage;
        // додати в базовий  сервіс, перевірити роботу
        //private readonly IPermissionStorage _permissionStorage;

        public BaseService(IHttpContextAccessor httpContextAccessor, IUserRoleStorage userRoleStorage, IColumnStorage сolumnStorage)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRoleStorage = userRoleStorage;
            _columnStorage = сolumnStorage;
        }

        public string GetUserId()
        {
            var claims = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Claims;
            if (claims != null && claims.Count() > 0)
            {
                string userId = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Claims?.FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;
                return userId;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<List<UserColumn>> GetUserColumns(EntityEnum entity)
        {
            List<UserColumnEntity> userColumnsEntity = await _columnStorage.GetColumnsByEntity(entity, GetUserId());
            List<UserColumn> userColumns = userColumnsEntity.TransferToList();
            return userColumns;
        }

        public async Task<FunctionPermissionDto> GetFunctionPermissions(ModuleTypeEnum module, PermissionFieldEnum? function = null)
        {
            string userId = this.GetUserId();

            if (!string.IsNullOrWhiteSpace(userId))
            {
                var permissions = await _userRoleStorage.GetUserPermissionByModule(userId, module);
                var dto = permissions.TransferToUserRolePermissionDto(module);
                var databaseVehiclePermission = dto.Functions.Where(x => x.Function == function).FirstOrDefault();
                var permission = module != ModuleTypeEnum.Configuration ? databaseVehiclePermission?.Permission : databaseVehiclePermission.Value == "Yes" ? new FunctionPermissionDto() { Add = true, Delete = true, Edit = true, View = true } : new FunctionPermissionDto();

                return permission == null ? new FunctionPermissionDto() { Add = true, Delete = true, Edit = true, View = true } : permission;
            }
            else
            {
                return new FunctionPermissionDto() { Add = true };
            }
        }

        public async Task<FunctionPermissionDto> GetModulePermission(ModuleTypeEnum module)
        {
            string userId = this.GetUserId();

            if (!string.IsNullOrWhiteSpace(userId))
            {
                var permissions = await _userRoleStorage.GetUserPermissionByModule(this.GetUserId(), module);
                var dto = permissions.TransferToUserRolePermissionDto(module);
                if (dto.IsAllowed)
                {
                    return new FunctionPermissionDto() { Add = true, Delete = true, Edit = true, View = true };
                }
                else
                {
                    return new FunctionPermissionDto() { Add = false, Delete = false, Edit = false, View = false };
                }
            }
            else
            {
                return new FunctionPermissionDto() { Add = true, View = true };
            }
        }


        public async Task<UserRoleModulePermissionDto> GetPermissions(ModuleTypeEnum module)
        {
            var permissions = await _userRoleStorage.GetUserPermissionByModule(this.GetUserId(), module);
            var dto = permissions.TransferToUserRolePermissionDto(module);
            return dto;
        }

        public string GetCurrentLang()
        {
            var currentLanguage = CultureInfo.CurrentCulture?.TwoLetterISOLanguageName;
            return currentLanguage ?? "ru";
        }


    }
}
