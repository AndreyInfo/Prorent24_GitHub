using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Services;
using Prorent24.BLL.Services.Configuration.Financial.InvoiceMoment;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.Configuration.Financials;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Configurations.Financials;
using Swashbuckle.AspNetCore.Annotations;

namespace Prorent24.WebApp.Controllers.Configurations.Financials
{
    [Route("api/configuration/financial/invoice_moments")]
    [ApiController]
    [SwaggerTag("Configuration -> Financial")]
    public class InvoiceMomentController : ControllerBase
    {
        private readonly IInvoiceMomentService _invoiceMomentService;
        private readonly IPermissionService _permissionService;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="invoiceMomentService"></param>
        /// <param name="permissionService"></param>
        public InvoiceMomentController(IInvoiceMomentService invoiceMomentService, IPermissionService permissionService)
        {
            _invoiceMomentService = invoiceMomentService;
            _permissionService = permissionService;
        }
        /// <summary>
        /// get invoice moments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetInvoiceMoments()
        {
            BaseListDto dto = await _invoiceMomentService.GetPagedList(null);
            return Ok(dto.TransferToViewModel());
        }
        /// <summary>
        ///  get invoice moment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceMomentById(int id)
        {
            InvoiceMomentDto dto = await _invoiceMomentService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }
        /// <summary>
        /// create invoice moment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateInvoiceMoment(InvoiceMomentViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.InvoiceMoments).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                InvoiceMomentDto dto = await _invoiceMomentService.Create(model.TransferToDtoModel());
                return Ok(dto.TransferToViewModel());
            }
            else
            {
                return Forbid();
            }
        }
        /// <summary>
        /// update invoice moment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateInvoiceMoment(int id, InvoiceMomentViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.InvoiceMoments).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                InvoiceMomentDto dto = await _invoiceMomentService.Update(model.TransferToDtoModel());
                return Ok(dto.TransferToViewModel());
            }
            else
            {
                return Forbid();
            }
        }
        /// <summary>
        /// delete invoice moment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteInvoiceMoment(int id)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.InvoiceMoments).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                bool result = await _invoiceMomentService.Delete(id);
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// Delete invoice moment multiple
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteInvoiceMomentMultiple([FromBody]List<int> ids)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.InvoiceMoments).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                bool result = await _invoiceMomentService.DeleteMultiple(ids);
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
        }
    }
}