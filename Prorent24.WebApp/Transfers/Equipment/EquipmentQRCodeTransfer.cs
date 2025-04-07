using Prorent24.BLL.Models.Equipment;
using Prorent24.WebApp.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Equipment
{
    /// <summary>
    /// 
    /// </summary>
    public static class EquipmentQRCodeTransfer
    {
        /// <summary>
        /// Transfer from List<EquipmentQRCodeViewModel> to List<EquipmentQRCodeDto>
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<EquipmentQRCodeDto> TransferToListDto(this List<EquipmentQRCodeViewModel> models)
        {
            List<EquipmentQRCodeDto> EquipmentQRCodeDtos = models.Select(x => x.TransferToDto()).ToList();

            return EquipmentQRCodeDtos;
        }

        /// <summary>
        /// Transfer from EquipmentQRCodeDto to EquipmentQRCodeViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EquipmentQRCodeDto TransferToDto(this EquipmentQRCodeViewModel model)
        {
            EquipmentQRCodeDto EquipmentQRCodeDto = new EquipmentQRCodeDto()
            {
                Id = model.Id,
                Code = model.Code,
                Image = model.Image
            };

            return EquipmentQRCodeDto;
        }

        /// <summary>
        /// Transfer from EquipmentQRCodeViewModel to EquipmentQRCodeDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentQRCodeViewModel TransferToViewModel(this EquipmentQRCodeDto dto)
        {
            EquipmentQRCodeViewModel viewModel = new EquipmentQRCodeViewModel()
            {
                Id = dto.Id,
                Code = dto.Code,
                Image = dto.Image
            };
            return viewModel;
        }

        /// <summary>
        /// Transfer from List<EquipmentQRCodeDto> to List<EquipmentQRCodeViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<EquipmentQRCodeViewModel> TransferToViewModel(this List<EquipmentQRCodeDto> dtos)
        {
            List<EquipmentQRCodeViewModel> viewModels = dtos.Select(x => x.TransferToViewModel()).ToList();
            return viewModels;
        }
    }
}
