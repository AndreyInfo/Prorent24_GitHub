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
        /// <summary>
        /// Get Equipment PeriodicInspections 
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}/periodic_inspections")]
        public async Task<IActionResult> GetEquipmentPeriodicInspection([FromRoute]int equipmentId)
        {
            BaseListDto dto = await _equipmentPeriodicInspectionService.GetPagedList(equipmentId);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update Equipment PeriodicInspections 
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/periodic_inspections")]
        public async Task<IActionResult> UpdateEquipmentPeriodicInspection([FromRoute]int equipmentId, [FromBody] EquipmentPeriodicInspectionViewModel model)
        {
            EquipmentPeriodicInspectionDto dto = model.TransferToDto();
            dto.EquipmentId = equipmentId;
            EquipmentPeriodicInspectionDto result = await _equipmentPeriodicInspectionService.Save(dto);
            return Ok(result.TransferToViewModel());
        }
    }
}
