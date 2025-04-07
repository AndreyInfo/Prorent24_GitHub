using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models.General.Document;
using Prorent24.BLL.Services.General.Document;
using Prorent24.Enum.Configuration;
using Prorent24.WebApp.Models.General.Document;
using Prorent24.WebApp.Transfers.General;
using Prorent24.WebApp.Transfers.Project;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.General
{
    [Route("api/documents")]
    [ApiController]
    [SwaggerTag("Documents")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService ?? throw new ArgumentNullException(nameof(documentService));
        }

        /// <summary>
        /// Generate Empty Document by type
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost("{documentType}/create")]
        public async Task<IActionResult> CreateDocument([FromRoute]DocumentTemplateTypeEnum documentType, [FromQuery]int? projectId, [FromQuery] int? invoiceId)
        {
            var result = await _documentService.CreateDocument(documentType, projectId);
            var createResult = result.TransferToViewModel();
            return Ok(createResult);
        }

        /// <summary>
        /// Get Document Info by type
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById([FromRoute] int id)
        {
            var result = await _documentService.GetById(id);
            var createResult = result.TransferToViewModel();
            return Ok(createResult);
        }

        /// <summary>
        /// Get Document 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateDocumentById([FromRoute] int id, [FromBody] DocumentViewModel model)
        {
            DocumentDto dto = model.TransferToDto();
            dto.Id = id;
            var result = await _documentService.Update(dto);
            var createResult = result.TransferToViewModel();
            return Ok(createResult);
        }

        /// <summary>
        /// Get Document File by Id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{id}/file")]
        [SwaggerResponse(200, "", typeof(FileContentResult))]
        [ProducesResponseType(typeof(FileContentResult), 200)]
        public async Task<IActionResult> GetDocumentFileById([FromRoute] int id)
        {
            byte[] pdf = await _documentService.GetDocumentFileById(id);
            // var createResult = result.TransferToViewModel();
            return new FileContentResult(pdf, "application/pdf");
        }
    }
}
