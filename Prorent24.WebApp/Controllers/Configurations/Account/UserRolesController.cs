using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Services;
using Prorent24.BLL.Services.Configuration.AccountConfiguration.UserRole;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.Account;
using Prorent24.WebApp.Transfers.Configurations.Account;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.Configurations.Account
{
    /// <summary>
    /// Configuration user roles
    /// </summary>
    [Route("api/configuration/account/user_roles")]
    [ApiController]
    [SwaggerTag("Configuration -> Account")]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;
        private readonly IPermissionService _permissionService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRoleService"></param>
        public UserRolesController(IUserRoleService userRoleService, IPermissionService permissionService)
        {
            _userRoleService = userRoleService;
            _permissionService = permissionService;
        }


        /// <summary>
        /// Get list UserRoles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserRoles()
        {
            BaseListDto dto = await _userRoleService.GetPagedList();
            return Ok(dto);
        }

        /// <summary>
        /// Get list UserRole by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetUserRoleById(string roleId)
        {
            UserRoleDto userRole = await _userRoleService.GetById(roleId);
            return Ok(userRole.TransferToView());

        }


        /// <summary>
        /// get permissions for new role
        /// </summary>
        /// <returns></returns>
        [HttpGet("modules_new_role")]
        public async Task<IActionResult> GetPermission()
        {
            List<PermissionDirectoryDto> dtos = await _userRoleService.GetPermission();
            return Ok(dtos.Select(x => x.TransferToView()));
        }

        /// <summary>
        /// update role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{roleId}")]
        public async Task<IActionResult> UpdateRole(string roleId, UserRoleViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.UserRoles).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                UserRoleDto dto = await _userRoleService.UpdateRole(model.TransferToDto());
                return Ok(model);
            }
            else
            {
                return Forbid();
            }

        }

        /// <summary>
        /// delete role - return true if exist user with
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost("{roleId}/delete")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.UserRoles).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                bool result = await _userRoleService.DeleteRole(roleId);
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// insert role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertRole(UserRoleViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.UserRoles).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                UserRoleDto dto = await _userRoleService.InsertRole(model.TransferToDto());
                return Ok(dto?.TransferToView());
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// Initial Permission
        /// </summary>
        /// <returns></returns>
        [HttpPost("initialPermission")]
        public async Task<IActionResult> InitialPermission()
        {
            await _userRoleService.InitPermission();
            return Ok();
        }
    }
}