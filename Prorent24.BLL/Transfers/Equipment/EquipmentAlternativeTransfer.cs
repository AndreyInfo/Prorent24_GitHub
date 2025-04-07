using Prorent24.BLL.Models.Equipment;
using Prorent24.Entity.Equipment;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.Equipment
{
    public static class EquipmentAlternativeTransfer
    {
        /// <summary>
        /// Transfer from List<EquipmentAlternativeEntity> to List<EquipmentAlternativeDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<EquipmentAlternativeDto> TransferToListDto(this List<EquipmentAlternativeEntity> entities)
        {
            List<EquipmentAlternativeDto> dtos = entities.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from EquipmentAlternativeDto to EquipmentAlternativeEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static EquipmentAlternativeDto TransferToDto(this EquipmentAlternativeEntity entity)
        {
            EquipmentAlternativeDto dto = new EquipmentAlternativeDto()
            {
                Id = entity.Id,
                EquipmentId = entity.EquipmentId,
                AlternativeId = entity.AlternativeId,
                Alternative = entity.Alternative?.TransferToDto()
            };

            return dto;
        }

        /// <summary>
        /// Transfer from EquipmentAlternativeEntity to EquipmentAlternativeDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentAlternativeEntity TransferToEntity(this EquipmentAlternativeDto dto)
        {
            EquipmentAlternativeEntity entity = new EquipmentAlternativeEntity()
            {
                Id = dto.Id,
                EquipmentId = dto.EquipmentId,
                AlternativeId = dto.AlternativeId
            };
            return entity;
        }

        /// <summary>
        /// Transfer from List<EquipmentAlternativeDto> to List<EquipmentAlternativeEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<EquipmentAlternativeEntity> TransferToViewModel(this List<EquipmentAlternativeDto> dto)
        {
            List<EquipmentAlternativeEntity> entities = dto.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }
    }
}
