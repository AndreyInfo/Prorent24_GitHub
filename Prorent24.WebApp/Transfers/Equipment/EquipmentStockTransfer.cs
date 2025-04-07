using Prorent24.BLL.Models.Equipment;
using Prorent24.WebApp.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Equipment
{
    public static class EquipmentStockTransfer
    {
        /// <summary>
        /// Transfer from List<EquipmentStockViewModel> to List<EquipmentStockDto>
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<EquipmentStockDto> TransferToListDto(this List<EquipmentStockViewModel> models)
        {
            List<EquipmentStockDto> dtos = models.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from EquipmentStockDto to EquipmentStockViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EquipmentStockDto TransferToDto(this EquipmentStockViewModel model)
        {
            EquipmentStockDto dto = new EquipmentStockDto()
            {
                Id = model.Id,
                EquipmentId = model.EquipmentId,
                DeliveryDate = model.DeliveryDate,
                Description = model.Description,
                Details = model.Details,
                Quantity = model.Quantity
            };

            return dto;
        }

        /// <summary>
        /// Transfer from EquipmentStockViewModel to EquipmentStockDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentStockViewModel TransferToViewModel(this EquipmentStockDto dto)
        {
            EquipmentStockViewModel model = new EquipmentStockViewModel()
            {
                Id = dto.Id,
                EquipmentId = dto.EquipmentId,
                DeliveryDate = dto.DeliveryDate,
                Description = dto.Description,
                Details = dto.Details,
                Quantity = dto.Quantity,
                Date = dto.Date
            };
            return model;
        }

        /// <summary>
        /// Transfer from List<EquipmentStockDto> to List<EquipmentStockViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<EquipmentStockViewModel> TransferToViewModel(this List<EquipmentStockDto> dtos)
        {
            List<EquipmentStockViewModel> models = dtos.Select(x => x.TransferToViewModel()).ToList();
            return models;
        }

        /// <summary>
        /// Transfer EquipmentStockCorrectViewModel to dto
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EquipmentStockCorrectDto TransferToDto(this EquipmentStockCorrectViewModel model) {
            EquipmentStockCorrectDto dto = new EquipmentStockCorrectDto()
            {
                Description = model.Description,
                Details = model.Details,
                Quantity = model.Quantity
            };

            return dto;
        }
    }
}
