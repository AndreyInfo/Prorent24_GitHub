using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.BLL.Services.Configuration.CustomerCommunication.Letterhead;
using Prorent24.WebApp.Models.Configuration.CustomerCommunication;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Configurations.CustomerCommunication;
using Swashbuckle.AspNetCore.Annotations;

namespace Prorent24.WebApp.Controllers.Configurations.CustomerCommunication
{
    [Route("api/configuration/customer_communication/[controller]")]
    [ApiController]
    [SwaggerTag("Configuration -> CustomerCommunication")]
    public class LetterheadsController : ControllerBase
    {
        private readonly ILetterheadService _letterheadService;

        public LetterheadsController(ILetterheadService letterheadService)
        {
            _letterheadService = letterheadService;
        }
        /// <summary>
        /// Get list Letterheads
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetLetterheads()
        {
            BaseListDto dto = await _letterheadService.GetPagedList(null);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get Letterhead by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLetterheadById(int id)
        {
            LetterheadDto dto = await _letterheadService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Create Letterhead
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateDocumentTamplate(LetterheadViewModel model)
        {
            LetterheadDto dto = await _letterheadService.Create(model.TransferToDto());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update Letterhead
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateLetterhead(LetterheadViewModel model)
        {
            LetterheadDto dto = await _letterheadService.Update(model.TransferToDto());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Delete Letterhead
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteLetterhead(int id)
        {
            bool result = await _letterheadService.Delete(id);
            return Ok(result);
        }

    }
}