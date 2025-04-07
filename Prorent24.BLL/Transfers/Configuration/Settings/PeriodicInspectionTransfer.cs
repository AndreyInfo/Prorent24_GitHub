using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.Entity.Configuration.Settings;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.Configuration.Settings
{
    public static class PeriodicInspectionTransfer
    {
        /// <summary>
        /// Transfer from List<PeriodicInspectionEntity> to List<PeriodicInspectionDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<PeriodicInspectionDto> TransferToListDto(this List<PeriodicInspectionEntity> entities)
        {
            List<PeriodicInspectionDto> dtos = entities.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from PeriodicInspectionEntity to PeriodicInspectionDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static PeriodicInspectionDto TransferToDto(this PeriodicInspectionEntity entity)
        {
            PeriodicInspectionDto dto = new PeriodicInspectionDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                FrequencyInterval = entity.FrequencyInterval,
                FrequencyUnitType = entity.FrequencyUnitType,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate
            };

            return dto;
        }

        /// <summary>
        /// Transfer from PeriodicInspectionDto to PeriodicInspectionEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static PeriodicInspectionEntity TransferToEntity(this PeriodicInspectionDto dto)
        {
            PeriodicInspectionEntity entity = new PeriodicInspectionEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                FrequencyInterval = dto.FrequencyInterval,
                FrequencyUnitType = dto.FrequencyUnitType,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return entity;
        }
    }
}
