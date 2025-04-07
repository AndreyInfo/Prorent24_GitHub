using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Equipment;
using Prorent24.WebApp.Models.Equipment;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Equipment;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers
{
    public partial class EquipmentController
    {
        #region EQUIPMENT STOCK

        /// <summary>
        /// Get list Stocks
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}/stock")]
        public async Task<IActionResult> GetStocksList([FromRoute]int equipmentId)
        {
            BaseListDto dto = await _equipmentStockService.GetPagedList(equipmentId);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get Stock by Id
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}/stock/{id}")]
        public async Task<IActionResult> GetStockById([FromRoute]int equipmentId, int id)
        {
            EquipmentStockDto dto = await _equipmentStockService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }


        /// <summary>
        /// Create Equipment Stock
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/stock")]
        public async Task<IActionResult> CreateStock([FromRoute]int equipmentId, EquipmentStockViewModel model)
        {
            if (ModelState.IsValid)
            {
                EquipmentStockDto dto = model.TransferToDto();
                dto.EquipmentId = equipmentId;
                EquipmentStockDto result = await _equipmentStockService.Create(dto);
                return Ok(result.TransferToViewModel());
            }

            var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
            return BadRequest(errors);
        }

        /// <summary>
        /// Create Equipment Stock
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/stock/correct")]
        public async Task<IActionResult> CorrectStock([FromRoute]int equipmentId, EquipmentStockCorrectViewModel model)
        {
            if (ModelState.IsValid)
            {
                EquipmentStockCorrectDto dto = model.TransferToDto();
                dto.EquipmentId = equipmentId;
                EquipmentStockDto result = await _equipmentStockService.Correct(dto);
                return Ok(result.TransferToViewModel());
            }

            var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
            return BadRequest(errors);
        }

        /// <summary>
        /// Update Equipment Stock
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/stock/{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute]int equipmentId, [FromRoute]int id, EquipmentStockViewModel model)
        {
            EquipmentStockDto dto = model.TransferToDto();
            dto.EquipmentId = equipmentId;
            dto.Id = id;
            EquipmentStockDto result = await _equipmentStockService.Update(dto);
            return Ok(result.TransferToViewModel());
        }

        /// <summary>
        /// Delete Equipment Stock
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/stock/{id}/delete")]
        public async Task<IActionResult> DeleteStock([FromRoute]int equipmentId, [FromRoute]int id)
        {
            bool result = await _equipmentStockService.Delete(id);
            return Ok(result);
        }

        #endregion
    }
}
