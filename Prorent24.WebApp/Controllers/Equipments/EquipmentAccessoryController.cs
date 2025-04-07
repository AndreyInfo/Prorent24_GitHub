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
        #region EQUIPMENT ACCESSORIES
        /// <summary>
        /// Get list Accessories
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}/accessories")]
        public async Task<IActionResult> GetAccessoryList([FromRoute]int equipmentId) //  [FromQuery]string filters
        {
            //List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            BaseListDto dto = await _equipmentAccessoryService.GetPagedList(equipmentId);  //GetPagedList(resultFilter.TransferToDtoModel());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get Accessory by Id
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}/accessories/{id}")]
        public async Task<IActionResult> GetAccessoryById([FromRoute]int equipmentId, int id)
        {
            EquipmentAccessoryDto dto = await _equipmentAccessoryService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }


        /// <summary>
        /// Create Equipment Accessory
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/accessories")]
        public async Task<IActionResult> CreateAccessory([FromRoute]int equipmentId, EquipmentAccessoryViewModel model)
        {
            if (ModelState.IsValid && model.AccessoryId != equipmentId)
            {
                EquipmentAccessoryDto dto = model.TransferToDto();
                dto.EquipmentId = equipmentId;
                dto.Quantity = dto.Quantity == 0 ? 1 : dto.Quantity;
                EquipmentAccessoryDto result = await _equipmentAccessoryService.Create(dto);

                return Ok(result.TransferToViewModel());
            }
            else if (ModelState.IsValid && model.AccessoryId == equipmentId)
            {
                return BadRequest("Equipment equal to  Accessory");
            }

            var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
            return BadRequest(errors);
        }

        /// <summary>
        /// Update Equipment Accessory
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/accessories/{id}")]
        public async Task<IActionResult> UpdateAccessory([FromRoute]int equipmentId, [FromRoute]int id, EquipmentAccessoryViewModel model)
        {
            model.EquipmentId = equipmentId;
            EquipmentAccessoryDto dto = await _equipmentAccessoryService.Update(model.TransferToDto());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Delete Equipment Accessory
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/accessories/{id}/delete")]
        public async Task<IActionResult> DeleteAccessory([FromRoute]int equipmentId, [FromRoute] int id)
        {
            bool result = await _equipmentAccessoryService.Delete(id);
            return Ok(result);
        }


        #endregion
    }
}
