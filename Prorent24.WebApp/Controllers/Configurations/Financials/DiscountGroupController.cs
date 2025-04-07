using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Services;
using Prorent24.BLL.Services.Configuration.Financial.DiscountGroup;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.Configuration.Financials;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Configurations.Financials;
using Swashbuckle.AspNetCore.Annotations;

namespace Prorent24.WebApp.Controllers.Configurations.Financials
{
    [Route("api/configuration/financial/discount_groups")]
    [ApiController]
    [SwaggerTag("Configuration -> Financial")]
    public class DiscountGroupController : ControllerBase
    {
        private readonly IDiscountGroupService _discountGroupService;
        private readonly IPermissionService _permissionService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="discountGroupService"></param>
        /// <param name="permissionService"></param>
        public DiscountGroupController(IDiscountGroupService discountGroupService, IPermissionService permissionService)
        {
            _discountGroupService = discountGroupService;
            _permissionService = permissionService;
        }

        /// <summary>
        /// get discount groups
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetDiscountGroups()
        {
            BaseListDto dto = await _discountGroupService.GetPagedList(null);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// get discount group by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountGroupById(int id)
        {
            DiscountGroupDto dto = await _discountGroupService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// create discount group
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateDiscountGroup(DiscountGroupViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.DiscountGroups).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                DiscountGroupDto dto = await _discountGroupService.Create(model.TransferToDtoModel());
                return Ok(dto.TransferToViewModel());
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// update discount group
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateDiscountGroup(int id, DiscountGroupViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.DiscountGroups).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                DiscountGroupDto dto = await _discountGroupService.Update(model.TransferToDtoModel());
                return Ok(dto.TransferToViewModel());
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// delete discount group
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteDiscountGroup(int id)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.DiscountGroups).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                bool result = await _discountGroupService.Delete(id);
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// delete discount group multiple
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteDiscountGroupMultiple([FromBody]List<int> ids)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.DiscountGroups).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                bool result = await _discountGroupService.DeleteMultiple(ids);
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
        }
    }
}