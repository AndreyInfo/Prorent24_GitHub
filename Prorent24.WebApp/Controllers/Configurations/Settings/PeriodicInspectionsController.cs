using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.BLL.Services.Configuration.Settings.PeriodicInspection;
using Prorent24.WebApp.Models.Configuration.Settings;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Prorent24.WebApp.Transfers.Configurations.Settings;
using Prorent24.WebApp.Transfers;
using System.Collections.Generic;

namespace Prorent24.WebApp.Controllers.Configurations.Settings
{
    [Route("api/configuration/settings/project_inspections")]
    [ApiController]
    [SwaggerTag("Configuration -> Settings")]
    public class PeriodicInspectionsController : ControllerBase
    {
        private readonly IPeriodicInspectionService _periodicInspectionService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="periodicInspectionService"></param>
        public PeriodicInspectionsController(IPeriodicInspectionService periodicInspectionService)
        {
            _periodicInspectionService = periodicInspectionService;
        }

        /// <summary>
        /// Get list PeriodicInspections
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPeriodicInspections()
        {
            BaseListDto dto = await _periodicInspectionService.GetPagedList(null);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get PeriodicInspection by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPeriodicInspectionById(int id)
        {
            PeriodicInspectionDto dto = await _periodicInspectionService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Create PeriodicInspection
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateDocumentTamplate(PeriodicInspectionViewModel model)
        {
            PeriodicInspectionDto dto = await _periodicInspectionService.Create(model.TransferToDto());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update PeriodicInspection
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdatePeriodicInspection(PeriodicInspectionViewModel model)
        {
            PeriodicInspectionDto dto = await _periodicInspectionService.Update(model.TransferToDto());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Delete PeriodicInspection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeletePeriodicInspection(int id)
        {
            bool result = await _periodicInspectionService.Delete(id);
            return Ok(result);
        }

        /// <summary>
        /// Delete PeriodicInspection Multiple
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> DeletePeriodicInspectionMultiple(List<int> ids)
        {
            bool result = await _periodicInspectionService.DeleteMultiple(ids);
            return Ok(result);
        }
    }
}
