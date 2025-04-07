using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Services.Configuration.Financial.ElectronicInvoicing;
using Prorent24.WebApp.Models.Configuration.Financials;
using Prorent24.WebApp.Transfers.Configurations.Financials;
using Swashbuckle.AspNetCore.Annotations;

namespace Prorent24.WebApp.Controllers.Configurations.Financials
{
    [Route("api/configuration/financial/electronic_invoicings")]
    [ApiController]
    [SwaggerTag("Configuration -> Financial")]
    public class ElectronicInvoicingController : ControllerBase
    {
        private readonly IElectronicInvoicingService _electronicInvoicingService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="electronicInvoicingService"></param>
        public ElectronicInvoicingController(IElectronicInvoicingService electronicInvoicingService)
        {
            _electronicInvoicingService = electronicInvoicingService;
        }

        /// <summary>
        /// Get Electronic Invoicing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ElectronicInvoicingViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetElectronicInvoicingInfo()
        {
            ElectronicInvoicingDto dto = await _electronicInvoicingService.GetAsync();
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update Electronic Invoicing Info
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ElectronicInvoicingViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateElectronicInvoicingInfo([FromBody]ElectronicInvoicingViewModel model)
        {
            ElectronicInvoicingDto dto = await _electronicInvoicingService.Update(model.TransferToDto());
            return Ok(dto.TransferToViewModel());
        }
    }
}