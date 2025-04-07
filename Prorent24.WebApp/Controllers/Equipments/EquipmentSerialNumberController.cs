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
        #region EQUIPMENT SERIALNUMBERS
        /// <summary>
        /// Get list SerialNumbers
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}/serial_numbers")]
        public async Task<IActionResult> GetSerialNumberList([FromRoute]int equipmentId) // , [FromQuery]string filters
        {
            // List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            BaseListDto dto = await _equipmentSerialNumberService.GetPagedList(equipmentId);  //GetPagedList(resultFilter.TransferToDtoModel());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get SerialNumber by Id
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}/serial_numbers/{id}")]
        public async Task<IActionResult> GetSerialNumberById([FromRoute]int equipmentId, int id)
        {
            EquipmentSerialNumberDto dto = await _equipmentSerialNumberService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Create Equipment SerialNumber
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/serial_numbers")]
        public async Task<IActionResult> CreateSerialNumber([FromRoute]int equipmentId, EquipmentSerialNumberViewModel model)
        {
            model.EquipmentId = equipmentId;
            EquipmentSerialNumberDto dto = await _equipmentSerialNumberService.Create(model.TransferToDto());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update Equipment SerialNumber
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/serial_numbers/{id}")]
        public async Task<IActionResult> UpdateSerialNumber([FromRoute]int equipmentId, [FromRoute]int id, EquipmentSerialNumberViewModel model)
        {
            model.EquipmentId = equipmentId;
            EquipmentSerialNumberDto dto = await _equipmentSerialNumberService.Update(model.TransferToDto());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Delete Equipment SerialNumber
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/serial_numbers/{id}/delete")]
        public async Task<IActionResult> DeleteSerialNumber([FromRoute]int equipmentId, [FromRoute] int id)
        {
            bool result = await _equipmentSerialNumberService.Delete(id);
            return Ok(result);
        }
        #endregion
    }
}
