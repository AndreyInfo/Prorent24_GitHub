using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Equipment;
using Prorent24.Enum.Entity;
using Prorent24.WebApp.Models.Equipment;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Equipment;
using Prorent24.WebApp.Transfers.General;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers
{
    public partial class EquipmentController
    {
        #region EQUIPMENT ALTERNATIVES

        /// <summary>
        /// Get list QRCode
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="serialNumberId"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}/qr_codes", Name = "GetQRCodesList")]
        [HttpGet("{equipmentId}/serial_numbers/{serialNumberId?}/qr_codes", Name = "GetSerialNumberQRCodesList")]
        public async Task<IActionResult> GetQRCodesList([FromRoute]int equipmentId, [FromRoute]int? serialNumberId)
        {
            //List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            BaseListDto dto = await _equipmentQRCodeService.GetPagedList(equipmentId, serialNumberId);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get QRCode by Id
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="serialNumberId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}/qr_codes/{id}", Name = "GetQRCodeById")]
        [HttpGet("{equipmentId}/serial_numbers/{serialNumberId}/qr_codes/{id?}", Name = "GetSerialNumberQRCodesById")]
        public async Task<IActionResult> GetQRCodeById([FromRoute]int equipmentId, [FromRoute]int? serialNumberId, [FromRoute]int id)
        {
            EquipmentQRCodeDto dto = await _equipmentQRCodeService.GetById(id);
            if (dto == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(dto?.TransferToViewModel());
            }
        }

        /// <summary>
        /// Create Equipment QRCode
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="serialNumberId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/qr_codes", Name = "CreateQRCode")]
        [HttpPost("{equipmentId}/serial_numbers/{serialNumberId}/qr_codes", Name = "CreateSerialNumberQRCode")]
        public async Task<IActionResult> CreateQRCode([FromRoute]int equipmentId, [FromRoute]int? serialNumberId, EquipmentQRCodeViewModel model)
        {
            EquipmentQRCodeDto dto = model.TransferToDto();
            dto.EquipmentId = equipmentId;
            dto.EquipmentSerialNumberId = serialNumberId;
            //dto.LinkedId = equipmentId;
            //dto.LinkedType = EntityEnum.EquipmentQRCodeEntity;

            EquipmentQRCodeDto result = await _equipmentQRCodeService.Create(dto);
            return Ok(result.TransferToViewModel());
        }

        /// <summary>
        /// Update Equipment QRCode
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="serialNumberId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/qr_codes/{id}", Name = "UpdateQRCode")]
        [HttpPost("{equipmentId}/serial_numbers/{serialNumberId}/qr_codes/{id}", Name = "UpdateSerialNumberQRCode")]
        public async Task<IActionResult> UpdateQRCode([FromRoute]int equipmentId, [FromRoute]int? serialNumberId, [FromRoute]int id, EquipmentQRCodeViewModel model)
        {
            EquipmentQRCodeDto dto = model.TransferToDto();
            dto.Id = id;
            // не можемо змінювати прив'язку, хіба що можливо 
            //dto.EquipmentId = equipmentId;
            //dto.EquipmentSerialNumberId = serialNumberId;
            
            //dto.LinkedId = equipmentId;
            //dto.LinkedType = EntityEnum.EquipmentQRCodeEntity;
            EquipmentQRCodeDto result = await _equipmentQRCodeService.Update(model.TransferToDto());
            return Ok(result.TransferToViewModel());
        }

        /// <summary>
        /// Delete Equipment QRCode
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="serialNumberId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{equipmentId}/qr_codes/{id}/delete", Name = "DeleteQRCode")]
        [HttpPost("{equipmentId}/serial_numbers/{serialNumberId}/qr_codes/{id}/delete", Name = "DeleteSerialNumberQRCode")]
        public async Task<IActionResult> DeleteQRCode([FromRoute]int equipmentId, [FromRoute]int? serialNumberId, int id)
        {
            bool result = await _equipmentQRCodeService.Delete(id);
            return Ok(result);
        }

        #endregion
    }
}
