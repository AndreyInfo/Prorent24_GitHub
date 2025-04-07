using Prorent24.BLL.Models.Equipment;
using Prorent24.WebApp.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Equipment
{
    public static class EquipmentAlternativeTransfer
    {
        /// <summary>
        /// Transfer from List<EquipmentAlternativeViewModel> to List<EquipmentAlternativeDto>
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<EquipmentAlternativeDto> TransferToListDto(this List<EquipmentAlternativeViewModel> models)
        {
            List<EquipmentAlternativeDto> dtos = models.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from EquipmentAlternativeDto to EquipmentAlternativeViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EquipmentAlternativeDto TransferToDto(this EquipmentAlternativeViewModel model)
        {
            EquipmentAlternativeDto dto = new EquipmentAlternativeDto()
            {
                Id = model.Id,
                EquipmentId = model.EquipmentId,
                AlternativeId = model.AlternativeId
            };

            return dto;
        }

        /// <summary>
        /// Transfer from EquipmentAlternativeViewModel to EquipmentAlternativeDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentAlternativeViewModel TransferToViewModel(this EquipmentAlternativeDto dto)
        {
            EquipmentAlternativeViewModel model = new EquipmentAlternativeViewModel()
            {
                Id = dto.Id,
                EquipmentId = dto.EquipmentId,
                AlternativeId = dto.AlternativeId,
                AlternativeName =dto.AlternativeName,
                Code = dto.Code
            };
            return model;
        }

        /// <summary>
        /// Transfer from List<EquipmentAlternativeDto> to List<EquipmentAlternativeViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<EquipmentAlternativeViewModel> TransferToViewModel(this List<EquipmentAlternativeDto> dtos)
        {
            List<EquipmentAlternativeViewModel> models = dtos.Select(x => x.TransferToViewModel()).ToList();
            return models;
        }
    }
}
