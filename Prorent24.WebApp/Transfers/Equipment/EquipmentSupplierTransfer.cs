using Prorent24.BLL.Models.Equipment;
using Prorent24.WebApp.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Equipment
{
    public static class EquipmentSupplierTransfer
    {
        /// <summary>
        /// Transfer from List<EquipmentSupplierViewModel> to List<EquipmentSupplierDto>
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<EquipmentSupplierDto> TransferToListDto(this List<EquipmentSupplierViewModel> models)
        {
            List<EquipmentSupplierDto> EquipmentSupplierDtos = models.Select(x => x.TransferToDto()).ToList();

            return EquipmentSupplierDtos;
        }

        /// <summary>
        /// Transfer from EquipmentSupplierDto to EquipmentSupplierViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EquipmentSupplierDto TransferToDto(this EquipmentSupplierViewModel model)
        {
            EquipmentSupplierDto EquipmentSupplierDto = new EquipmentSupplierDto()
            {
                Id = model.Id,
                EquipmentId = model.EquipmentId,
                ContactId = model.ContactId,
                Details = model.Details,
                Price = model.Price
            };

            return EquipmentSupplierDto;
        }

        /// <summary>
        /// Transfer from EquipmentSupplierViewModel to EquipmentSupplierDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentSupplierViewModel TransferToViewModel(this EquipmentSupplierDto dto)
        {
            EquipmentSupplierViewModel EquipmentSupplierViewModel = new EquipmentSupplierViewModel()
            {
                Id = dto.Id,
                EquipmentId = dto.EquipmentId,
                ContactId = dto.ContactId,
                ContactName = dto.ContactName,
                Details = dto.Details,
                Price = dto.Price

            };
            return EquipmentSupplierViewModel;
        }

        /// <summary>
        /// Transfer from List<EquipmentSupplierDto> to List<EquipmentSupplierViewModel>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<EquipmentSupplierViewModel> TransferToViewModel(this List<EquipmentSupplierDto> dtos)
        {
            List<EquipmentSupplierViewModel> EquipmentSupplierViewModels = dtos.Select(x => x.TransferToViewModel()).ToList();
            return EquipmentSupplierViewModels;
        }
    }
}
