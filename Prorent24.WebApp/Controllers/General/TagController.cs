using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models.General.Tag;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.General.Tag;
using Prorent24.WebApp.Transfers.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.General
{
    /// <summary>
    /// Tags
    /// </summary>
    public partial class GeneralController : ControllerBase
    {
        /// <summary>
        /// Create new tag
        /// </summary>
        /// <returns></returns>
        [HttpPost("tags/{entity}/{referenceId}")]
        public async Task<IActionResult> CreateTag([FromRoute] ModuleTypeEnum entity, [FromRoute] string referenceId, TagViewModel model)
        {
            model.BelongsTo = entity;
            model.ReferenceId = referenceId;

            TagDto dto = model.TransferToDtoModel();
            dto = await _tagService.CreateTag(dto);
            model = dto.TransferToViewModel();
            return Ok(model);
        }

        /// <summary>
        /// Update tag
        /// </summary>
        /// <returns></returns>
        //[HttpPost("tags/{id}")]
        //public async Task<IActionResult> UpdateTag(int id, TagViewModel model)
        //{
        //    TagDto dto = model.TransferToDtoModel();
        //    dto = await _tagService.Update(dto);
        //    model = dto.TransferToViewModel();
        //    return Ok(model);
        //}

        /// <summary>
        /// search tags 
        /// </summary>
        /// <returns></returns>
        [HttpGet("tags/{module}")] // ?search={search_string}
        public async Task<IActionResult> SearchTag(ModuleTypeEnum module, [FromQuery]string search)
        {
            List<TagDto> dtos  = await _tagService.SearchTags(module, search);
            return Ok(dtos.TransferToViewModel());
        }

        /// <summary>
        /// Delete tag
        /// </summary>
        /// <returns></returns>
        [HttpPost("tags/{id}/delete")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            bool result = await _tagService.Delete(id);
            return Ok(result);
        }
    }
}