using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models.Invoice;
using Prorent24.WebApp.Models.Invoice;
using System.Threading.Tasks;
using Prorent24.WebApp.Transfers.Invoice;

namespace Prorent24.WebApp.Controllers
{
    public partial class InvoiceController : ControllerBase
    {
        /// <summary>
        /// Get InvoiceExcluded by Id
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{invoiceId}/exclusions/{id}")]
        public async Task<IActionResult> GetInvoiceExcludedById([FromRoute]int invoiceId, [FromRoute] int id)
        {
            InvoiceExcludedDto dto = await _invoiceExcludedService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Create InvoiceExcluded
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{invoiceId}/exclusions")]
        public async Task<IActionResult> CreateInvoiceExcluded([FromRoute]int invoiceId, InvoiceExcludedViewModel model)
        {
            InvoiceExcludedDto dto = model.TransferToDto();
            dto.InvoiceId = invoiceId;
            InvoiceExcludedDto result = await _invoiceExcludedService.Create(dto);
            return Ok(result.TransferToViewModel());
        }

        /// <summary>
        /// Update InvoiceExcluded
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{invoiceId}/exclusions/{id}")]
        public async Task<IActionResult> UpdateInvoiceExcluded([FromRoute]int invoiceId, [FromRoute]int id, [FromBody]InvoiceExcludedViewModel model)
        {
            InvoiceExcludedDto dto = model.TransferToDto();
            dto.Id = id;
            dto.InvoiceId = invoiceId;
            InvoiceExcludedDto result = await _invoiceExcludedService.Update(model.TransferToDto());
            return Ok(result.TransferToViewModel());
        }

        /// <summary>
        /// Delete InvoiceExcluded
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{invoiceId}/exclusions/{id}/delete")]
        public async Task<IActionResult> DeleteInvoiceExcluded([FromRoute]int invoiceId, [FromRoute]int id)
        {
            bool result = await _invoiceExcludedService.Delete(id);
            return Ok(result);
        }
    }
}
