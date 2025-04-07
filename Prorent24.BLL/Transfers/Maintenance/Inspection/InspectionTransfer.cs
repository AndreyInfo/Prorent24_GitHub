using Prorent24.BLL.Models.Maintenance;
using Prorent24.BLL.Transfers.Configuration.Settings;
using Prorent24.BLL.Transfers.General;
using Prorent24.BLL.Transfers.Tasks;
using Prorent24.Entity.Maintenance;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.Maintenance.Inspection
{
    public static class InspectionTransfer
    {
        /// <summary>
        /// Transfer from List<InspectionEntity> to List<InspectionDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<InspectionDto> TransferToListDto(this List<InspectionEntity> entities)
        {
            List<InspectionDto> dtos = entities.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from InspectionDto to InspectionEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static InspectionDto TransferToDto(this InspectionEntity entity, string exclude = "")
        {
            InspectionDto dto = new InspectionDto()
            {
                Id = entity.Id,
                PeriodicInspectionId = entity.PeriodicInspectionId,
                PeriodicInspection = entity.PeriodicInspection?.TransferToDto(),

                Description = entity.Description,
                Date = entity.Date,
                Status = entity.Status,

                Tasks = exclude == "Inspection" ? null : entity.Tasks?.ToList().TransferToListDto("Inspection"),
                Notes = entity.Notes?.ToList().TransferToListDto(),
                Tags = entity.Tags?.ToList().TransferToListTagDto(),
                Files = entity.Files?.ToList().TransferToListDto()
            };

            return dto;
        }

        /// <summary>
        /// Transfer from InspectionEntity to InspectionDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static InspectionEntity TransferToEntity(this InspectionDto dto)
        {
            InspectionEntity entity = new InspectionEntity()
            {
                Id = dto.Id,
                PeriodicInspectionId = dto.PeriodicInspectionId,
                Description = dto.Description,
                Date = dto.Date,
                Status = dto.Status
            };
            return entity;
        }

        /// <summary>
        /// Transfer from List<InspectionDto> to List<InspectionEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<InspectionEntity> TransferToViewModel(this List<InspectionDto> dto)
        {
            List<InspectionEntity> entities = dto.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }
    }
}
