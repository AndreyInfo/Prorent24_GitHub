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
        #region EQUIPMENT SUPPLIERS
        /// <summary>
        /// Get list Suppliers
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}/suppliers")]
        public async Task<IActionResult> GetSupplierList([FromRoute]int equipmentId) //, [FromQuery]string filters
        {
            // List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            BaseListDto dto = await _equipmentSupplierService.GetPagedList(equipmentId);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get Supplier by Id
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}/suppliers/{id}")]
        public async Task<IActionResult> GetSupplierById([FromRoute]int equipmentId, int id)
        {
            EquipmentSupplierDto dto = await _equipmentSupplierService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }


        /// <summary>
        /// Create Equipment Supplier
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/suppliers")]
        public async Task<IActionResult> CreateSupplier([FromRoute]int equipmentId, EquipmentSupplierViewModel model)
        {
            EquipmentSupplierDto dto = model.TransferToDto();
            dto.EquipmentId = equipmentId;
            EquipmentSupplierDto result = await _equipmentSupplierService.Create(dto);
            return Ok(result.TransferToViewModel());
        }

        /// <summary>
        /// Update Equipment Supplier
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/suppliers/{id}")]
        public async Task<IActionResult> UpdateSupplier([FromRoute]int equipmentId, [FromRoute]int id, EquipmentSupplierViewModel model)
        {
            EquipmentSupplierDto dto = model.TransferToDto();
            dto.EquipmentId = equipmentId;
            dto.Id = id;
            EquipmentSupplierDto result = await _equipmentSupplierService.Update(model.TransferToDto());
            return Ok(result.TransferToViewModel());
        }

        /// <summary>
        /// Delete Equipment Supplier
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/suppliers/{id}/delete")]
        public async Task<IActionResult> DeleteSupplier([FromRoute]int equipmentId, [FromRoute] int id)
        {
            bool result = await _equipmentSupplierService.Delete(id);
            return Ok(result);
        }
        #endregion

    }
}
