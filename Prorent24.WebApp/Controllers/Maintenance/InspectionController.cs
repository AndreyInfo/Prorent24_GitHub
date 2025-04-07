using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Maintenance;
using Prorent24.BLL.Services.Maintenance.Inspection;
using Prorent24.BLL.Services.Maintenance.Inspection.SerialNumber;
using Prorent24.WebApp.Models.General.Filter;
using Prorent24.WebApp.Models.Maintenance.Inspection;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.General;
using Prorent24.WebApp.Transfers.Maintenance.Inspection;
using Prorent24.WebApp.Transfers.Maintenance.InspectionSerialNumber;
using Prorent24.WebApp.Transfers.Modules;

namespace Prorent24.WebApp.Controllers.Maintenance
{
    [Route("api/inspections")]
    [ApiController]
    public class InspectionController : ControllerBase
    {
        private readonly IInspectionService _inspectionService;
        private readonly IInspectionSerialNumberService _inspectionSerialNumberService;

        public InspectionController(IInspectionService inspectionService, IInspectionSerialNumberService inspectionSerialNumberService)
        {
            _inspectionService = inspectionService ?? throw new ArgumentNullException(nameof(inspectionService));
            _inspectionSerialNumberService = inspectionSerialNumberService ?? throw new ArgumentNullException(nameof(inspectionSerialNumberService));
        }
        #region INSPECTION
        /// <summary>
        /// Get Inspections list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] string filters)
        {
            List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            BaseListDto dto = await _inspectionService.GetPagedList(resultFilter.TransferToDtoModel());
            return Ok(dto?.TransferToViewModel());
        }

        /// <summary>
        /// Get Inspection by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            InspectionDto dto = await _inspectionService.GetById(id);
            return Ok(dto?.TransferToViewModel());
        }


        /// <summary>
        /// Get Inspection Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetInspectionDetails([FromRoute] int id)
        {
            List<ModuleDetailDto> dtos = await _inspectionService.GetDetails(id);
            return Ok(dtos?.TransferToModuleDetailViewModel());
        }


        /// <summary>
        /// Create Inspection
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(InspectionViewModel model)
        {
            InspectionDto dto = await _inspectionService.Create(model.TransferToDto());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update Inspection
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, InspectionViewModel model)
        {
            InspectionDto dto = model.TransferToDto();
            dto.Id = id;
            InspectionDto result = await _inspectionService.Update(model.TransferToDto());
            return Ok(result.TransferToViewModel());
        }

        /// <summary>
        /// Delete Inspection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _inspectionService.Delete(id);
            return Ok(result);
        }

        #endregion

        #region Inspection Serial Numbers

        /// <summary>
        /// Get InspectionSerialNumbers list
        /// </summary>
        /// <param name="inspectionId"></param>
        /// <returns></returns>
        [HttpGet("{inspectionId}/serial_numbers")]
        public async Task<IActionResult> GetSerialNumberList([FromRoute]int inspectionId)
        {
            BaseListDto result = await _inspectionSerialNumberService.GetPagedList(inspectionId);
            return Ok(result.TransferToViewModel());
        }

        /// <summary>
        /// Get InspectionSerialNumber by Id
        /// </summary>
        /// <param name="inspectionId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{inspectionId}/serial_numbers/{id}")]
        public async Task<IActionResult> GetSerialNumberById([FromRoute]int inspectionId, [FromRoute]int id)
        {
            InspectionSerialNumberDto dto = await _inspectionSerialNumberService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }


        /// <summary>
        /// Create InspectionSerialNumber
        /// </summary>
        /// <param name="inspectionId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{inspectionId}/serial_numbers")]
        public async Task<IActionResult> CreateSerialNumber([FromRoute]int inspectionId, InspectionSerialNumberViewModel model)
        {
            InspectionSerialNumberDto dto = model.TransferToDto();

            dto.InspectionId = inspectionId;
            InspectionSerialNumberDto result = await _inspectionSerialNumberService.Create(dto);
            return Ok(result.TransferToViewModel());
        }

        /// <summary>
        /// Update InspectionSerialNumber
        /// </summary>
        /// <param name="inspectionId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{inspectionId}/serial_numbers/{id}")]
        public async Task<IActionResult> UpdateSerialNumber([FromRoute]int inspectionId, [FromRoute]int id, InspectionSerialNumberViewModel model)
        {
            InspectionSerialNumberDto dto = model.TransferToDto();
            dto.InspectionId = inspectionId;
            dto.Id = id;
            InspectionSerialNumberDto result = await _inspectionSerialNumberService.Update(dto);
            return Ok(result.TransferToViewModel());
        }

        /// <summary>
        /// Delete InspectionSerialNumber
        /// </summary>
        /// <param name="inspectionId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{inspectionId}/serial_numbers/{id}/delete")]
        public async Task<IActionResult> DeleteSerialNumber([FromRoute]int inspectionId, [FromRoute]int id)
        {
            bool result = await _inspectionSerialNumberService.Delete(id);
            return Ok(result);
        }
        #endregion
    }
}