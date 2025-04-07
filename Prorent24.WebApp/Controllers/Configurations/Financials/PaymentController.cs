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
using Prorent24.BLL.Services.Configuration.Financial.Payment;
using Prorent24.Common.Models.Grids;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.Configuration.Financials.Payment;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Configurations.Financials;
using Swashbuckle.AspNetCore.Annotations;

namespace Prorent24.WebApp.Controllers.Configurations.Financials
{
    [Route("api/configuration/financial")]
    [ApiController]
    [SwaggerTag("Configuration -> Financial")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentConditionService _paymentConditionService;
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IPermissionService _permissionService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="paymentConditionService"></param>
        /// <param name="paymentMethodService"></param>
        /// <param name="permissionService"></param>
        public PaymentController(IPaymentConditionService paymentConditionService,
            IPaymentMethodService paymentMethodService,
             IPermissionService permissionService)
        {
            _paymentConditionService = paymentConditionService;
            _paymentMethodService = paymentMethodService;
            _permissionService = permissionService;
        }

        #region Payment Method
        /// <summary>
        /// get payment methods
        /// </summary>
        /// <returns></returns>
        [HttpGet("payment_methods")]
        public async Task<IActionResult> GetPaymentMethods()
        {
            BaseListDto grid = await _paymentMethodService.GetPagedList(null);
            return Ok(grid.TransferToViewModel());
        }

        /// <summary>
        /// create payment method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("payment_methods")]
        public async Task<IActionResult> CreatePaymentMethod(PaymentMethodViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.PaymentMethods).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                PaymentMethodDto dto = await _paymentMethodService.Create(model.TransferToDtoModel());
                return Ok(dto.TransferToViewModel());
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// update payment method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("payment_methods/{id}")]
        public async Task<IActionResult> UpdatePaymentMethod(int id, PaymentMethodViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.PaymentMethods).FirstOrDefault()?.Value?.ToLower() == "yes")
            {

                PaymentMethodDto dto = await _paymentMethodService.Update(model.TransferToDtoModel());
                return Ok(dto.TransferToViewModel());
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// delete payment method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("payment_methods/{id}/delete")]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.PaymentMethods).FirstOrDefault()?.Value?.ToLower() == "yes")
            {

                bool result = await _paymentMethodService.Delete(id);
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
        }


        /// <summary>
        /// Delete payment method multiple
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("payment_methods/delete")]
        public async Task<IActionResult> DeletePaymentMethodMultiple([FromBody]List<int> ids)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.PaymentMethods).FirstOrDefault()?.Value?.ToLower() == "yes")
            {

                bool result = await _paymentMethodService.DeleteMultiple(ids);
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
        }

        #endregion

        #region Payment Condition

        /// <summary>
        /// Get payment condition by default
        /// </summary>
        /// <returns></returns>
        [HttpGet("default")]
        public async Task<IActionResult> GetPaymentConditionByDefault()
        {
            PaymentConditionDefaultDto dto = await _paymentConditionService.GetPaymentConditionByDefault();
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update payment condition by default
        /// </summary>
        /// <returns></returns>
        [HttpPost("default")]
        public async Task<IActionResult> UpdatePaymentConditionByDefault(PaymentConditionDefaultViewModel paymentConditionDefaultViewModel)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.PaymentConditions).FirstOrDefault()?.Value?.ToLower() == "yes")
            {

                PaymentConditionDefaultDto dto = await _paymentConditionService.UpdatePaymentConditionByDefault(paymentConditionDefaultViewModel.TransferToDtoModel());
                return Ok(dto.TransferToViewModel());
            }
            else
            {
                return Forbid();
            }
        }


        /// <summary>
        /// get payment conditions
        /// </summary>
        /// <returns></returns>
        [HttpGet("payment_conditions")]
        public async Task<IActionResult> GetPaymentConditions()
        {
            BaseListDto grid = await _paymentConditionService.GetPagedList(null);
            return Ok(grid.TransferToViewModel());
        }

        /// <summary>
        /// create payment condition
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("payment_conditions")]
        public async Task<IActionResult> CreatePaymentCondition(PaymentConditionViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.PaymentConditions).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                PaymentConditionDto dto = await _paymentConditionService.Create(model.TransferToDtoModel());
                return Ok(dto.TransferToViewModel());
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// update payment condition
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("payment_conditions/{id}")]
        public async Task<IActionResult> UpdatePaymentCondition(int id, PaymentConditionViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.PaymentConditions).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                PaymentConditionDto dto = await _paymentConditionService.Update(model.TransferToDtoModel());
                return Ok(dto.TransferToViewModel());
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// delete payment condition
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("payment_conditions/{id}/delete")]
        public async Task<IActionResult> DeletePaymentCondition(int id)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.PaymentConditions).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                bool result = await _paymentConditionService.Delete(id);
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// Delete payment condition multiple
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("payment_conditions/delete")]
        public async Task<IActionResult> DeletePaymentConditionMultiple([FromBody]List<int> ids)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.PaymentConditions).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                bool result = await _paymentConditionService.DeleteMultiple(ids);
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
        }

        #endregion
    }
}