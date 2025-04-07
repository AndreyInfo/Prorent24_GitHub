using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.BLL.Services;
using Prorent24.BLL.Services.Configuration.CustomerCommunication.Communication;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.Configuration.CustomerCommunication;
using Prorent24.WebApp.Transfers.Configurations.CustomerCommunication;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.Configurations.CustomerCommunication
{
    /// <summary>
    /// Configuration customer communication
    /// </summary>
    [Route("api/configuration/customer_communication/details")]
    [ApiController]
    [SwaggerTag("Configuration -> CustomerCommunicaation")]
    //[ApiExplorerSettings(GroupName = @"configuration")]
    public class CustomerCommunicationsController : ControllerBase
    {
        private readonly ICustomerCommmunicationService _custommerCommunicationService;
        private readonly IPermissionService _permissionService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="custommerCommunicationService"></param>
        /// <param name="permissionService"></param>
        public CustomerCommunicationsController(ICustomerCommmunicationService custommerCommunicationService,
            IPermissionService permissionService)
        {
            _custommerCommunicationService = custommerCommunicationService;
            _permissionService = permissionService;
        }

        /// <summary>
        /// Get Customer Communication Data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[SwaggerOperation(Tags = new[] { "Configuration.CustomerCommunication" })]
        [HttpGet]
        public async Task<IActionResult> GetCustomerCommunicationInfo()
        {
            CustomerCommmunicationDto dto = await _custommerCommunicationService.GetAsync();
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update Customer Communication Info
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //[SwaggerOperation(Tags = new[] { "Configuration.CustomerCommunication" })]
        [HttpPost]
        public async Task<IActionResult> UpdateCustomerCommunicationInfo([FromBody]CustomerCommmunicationViewModel model)
        {

            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.Communication).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                if (ModelState.IsValid)
                {
                    CustomerCommmunicationDto dto = await _custommerCommunicationService.Update(model.TransferToDto());
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
    }
}