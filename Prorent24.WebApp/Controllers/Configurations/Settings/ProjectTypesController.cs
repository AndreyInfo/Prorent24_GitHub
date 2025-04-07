using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.BLL.Services.Configuration.Settings.ProjectType;
using Prorent24.WebApp.Models.Configuration.Settings;
using Swashbuckle.AspNetCore.Annotations;
using Prorent24.WebApp.Transfers.Configurations.Settings;
using Prorent24.WebApp.Transfers;
using System.Linq;
using System.Collections.Generic;
using Prorent24.Enum.General;
using Prorent24.BLL.Services;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.Enum.Configuration.ConfigurationRoles;

namespace Prorent24.WebApp.Controllers.Configurations.Settings
{
    [Route("api/configuration/settings/project_types")]
    [ApiController]
    [SwaggerTag("Configuration -> Settings")]
    public class ProjectTypesController : ControllerBase
    {
        private readonly IProjectTypeService _projectTypeService;
        private readonly IPermissionService _permissionService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectTypeService"></param>
        /// <param name="permissionService"></param>
        public ProjectTypesController(IProjectTypeService projectTypeService, IPermissionService permissionService)
        {
            _projectTypeService = projectTypeService;
            _permissionService = permissionService;
        }


        /// <summary>
        /// Get list ProjectTypes
        /// </summary>
        /// <returns></returns>
        [HttpGet("default")]
        public async Task<IActionResult> GetProjectTypesByDefault()
        {
            ProjectTypeDefaultDto dto = await _projectTypeService.GetProjectTypeByDefault();
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get list ProjectTypes
        /// </summary>
        /// <returns></returns>
        [HttpPost("default")]
        public async Task<IActionResult> UpdateProjectTypesByDefault(ProjectTypeDefaultViewModel projectTypeDefaultViewModel)
        {
            ProjectTypeDefaultDto dto = await _projectTypeService.UpdateProjectTypeByDefault(projectTypeDefaultViewModel.TransferToDto());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get list ProjectTypes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProjectTypes()
        {
            BaseListDto dto = await _projectTypeService.GetPagedList(null);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get ProjectType by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectTypeById(int id)
        {
            ProjectTypeDto dto = await _projectTypeService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Create ProjectType
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProjectType(ProjectTypeViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.ProjectTypes).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                if (ModelState.IsValid)
                {
                    ProjectTypeDto dto = await _projectTypeService.Create(model.TransferToDto());
                    return Ok(dto.TransferToViewModel());
                }
                var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                return BadRequest(errors);

            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// Update ProjectType
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateProjectType(ProjectTypeViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.ProjectTypes).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                if (ModelState.IsValid)
                {
                    ProjectTypeDto dto = await _projectTypeService.Update(model.TransferToDto());
                    return Ok(dto.TransferToViewModel());
                }

                var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                return BadRequest(errors);
            }
            else
            {
                return Forbid();
            }

        }

        /// <summary>
        /// Delete ProjectType
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteProjectType(int id)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.ProjectTypes).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                bool result = await _projectTypeService.Delete(id);
                return Ok(result);

            }
            else
            {
                return Forbid();
            }


        }

        /// <summary>
        /// Delete ProjectType Multiple
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteProjectTypeMultiple(List<int> ids)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.ProjectTypes).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                bool result = await _projectTypeService.DeleteMultiple(ids); 
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
      
        }
    }
}