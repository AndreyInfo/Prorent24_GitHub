using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.BLL.Services.Configuration.CustomerCommunication.Salutation;
using Prorent24.WebApp.Models.Configuration.CustomerCommunication;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Configurations.CustomerCommunication;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.Configurations.CustomerCommunication
{
    [Route("api/configuration/customer_communication/[controller]")]
    [ApiController]
    [SwaggerTag("Configuration -> CustomerCommunicaation")]
    public class SalutationsController : ControllerBase
    {
        private readonly ISalutationService _salutationService;

        public SalutationsController(ISalutationService salutationService) {
            _salutationService = salutationService;
        }
        /// <summary>
        /// Get list Salutations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetSalutations()
        {
            BaseListDto dto = await _salutationService.GetPagedList(null);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get list Salutation by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalutationById(int id)
        {
            SalutationDto dto = await _salutationService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Create Salutation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateSalutation(SalutationViewModel model)
        {
            SalutationDto dto = await _salutationService.Create(model.TransferToViewModel());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update Salutation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateSalutation(SalutationViewModel model)
        {
            SalutationDto dto = await _salutationService.Update(model.TransferToViewModel());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Delete Salutation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteSalutation(int id)
        {
            bool result = await _salutationService.Delete(id);
            return Ok(result);
        }
    }
}