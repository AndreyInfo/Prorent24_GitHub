using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Subhire;
using Prorent24.BLL.Services.Subhire;
using Prorent24.WebApp.Models.General.Filter;
using Prorent24.WebApp.Models.Subhire;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.General;
using Prorent24.WebApp.Transfers.Modules;
using Prorent24.WebApp.Transfers.Subhire;

namespace Prorent24.WebApp.Controllers
{
    [Route("api/subhires")]
    [ApiController]
    public class SubhireController : ControllerBase
    {
        private readonly ISubhireService _subhireService;
        private readonly ISubhireEquipmentGroupService _subhireEquipmentGroupService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="subhireService"></param>
        /// <param name="subhireEquipmentGroupService"></param>
        public SubhireController(ISubhireService subhireService, ISubhireEquipmentGroupService subhireEquipmentGroupService)
        {
            _subhireService = subhireService;
            _subhireEquipmentGroupService = subhireEquipmentGroupService;
        }


        #region Subhire
        /// <summary>
        /// Get list Subhires
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetSubhires([FromQuery]string filters)
        {
            List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            BaseListDto dto = await _subhireService.GetPagedList(resultFilter.TransferToDtoModel());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get Subhire by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubhireById(int id)
        {
            SubhireDto dto = await _subhireService.GetById(id);
            return Ok(dto?.TransferToViewModel());
        }

        /// <summary>
        /// Get Subhires by projects in some period 
        /// </summary>
        /// <returns></returns>
        [HttpGet("byProjects")]
        public async Task<IActionResult> GetSubhiresByProject([FromQuery]List<int> projectIds)
        {
            SubhireListDto dto = await _subhireService.GetSubhiresByProject(projectIds);
            return Ok(dto?.TransferToViewModel());
        }


        /// <summary>
        /// Create Subhire
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateSubhire(SubhireViewModel model)
        {
            try
            {
                SubhireDto dto = await _subhireService.Create(model.TransferToDto());
                return Ok(dto.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Save Subhire FromShortage
        /// </summary>
        /// <param name="subhireId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("fromShortage")]
        [HttpPost("fromShortage/{subhireId}")]
        public async Task<IActionResult> SaveSubhireFromShortage([FromRoute]int? subhireId, [FromBody]SubhireModifiedEquipmentModel model)
        {
            try
            {
                SubhireDto dto = await _subhireService.SaveFromShortage(subhireId, model.Equipments.Select(x => x.TransferToDto()).ToList(), model.DateFrom, model.DateTo);
                return Ok(dto.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Update Subhire
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateSubhire(int id, SubhireViewModel model)
        {
            try
            {
                SubhireDto dto = model.TransferToDto();
                dto.Id = id;
                SubhireDto result = await _subhireService.Update(dto);
                return Ok(result.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Get Subhire Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetSubhireDetails(int id)
        {
            List<ModuleDetailDto> dtos = await _subhireService.GetDetails(id);
            return Ok(dtos?.TransferToModuleDetailViewModel());
        }

        /// <summary>
        /// Delete Subhire
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteSubhire(int id)
        {
            bool result = await _subhireService.Delete(id);
            return Ok(result);
        }
        #endregion

        #region SubhireGroup
        /// <summary>
        /// Create SubhireEquipmentGroup
        /// </summary>
        /// <param name="subhireId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{subhireId}/groups")]
        public async Task<IActionResult> CreateSubhireEquipmentGroup([FromRoute]int subhireId, [FromBody]SubhireEquipmentGroupViewModel model)
        {
            try
            {
                SubhireEquipmentGroupDto dto = model.TransferToDto();
                dto.SubhireId = subhireId;
                SubhireEquipmentGroupDto result = await _subhireEquipmentGroupService.CreateGroup(dto);
                return Ok(result.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Get SubhireEquipmentGroup By Id
        /// </summary>
        /// <param name="subhireId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("{subhireId}/groups/{id}")]
        public async Task<IActionResult> GetSubhireEquipmentGroup([FromRoute]int subhireId, [FromRoute]int id)
        {
            try
            {
                SubhireEquipmentGroupDto result = await _subhireEquipmentGroupService.GetGroupById(id);
                return Ok(result.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Update SubhireEquipmentGroup
        /// </summary>
        /// <param name="subhireId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{subhireId}/groups/{id}")]
        public async Task<IActionResult> UpdateSubhireEquipmentGroup([FromRoute]int subhireId, [FromRoute]int id, SubhireEquipmentGroupViewModel model)
        {
            try
            {
                SubhireEquipmentGroupDto dto = model.TransferToDto();
                dto.Id = id;
                dto.SubhireId = subhireId;
                SubhireEquipmentGroupDto result = await _subhireEquipmentGroupService.UpdateGroup(dto);
                return Ok(result?.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Get SubhireEquipmentGroups by subhireId
        /// </summary>
        /// <param name="subhireId"></param>
        /// <returns></returns>
        [HttpGet("{subhireId}/groups")]
        public async Task<IActionResult> GetEquipmentGroupBySubhire([FromRoute]int subhireId)
        {
            List<SubhireEquipmentGroupDto> dto = await _subhireEquipmentGroupService.GetAllGroupBySubhire(subhireId);
            return Ok(dto.TransferToListViewModel());
        }


        /// <summary>
        /// Delete SubhireEquipmentGroups
        /// </summary>
        /// <param name="subhireId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{subhireId}/groups/{id}/delete")]
        public async Task<IActionResult> DeleteSubhireEquipmentGroup([FromRoute]int subhireId, [FromRoute]int id)
        {
            bool result = await _subhireEquipmentGroupService.DeleteGroup(id);
            return Ok(result);
        }

        #endregion

        #region SubhireEquipment

        /// <summary>
        /// Get SubhireEquipments
        /// </summary>
        /// <param name="subhireId"></param>
        /// <returns></returns>
        [HttpGet("{subhireId}/equipments")]
        public async Task<IActionResult> GetSubhireEquipments([FromRoute]int subhireId)
        {
            BaseListDto dto = await _subhireEquipmentGroupService.GetEquipmentsBySubhire(subhireId);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Create SubhireEquipment
        /// </summary>
        /// <param name="subhireId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{subhireId}/equipments")]
        public async Task<IActionResult> CreateSubhireEquipment([FromRoute]int subhireId, [FromBody]SubhireEquipmentViewModel model)
        {
            try
            {
                SubhireEquipmentDto dto = model.TransferToDto();
                dto.SubhireId = subhireId;
                SubhireEquipmentGridDto result = await _subhireEquipmentGroupService.CreateEquipment(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Update SubhireEquipment
        /// </summary>
        /// <param name="subhireId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{subhireId}/equipments/{id}")]
        public async Task<IActionResult> UpdateSubhireEquipment([FromRoute]int subhireId, [FromRoute]int id, SubhireEquipmentViewModel model)
        {
            try
            {
                SubhireEquipmentDto dto = model.TransferToDto();
                dto.Id = id;
                dto.SubhireId = subhireId;
                SubhireEquipmentGridDto result = await _subhireEquipmentGroupService.UpdateEquipment(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Delete SubhireEquipment
        /// </summary>
        /// <param name="subhireId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{subhireId}/equipments/{id}/delete")]
        public async Task<IActionResult> DeleteSubhireEquipment([FromRoute]int subhireId, [FromRoute]int id)
        {
            bool result = await _subhireEquipmentGroupService.DeleteEquipment(id);
            return Ok(result);
        }
        #endregion

        #region Shortages

        /// <summary>
        /// Get Shortages list
        /// </summary>
        /// <returns></returns>
        [HttpGet("shortages")]
        public async Task<IActionResult> GetShortages([FromQuery]string filters)
        {
            List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            BaseListDto dto = await _subhireEquipmentGroupService.GetShortages(resultFilter.TransferToDtoModel());
            return Ok(dto?.TransferToViewModel());
        }

        /// <summary>
        /// Get Shortage Details
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="equipmentId"></param>
        /// <param name="subhireIds"></param>
        /// <returns></returns>
        [HttpGet("shortage/details")]
        public async Task<IActionResult> GetShortageDetails([FromQuery]int projectId, [FromQuery]int equipmentId, [FromQuery]int[] subhireIds)
        {
            List<ModuleDetailDto> dtos = await _subhireService.GetShortageDetails(projectId, equipmentId, subhireIds);
            return Ok(dtos.TransferToModuleDetailViewModel());

        }
        #endregion
    }
}