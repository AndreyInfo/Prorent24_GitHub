using Prorent24.BLL.Models.Equipment;
using Prorent24.Entity.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Equipment
{
    public static class EquipmentPeriodicInspectionTransfer
    {
        /// <summary>
        /// Transfer from List<EquipmentPeriodicInspectionEntity> to List<EquipmentPeriodicInspectionDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<EquipmentPeriodicInspectionDto> TransferToListDto(this List<EquipmentPeriodicInspectionEntity> entities)
        {
            List<EquipmentPeriodicInspectionDto> dtos = entities.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from EquipmentPeriodicInspectionDto to EquipmentPeriodicInspectionEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static EquipmentPeriodicInspectionDto TransferToDto(this EquipmentPeriodicInspectionEntity entity)
        {
            EquipmentPeriodicInspectionDto dto = new EquipmentPeriodicInspectionDto()
            {
                Id = entity.Id,
                EquipmentId = entity.EquipmentId,
                PeriodicInspectionId = entity.PeriodicInspectionId,
                Active = entity.Active
            };

            return dto;
        }

        /// <summary>
        /// Transfer from EquipmentPeriodicInspectionEntity to EquipmentPeriodicInspectionDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentPeriodicInspectionEntity TransferToEntity(this EquipmentPeriodicInspectionDto dto)
        {
            var entity = new EquipmentPeriodicInspectionEntity()
            {
                Id = dto.Id,
                EquipmentId = dto.EquipmentId,
                PeriodicInspectionId = dto.PeriodicInspectionId,
                Active = dto.Active
            };
            return entity;
        }

        /// <summary>
        /// Transfer from List<EquipmentPeriodicInspectionDto> to List<EquipmentPeriodicInspectionEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<EquipmentPeriodicInspectionEntity> TransferToViewModel(this List<EquipmentPeriodicInspectionDto> dto)
        {
            List<EquipmentPeriodicInspectionEntity> entities = dto.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }
    }
}
