using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Services;
using Prorent24.BLL.Services.Configuration.Financial.Ledger;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.Configuration.Financials;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Configurations.Financials;
using Swashbuckle.AspNetCore.Annotations;

namespace Prorent24.WebApp.Controllers.Configurations.Financials
{
    [Route("api/configuration/financial/ledgers")]
    [ApiController]
    [SwaggerTag("Configuration -> Financial")]
    public class LedgerController : ControllerBase
    {
        private readonly ILedgerService _ledgerService;
        private readonly IPermissionService _permissionService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="ledgerService"></param>
        /// <param name="permissionService"></param>
        public LedgerController(ILedgerService ledgerService,
            IPermissionService permissionService)
        {
            _ledgerService = ledgerService;
            _permissionService = permissionService;
        }
        /// <summary>
        /// get ledgers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetLedgers()
        {
            BaseListDto dto = await _ledgerService.GetPagedList(null);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// get ledger by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLedgerById(int id)
        {
            LedgerDto dto = await _ledgerService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// create ledger
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateLedger(LedgerViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.Ledger).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                LedgerDto dto = await _ledgerService.Create(model.TransferToDtoModel());
                return Ok(dto.TransferToViewModel());
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// update ledger
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateLedger(int id, LedgerViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.Ledger).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                LedgerDto dto = await _ledgerService.Update(model.TransferToDtoModel());
                return Ok(dto.TransferToViewModel());
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// delete ledger by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteLedger(int id)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.Ledger).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                bool result = await _ledgerService.Delete(id);
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// Delete Ledger multiple
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteLedgerMultiple([FromBody]List<int> ids)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.Ledger).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                bool result = await _ledgerService.DeleteMultiple(ids);
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
        }

    }
}