using Prorent24.BLL.Models.Equipment;
using Prorent24.WebApp.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Equipment
{
    public static class EquipmentPeriodicInspectionTransfer
    {/// <summary>
     /// Transfer from List<EquipmentPeriodicInspectionViewModel> to List<EquipmentPeriodicInspectionDto>
     /// </summary>
     /// <param name="models"></param>
     /// <returns></returns>
        public static List<EquipmentPeriodicInspectionDto> TransferToListDto(this List<EquipmentPeriodicInspectionViewModel> models)
        {
            List<EquipmentPeriodicInspectionDto> dtos = models.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from EquipmentPeriodicInspectionDto to EquipmentPeriodicInspectionViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EquipmentPeriodicInspectionDto TransferToDto(this EquipmentPeriodicInspectionViewModel model)
        {
            EquipmentPeriodicInspectionDto dto = new EquipmentPeriodicInspectionDto()
            {
                Id = model.Id,
                EquipmentId = model.EquipmentId,
                PeriodicInspectionId = model.PeriodicInspectionId,
                Active = model.Active
            };

            return dto;
        }

        /// <summary>
        /// Transfer from EquipmentPeriodicInspectionViewModel to EquipmentPeriodicInspectionDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentPeriodicInspectionViewModel TransferToViewModel(this EquipmentPeriodicInspectionDto dto)
        {
            EquipmentPeriodicInspectionViewModel model = new EquipmentPeriodicInspectionViewModel()
            {
                Id = dto.Id,
                EquipmentId = dto.EquipmentId,
                Description = dto.Description,
                Frequency = dto.Frequency,
                Name = dto.Name,
                Period = dto.Period,
                PeriodicInspectionId = dto.PeriodicInspectionId,
                Active = dto.Active
            };
            return model;
        }

        /// <summary>
        /// Transfer from List<EquipmentPeriodicInspectionDto> to List<EquipmentPeriodicInspectionViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<EquipmentPeriodicInspectionViewModel> TransferToViewModel(this List<EquipmentPeriodicInspectionDto> dtos)
        {
            List<EquipmentPeriodicInspectionViewModel> models = dtos.Select(x => x.TransferToViewModel()).ToList();
            return models;
        }
    }
}
