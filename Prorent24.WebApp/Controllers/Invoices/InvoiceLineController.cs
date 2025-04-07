using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models.Invoice;
using Prorent24.WebApp.Models.Invoice;
using Prorent24.WebApp.Transfers.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers
{
    public partial class InvoiceController : ControllerBase
    {
        /// <summary>
        /// Get InvoiceLine by Id
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{invoiceId}/lines/{id}")]
        public async Task<IActionResult> GetInvoiceLineById([FromRoute]int invoiceId, [FromRoute] int id)
        {
            InvoiceLineDto dto = await _invoiceLineService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Create InvoiceLine
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{invoiceId}/lines")]
        public async Task<IActionResult> CreateInvoiceLine([FromRoute]int invoiceId, InvoiceLineViewModel model)
        {
            InvoiceLineDto dto = model.TransferToDto();
            dto.InvoiceId = invoiceId;
            InvoiceLineDto result = await _invoiceLineService.Create(dto);
            //InvoiceTotalDto total = await _invoiceService.CalculateTotal(invoiceId);
            return Ok(result.TransferToViewModel());
        }

        /// <summary>
        /// Update InvoiceLine
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{invoiceId}/lines/{id}")]
        public async Task<IActionResult> UpdateInvoiceLine([FromRoute]int invoiceId, [FromRoute]int id,[FromBody]InvoiceLineViewModel model)
        {
            InvoiceLineDto dto = model.TransferToDto();
            dto.Id = id;
            dto.InvoiceId = invoiceId;
            InvoiceLineDto result = await _invoiceLineService.Update(dto);
            //InvoiceTotalDto total = await _invoiceService.CalculateTotal(invoiceId);
            return Ok(result.TransferToViewModel());
        }

        /// <summary>
        /// Delete InvoiceLine
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{invoiceId}/lines/{id}/delete")]
        public async Task<IActionResult> DeleteInvoiceLine([FromRoute]int invoiceId, [FromRoute]int id)
        {
            bool result = await _invoiceLineService.Delete(id);
            //InvoiceTotalDto total = await _invoiceService.CalculateTotal(invoiceId);
            return Ok(result);
        }
    }
}
