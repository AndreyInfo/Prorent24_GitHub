using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models.Equipment;
using Prorent24.WebApp.Models.Equipment;
using Prorent24.WebApp.Transfers.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers
{
    public partial class EquipmentController
    {
        /// <summary>
        /// Get Equipment WebShop
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}/web_shop")]
        public async Task<IActionResult> GetEquipmentWebShop([FromRoute]int equipmentId)
        {
            EquipmentWebShopDto dto = await _equipmentWebShopService.GetByEquipmentId(equipmentId);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update Equipment WebShop 
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/web_shop")]
        public async Task<IActionResult> UpdateEquipmentWebShop([FromRoute]int equipmentId, [FromBody] EquipmentWebShopViewModel model)
        {
            EquipmentWebShopDto dto = model.TransferToDto();
            dto.EquipmentId = equipmentId;
            EquipmentWebShopDto result = await _equipmentWebShopService.Save(dto);
            return Ok(result.TransferToViewModel());
        }
    }
}
