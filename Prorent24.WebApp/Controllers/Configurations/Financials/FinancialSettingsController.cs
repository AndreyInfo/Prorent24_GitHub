using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Services;
using Prorent24.BLL.Services.Configuration.Financial.FinancialSetting;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.Configuration.Financials;
using Prorent24.WebApp.Transfers.Configurations.Financials;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.Configurations.Financials
{
    [Route("api/configuration/financial/financial_settings")]
    [ApiController]
    [SwaggerTag("Configuration -> Financial")]
    //[Obsolete("dont work")]
    public class FinancialSettingController : ControllerBase
    {
        private readonly IFinancialSettingService _financialSettingService;
        private readonly IPermissionService _permissionService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="financialSettingService"></param>
        /// <param name="permissionService"></param>
        public FinancialSettingController(IFinancialSettingService financialSettingService,
            IPermissionService permissionService)
        {
            _financialSettingService = financialSettingService;
            _permissionService = permissionService;
        }

        /// <summary>
        /// Get Financial Settings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(FinancialSettingViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFinancialSettingInfo()
        {
            FinancialSettingDto dto = await _financialSettingService.Get();
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update Electronic Invoicing Info
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(FinancialSettingViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateElectronicInvoicingInfo([FromBody]FinancialSettingViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.Financial).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                FinancialSettingDto dto = await _financialSettingService.Update(model.TransferToDtoModel());
                return Ok(dto.TransferToViewModel());
            }
            else
            {
                return Forbid();
            }
        }
    }
}