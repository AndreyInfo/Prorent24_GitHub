using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.WebApp.Models.Configuration.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Configurations.Settings
{
    public static class PeriodicInspectionTransfer
    {
        /// <summary>
        /// Transfer from List<PeriodicInspectionDto> to List<PeriodicInspectionViewModel> 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<PeriodicInspectionViewModel> TransferToListDto(this List<PeriodicInspectionDto> entities)
        {
            List<PeriodicInspectionViewModel> models = entities.Select(x => x.TransferToViewModel()).ToList();
            return models;
        }

        /// <summary>
        /// Transfer from PeriodicInspectionViewModel to PeriodicInspectionDto
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static PeriodicInspectionDto TransferToDto(this PeriodicInspectionViewModel model)
        {
            PeriodicInspectionDto dto = new PeriodicInspectionDto()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                FrequencyInterval = model.FrequencyInterval,
                FrequencyUnitType = model.FrequencyUnitType,
            };

            return dto;
        }

        /// <summary>
        /// Transfer from PeriodicInspectionDto to PeriodicInspectionDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static PeriodicInspectionViewModel TransferToViewModel(this PeriodicInspectionDto dto)
        {
            PeriodicInspectionViewModel entity = new PeriodicInspectionViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                FrequencyInterval = dto.FrequencyInterval,
                FrequencyUnitType = dto.FrequencyUnitType,
            };

            return entity;
        }
    }
}
