using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Equipment;
using Prorent24.WebApp.Models.Equipment;
using Prorent24.WebApp.Models.General.Filter;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Equipment;
using Prorent24.WebApp.Transfers.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers
{
    public partial class EquipmentController
    {
        #region EQUIPMENT ALTERNATIVES

        /// <summary>
        /// Get list Alternatives
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}/alternatives")]
        public async Task<IActionResult> GetAlternativesList([FromRoute]int equipmentId)
        {
            //List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            BaseListDto dto = await _equipmentAlternativeService.GetPagedList(equipmentId);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get Alternative by Id
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}/alternatives/{id}")]
        public async Task<IActionResult> GetAlternativeById([FromRoute]int equipmentId, int id)
        {
            EquipmentAlternativeDto dto = await _equipmentAlternativeService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }


        /// <summary>
        /// Create Equipment Alternative
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/alternatives")]
        public async Task<IActionResult> CreateAlternative([FromRoute]int equipmentId, EquipmentAlternativeViewModel model)
        {
            if (ModelState.IsValid && model.AlternativeId != equipmentId)
            {
                EquipmentAlternativeDto dto = model.TransferToDto();
                dto.EquipmentId = equipmentId;
                EquipmentAlternativeDto result = await _equipmentAlternativeService.Create(dto);
                return Ok(result.TransferToViewModel());
            }
            else if (ModelState.IsValid && model.AlternativeId == equipmentId)
            {
                return BadRequest("Equipment equal to  Alternative");
            }

            var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
            return BadRequest(errors);
        }

        /// <summary>
        /// Update Equipment Alternative
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/alternatives/{id}")]
        public async Task<IActionResult> UpdateAlternative([FromRoute]int equipmentId, [FromRoute]int id, EquipmentAlternativeViewModel model)
        {
            EquipmentAlternativeDto dto = model.TransferToDto();
            dto.EquipmentId = equipmentId;
            dto.Id = id;
            EquipmentAlternativeDto result = await _equipmentAlternativeService.Update(dto);
            return Ok(result.TransferToViewModel());
        }

        /// <summary>
        /// Delete Equipment Alternative
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/alternatives/{id}/delete")]
        public async Task<IActionResult> DeleteAlternative([FromRoute]int equipmentId, [FromRoute]int id)
        {
            bool result = await _equipmentAlternativeService.Delete(id);
            return Ok(result);
        }

        #endregion
    }
}
