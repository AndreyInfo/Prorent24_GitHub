using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.AccountConfiguration.UserRole
{
    public interface IUserRoleService // : IBaseService<PermissionDirectoryDto, int>
    {

        /// <summary>
        /// Get list data by filter
        /// </summary>
        /// <returns></returns>
        Task<BaseListDto> GetPagedList();

        Task<List<PermissionDirectoryDto>> GetPermission();
        Task<UserRoleDto> GetUserPermissions();
        Task<UserRoleDto> GetUserPermissionsForModule();

        Task InitPermission();
        Task InitPermissionForDefaultRole();

        /// <summary>
        /// Get Role by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserRoleDto> GetById(string id);
        Task<UserRoleDto> UpdateRole(UserRoleDto model);
        Task<UserRoleDto> InsertRole(UserRoleDto model);
        Task<bool> DeleteRole(string id);
    }
}
