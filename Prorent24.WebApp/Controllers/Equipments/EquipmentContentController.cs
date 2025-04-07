using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Equipment;
using Prorent24.WebApp.Models.Equipment;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Equipment;
using Prorent24.WebApp.Transfers.General;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers
{
    public partial class EquipmentController
    {
        #region EQUIPMENT CONTENTS
        /// <summary>
        /// Get list Contents
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}/contents")]
        public async Task<IActionResult> GetContentList([FromRoute]int equipmentId) // , [FromQuery]string filters
        {
            //List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            BaseListDto dto = await _equipmentContentService.GetPagedList(equipmentId);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get Content by Id
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}/contents/{id}")]
        public async Task<IActionResult> GetContentById([FromRoute]int equipmentId, [FromRoute]int id)
        {
            EquipmentContentDto dto = await _equipmentContentService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }


        /// <summary>
        /// Create Equipment Content
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/contents")]
        public async Task<IActionResult> CreateContent([FromRoute]int equipmentId, EquipmentContentViewModel model)
        {
            if (ModelState.IsValid && model.ContentId != equipmentId)
            {
                EquipmentContentDto dto = model.TransferToDto();
                dto.EquipmentId = equipmentId;
                dto.Quantity = dto.Quantity == 0 ? 1 : dto.Quantity;
                EquipmentContentDto result = await _equipmentContentService.Create(dto);
                return Ok(result.TransferToViewModel());
            }
            else if (ModelState.IsValid && model.ContentId == equipmentId)
            {
                return BadRequest("Equipment equal to  Content");
            }

            var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
            return BadRequest(errors);
        }

        /// <summary>
        /// Update Equipment Content
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/contents/{id}")]
        public async Task<IActionResult> UpdateContent([FromRoute]int equipmentId, [FromRoute]int id, EquipmentContentViewModel model)
        {
            model.EquipmentId = equipmentId;
            EquipmentContentDto dto = await _equipmentContentService.Update(model.TransferToDto());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Delete Equipment Content
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/contents/{id}/delete")]
        public async Task<IActionResult> DeleteContent([FromRoute]int equipmentId, int id)
        {
            bool result = await _equipmentContentService.Delete(id);
            return Ok(result);
        }


        #endregion
    }
}
