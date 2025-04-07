using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.Common.Models.Filters;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.General.Filter;
using Prorent24.WebApp.Transfers.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.General
{
    public partial class GeneralController : ControllerBase
    {
        /// <summary>
        /// Get list filters by Module
        /// </summary>
        /// <returns></returns>
        [HttpGet("filters/{moduleType}")]
        public async Task<IActionResult> GetListFilters(ModuleTypeEnum moduleType)
        {
            List<FilterListDto> listDto = await _filterService.GetListFilters(moduleType);
            return Ok(listDto.TransferToViewModel());
        }

        /// <summary>
        /// Save new filter
        /// </summary>
        /// <returns></returns>
        [HttpPost("filters")]
        public async Task<IActionResult> SaveFilter(SavedFilterViewModel model)
        {
            SavedFilterDto dto = model.TransferToDtoModel();
            dto = await _filterService.SaveFilter(dto);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Delete saved filter
        /// </summary>
        /// <returns></returns>
        [HttpPost("filters/{id}/delete")]
        public async Task<IActionResult> DeleteFilter(int id)
        {
            bool result = await _filterService.DeleteFilter(id);
            return Ok(result);
        }
    }
}