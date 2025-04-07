using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Invoice;
using Prorent24.BLL.Services.Invoice;
using Prorent24.BLL.Services.Invoice.Excluded;
using Prorent24.BLL.Services.Invoice.InvoiceLine;
using Prorent24.WebApp.Models.General.Filter;
using Prorent24.WebApp.Models.Invoice;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.General;
using Prorent24.WebApp.Transfers.Invoice;
using Prorent24.WebApp.Transfers.Modules;
using Swashbuckle.AspNetCore.Annotations;

namespace Prorent24.WebApp.Controllers
{
    [Route("api/invoices")]
    [ApiController]
    public partial class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IInvoiceLineService _invoiceLineService;
        private readonly IInvoiceExcludedService _invoiceExcludedService;

        public InvoiceController(IInvoiceService invoiceService, IInvoiceLineService invoiceLineService, IInvoiceExcludedService invoiceExcludedService)
        {
            _invoiceService = invoiceService ?? throw new ArgumentNullException(nameof(invoiceService));
            _invoiceLineService = invoiceLineService ?? throw new ArgumentNullException(nameof(invoiceLineService));
            _invoiceExcludedService = invoiceExcludedService ?? throw new ArgumentNullException(nameof(invoiceExcludedService));
        }

        #region Invoice
        /// <summary>
        /// Get Invoices list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] string filters)
        {
            List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            BaseListDto dto = await _invoiceService.GetPagedList(resultFilter.TransferToDtoModel());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get Invoice by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            InvoiceDto dto = await _invoiceService.GetById(id);
            return Ok(dto?.TransferToViewModel());
        }


        /// <summary>
        /// Get Invoice Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetInvoiceDetails([FromRoute] int id)
        {
            List<ModuleDetailDto> dtos = await _invoiceService.GetDetails(id);
            return Ok(dtos?.TransferToModuleDetailViewModel());
        }


        /// <summary>
        /// Create Invoice
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(InvoiceViewModel model)
        {
            InvoiceDto dto = await _invoiceService.Create(model.TransferToDto());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update Invoice
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, InvoiceViewModel model)
        {
            InvoiceDto dto = model.TransferToDto();
            dto.Id = id;
            InvoiceDto result = await _invoiceService.Update(dto);
            return Ok(result.TransferToViewModel());
        }

        /// <summary>
        /// Delete Invoice
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _invoiceService.Delete(id);
            return Ok(result);
        }

        /// <summary>
        /// Generate Documents
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/document/generate")]
        public async Task<IActionResult> GenerateDocument(int id)
        {
            await _invoiceService.GenerateInvoice(id);
            return Ok(true);
        }

        /// <summary>
        /// Get Invoice Documents
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/document")]
        [SwaggerResponse(200, "", typeof(FileContentResult))]
        [ProducesResponseType(typeof(FileContentResult), 200)]
        public async Task<IActionResult> GetDocument(int id)
        {

            byte[] pdf = await _invoiceService.GetInvoiceDocument(id);

            // byte[] pdf = _converter.Convert(doc);

            return new FileContentResult(pdf, "application/pdf");
        }

        [HttpPost("calculate_total")]
        public async Task<IActionResult> CalculateTotal(InvoiceViewModel model)
        {
            InvoiceTotalDto dto = await _invoiceService.CalculateTotal(model.TransferToDto());
            return Ok(dto); // TransferToViewModel
        }

        #endregion
    }
}