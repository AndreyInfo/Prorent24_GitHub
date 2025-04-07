using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.BLL.Services.Configuration.CustomerCommunication.DocumentTemplate;
using Prorent24.Enum.Configuration;
using Prorent24.WebApp.Models.Configuration.CustomerCommunication;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Configurations.CustomerCommunication;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.Configurations.CustomerCommunication
{
    /// <summary>
    /// Configuration document templates
    /// </summary>
    [Route("api/configuration/customer_communication/document_templates")]
    [ApiController]
    [SwaggerTag("Configuration -> CustomerCommunication")]
    public class DocumentTemplateController : ControllerBase
    {
        private readonly IDocumentTemplateService _documentTemplateService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="documentTemplateService"></param>
        public DocumentTemplateController(IDocumentTemplateService documentTemplateService)
        {
            _documentTemplateService = documentTemplateService;
        }

        /// <summary>
        /// Get list DocumnetTemplates
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetDocumentTemplates()
        {
            BaseListDto dto = await _documentTemplateService.GetPagedList(null);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get DocumnetTemplate by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentTemplateById(int id)
        {
            DocumentTemplateDto dto = await _documentTemplateService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get DocumnetTemplate by Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetDocumentTemplateByTypeId(DocumentTemplateTypeEnum type)
        {
            List<DocumentTemplateDto> dtos = await _documentTemplateService.GetByTypeAsync(type);
            return Ok(dtos.TransferToViewModels());
        }

        /// <summary>
        /// Create Document Template
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateDocumentTamplate(DocumentTemplateViewModel model)
        {
            DocumentTemplateDto dto = await _documentTemplateService.Create(model.TransferToDto());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update Document Template
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateDocumentTemplate(DocumentTemplateViewModel model)
        {
            DocumentTemplateDto dto = await _documentTemplateService.Update(model.TransferToDto());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Delete Document Template
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteDocumentTemplate(int id)
        {
            bool result = await _documentTemplateService.Delete(id);
            return Ok(result);
        }


        #region DocumentBlock


        /// <summary>
        /// Create Document Template Block
        /// </summary>
        /// <param name="documentTemplateId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{documentTemplateId}/blocks")]
        public async Task<IActionResult> CreateDocumentTamplateBlock([FromRoute]int documentTemplateId, DocumentTemplateBlockViewModel model)
        {
            var inputDto = model.TransferToDto();
            inputDto.DocumentTemplateId = documentTemplateId;
            DocumentTemplateBlockDto dto = await _documentTemplateService.CreateBlock(inputDto);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update Document Template Block
        /// </summary>
        /// <param name="documentTemplateId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{documentTemplateId}/blocks/{id}")]
        public async Task<IActionResult> UpdateDocumentTemplateBlock([FromRoute]int documentTemplateId, [FromRoute]int id,  DocumentTemplateBlockViewModel model)
        {
            var inputDto = model.TransferToDto();
            inputDto.Id = id;
            inputDto.DocumentTemplateId = documentTemplateId;
            DocumentTemplateBlockDto dto = await _documentTemplateService.UpdateBlock(inputDto);
           
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Delete Document Template Block
        /// </summary>
        /// <param name="documentTemplateId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{documentTemplateId}/blocks/{id}/delete")]
        public async Task<IActionResult> DeleteDocumentTemplateBlock([FromRoute]int documentTemplateId, [FromRoute]int id)
        {
            bool result = await _documentTemplateService.DeleteBlock(id);
            return Ok(result);
        }
        


        #endregion
    }
}
