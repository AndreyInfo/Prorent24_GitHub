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
using Prorent24.BLL.Services.Configuration.Financial.Vat;
using Prorent24.Common.Models.Grids;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.Configuration.Financials.Vat;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Configurations.Financials;
using Swashbuckle.AspNetCore.Annotations;

namespace Prorent24.WebApp.Controllers.Configurations.Financials
{
    [Route("api/configuration/financial")]
    [ApiController]
    [SwaggerTag("Configuration -> Financial")]
    public partial class VatController : ControllerBase
    {
        private readonly IVatSchemeService _vatSchemeService;
        private readonly IVatClassService _vatClassService;
        private readonly IPermissionService _permissionService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vatSchemeService"></param>
        /// <param name="vatClassService"></param>
        /// <param name="permissionService"></param>
        public VatController(IVatSchemeService vatSchemeService, IVatClassService vatClassService, IPermissionService permissionService)
        {
            _vatSchemeService = vatSchemeService;
            _vatClassService = vatClassService;
            _permissionService = permissionService;
        }

        #region Vat Scheme

        /// <summary>
        /// get Vat Scheme
        /// </summary>
        /// <returns></returns>
        [HttpGet("vat_schemes")]
        public async Task<IActionResult> GetVatSchemes()
        {
            BaseListDto grid = await _vatSchemeService.GetPagedList(null);
            return Ok(grid.TransferToViewModel());
        }

        /// <summary>
        /// get Vat Scheme by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("vat_schemes/{id}")]
        public async Task<IActionResult> GetVatSchemeById(int id)
        {
            VatSchemeDto dto = await _vatSchemeService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// create Vat Scheme
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("vat_schemes")]
        public async Task<IActionResult> CreateVatScheme(VatSchemeViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.VatSchemes).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                VatSchemeDto dto = await _vatSchemeService.Create(model.TransferToDtoModel());
                return Ok(dto.TransferToViewModel());
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// update Vat Scheme
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("vat_schemes/{id}")]
        public async Task<IActionResult> UpdateVatScheme(int id, VatSchemeViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.VatSchemes).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                VatSchemeDto dto = await _vatSchemeService.Update(model.TransferToDtoModel());
                return Ok(dto.TransferToViewModel());
            }
            else
            {
                return Forbid();
            }
        }

        //TODO починити видалення
        /// <summary>
        /// delete Vat Scheme
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("vat_schemes/{id}/delete")]
        public async Task<IActionResult> DeleteVatScheme(int id)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.VatSchemes).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                bool result = await _vatSchemeService.Delete(id);
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
        }

        //TODO починити видалення
        /// <summary>
        /// Delete Vat Scheme multiple
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("vat_schemes/delete")]
        public async Task<IActionResult> DeleteVatSchemeMultiple([FromBody]List<int> ids)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.VatSchemes).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                bool result = await _vatSchemeService.DeleteMultiple(ids);
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
        }

        #endregion

        #region  Vat Class
        /// <summary>
        /// get Vat Classes
        /// </summary>
        /// <returns></returns>
        [HttpGet("vat_classes")]
        public async Task<IActionResult> GetVatClasses()
        {
            BaseListDto grid = await _vatClassService.GetPagedList(null);
            return Ok(grid.TransferToViewModel());
        }

        /// <summary>
        /// get Vat Scheme
        /// </summary>
        /// <returns></returns>
        [HttpGet("vat_classes/list")]
        public async Task<IActionResult> GetListVatClasses()
        {
            List<VatClassDto> dtos = await _vatClassService.GetList();
            return Ok(dtos.TransferToListViewModel());
        }

        /// <summary>
        /// create vat class
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("vat_classes")]
        public async Task<IActionResult> CreateVatClass(VatClassViewModel model)
        {
            VatClassDto dto = await _vatClassService.Create(model.TransferToDtoModel());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// update vat class
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("vat_classes/{id}")]
        public async Task<IActionResult> UpdateVatClass(int id, VatClassViewModel model)
        {
            VatClassDto dto = await _vatClassService.Update(model.TransferToDtoModel());
            return Ok(dto.TransferToViewModel());
        }

        //TODO починити видалення
        /// <summary>
        /// delete vat class
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("vat_classes/{id}/delete")]
        public async Task<IActionResult> DeleteVatClass(int id)
        {
            bool result = await _vatClassService.Delete(id);
            return Ok(result);
        }

        //TODO починити видалення
        /// <summary>
        /// Delete vat class multiple
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("vat_classes/delete")]
        public async Task<IActionResult> DeleteVatClassMultiple([FromBody]List<int> ids)
        {
            bool result = await _vatClassService.DeleteMultiple(ids);
            return Ok(result);
        }

        #endregion
    }
}