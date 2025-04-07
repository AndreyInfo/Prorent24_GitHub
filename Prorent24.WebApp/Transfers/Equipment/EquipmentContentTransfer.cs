using Prorent24.BLL.Models.Equipment;
using Prorent24.WebApp.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Equipment
{
    public static class EquipmentContentTransfer
    {
        /// <summary>
        /// Transfer from List<EquipmentContentViewModel> to List<EquipmentContentDto>
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<EquipmentContentDto> TransferToListDto(this List<EquipmentContentViewModel> models)
        {
            List<EquipmentContentDto> EquipmentContentDtos = models.Select(x => x.TransferToDto()).ToList();

            return EquipmentContentDtos;
        }

        /// <summary>
        /// Transfer from EquipmentContentDto to EquipmentContentViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EquipmentContentDto TransferToDto(this EquipmentContentViewModel model)
        {
            EquipmentContentDto EquipmentContentDto = new EquipmentContentDto()
            {
                Id = model.Id,
                EquipmentId = model.EquipmentId,
                ContentId = model.ContentId,
                Quantity = model.Quantity,
                UnfoldPackingSlip = model.UnfoldPackingSlip,
                UnfoldFinancialDocuments = model.UnfoldFinancialDocuments
            };

            return EquipmentContentDto;
        }

        /// <summary>
        /// Transfer from EquipmentContentViewModel to EquipmentContentDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentContentViewModel TransferToViewModel(this EquipmentContentDto dto)
        {
            EquipmentContentViewModel EquipmentContentViewModel = new EquipmentContentViewModel()
            {
                Id = dto.Id,
                EquipmentId = dto.EquipmentId,
                ContentId = dto.ContentId,
                ContentName = dto.ContentName,
                Quantity = dto.Quantity,
                UnfoldPackingSlip = dto.UnfoldPackingSlip,
                UnfoldFinancialDocuments = dto.UnfoldFinancialDocuments
            };
            return EquipmentContentViewModel;
        }

        /// <summary>
        /// Transfer from List<EquipmentContentDto> to List<EquipmentContentViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<EquipmentContentViewModel> TransferToViewModel(this List<EquipmentContentDto> dtos)
        {
            List<EquipmentContentViewModel> EquipmentContentViewModels = dtos.Select(x => x.TransferToViewModel()).ToList();
            return EquipmentContentViewModels;
        }
    }
}
