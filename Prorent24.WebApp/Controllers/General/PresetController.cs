using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Models.General.Preset;
using Prorent24.Common.Models.Filters;
using Prorent24.WebApp.Models.General.Perset;
using Prorent24.WebApp.Transfers.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.General
{
    /// <summary>
    /// Presets
    /// </summary>
    public partial class GeneralController : ControllerBase
    {
        /// <summary>
        /// get filters by presetId
        /// </summary>
        /// <returns></returns>
        [HttpGet("presets/{id}")]
        public async Task<IActionResult> GetPreset(int id)
        {
            List<FilterListDto> listDto = await _filterService.GetFiltersByPreset(id);
            return Ok(listDto.TransferToViewModel());
        }

        /// <summary>
        /// Create new preset
        /// </summary>
        /// <returns></returns>
        [HttpPost("presets")]
        public async Task<IActionResult> CreatePreset(PresetViewModel model)
        {
            PresetDto dto = model.TransferToDtoModel();
            dto = await _presetService.CreatePerset(dto);
            model = dto.TransferToViewModel();
            return Ok(model);
        }

        /// <summary>
        /// Update preset
        /// </summary>
        /// <returns></returns>
        [HttpPost("presets/{id}")]
        public async Task<IActionResult> UpdatePreset(int id, PresetViewModel model)
        {
            PresetDto dto = model.TransferToDtoModel();
            dto = await _presetService.UpdatePerset(dto);
            model = dto.TransferToViewModel();
            return Ok(model);
        }

        /// <summary>
        /// Delete preset
        /// </summary>
        /// <returns></returns>
        [HttpPost("presets/{id}/delete")]
        public async Task<IActionResult> DeletePreset(int id)
        {
            bool result = await _presetService.DeletePerset(id);
            return Ok(result);
        }
    }
}