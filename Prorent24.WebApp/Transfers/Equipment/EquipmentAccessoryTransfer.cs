using Prorent24.BLL.Models.Equipment;
using Prorent24.WebApp.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Equipment
{
    public static class EquipmentAccessoryTransfer
    {
        /// <summary>
        /// Transfer from List<EquipmentAccessoryViewModel> to List<EquipmentAccessoryDto>
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<EquipmentAccessoryDto> TransferToListDto(this List<EquipmentAccessoryViewModel> models)
        {
            List<EquipmentAccessoryDto> EquipmentAccessoryDtos = models.Select(x => x.TransferToDto()).ToList();

            return EquipmentAccessoryDtos;
        }

        /// <summary>
        /// Transfer from EquipmentAccessoryDto to EquipmentAccessoryViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EquipmentAccessoryDto TransferToDto(this EquipmentAccessoryViewModel model)
        {
            EquipmentAccessoryDto EquipmentAccessoryDto = new EquipmentAccessoryDto()
            {
                Id = model.Id,
                EquipmentId = model.EquipmentId,
                AccessoryId = model.AccessoryId,
                Automatic = model.Automatic,
                Free = model.Free,
                Quantity = model.Quantity,
                SkipIfAlreadyPresent = model.SkipIfAlreadyPresent
            };

            return EquipmentAccessoryDto;
        }

        /// <summary>
        /// Transfer from EquipmentAccessoryViewModel to EquipmentAccessoryDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentAccessoryViewModel TransferToViewModel(this EquipmentAccessoryDto dto)
        {
            EquipmentAccessoryViewModel EquipmentAccessoryViewModel = new EquipmentAccessoryViewModel()
            {
                Id = dto.Id,
                EquipmentId = dto.EquipmentId,
                AccessoryId = dto.AccessoryId,
                AccessoryName = dto.AccessoryName,
                Automatic = dto.Automatic,
                Free = dto.Free,
                Quantity = dto.Quantity,
                SkipIfAlreadyPresent = dto.SkipIfAlreadyPresent
            };
            return EquipmentAccessoryViewModel;
        }

        /// <summary>
        /// Transfer from List<EquipmentAccessoryDto> to List<EquipmentAccessoryViewModel>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<EquipmentAccessoryViewModel> TransferToViewModel(this List<EquipmentAccessoryDto> dtos)
        {
            List<EquipmentAccessoryViewModel> EquipmentAccessoryViewModels = dtos.Select(x => x.TransferToViewModel()).ToList();
            return EquipmentAccessoryViewModels;
        }
    }
}
