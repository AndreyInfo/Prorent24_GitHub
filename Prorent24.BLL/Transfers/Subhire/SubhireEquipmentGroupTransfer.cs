using Prorent24.BLL.Models.Subhire;
using Prorent24.Entity.Subhire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Subhire
{
    public static class SubhireEquipmentGroupTransfer
    {
        /// <summary>
        /// Transfer from List<SubhireEquipmentGroupEntity> to List<SubhireEquipmentGroupDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<SubhireEquipmentGroupDto> TransferToListDto(this List<SubhireEquipmentGroupEntity> entities)
        {
            List<SubhireEquipmentGroupDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from  List<SubhireEquipmentGroupDto> to List<SubhireEquipmentGroupEntity>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<SubhireEquipmentGroupEntity> TransferToListEntity(this List<SubhireEquipmentGroupDto> dtos)
        {
            List<SubhireEquipmentGroupEntity> entities = dtos.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }


        /// <summary>
        /// Transfer from SubhireEquipmentGroupEntity to SubhireEquipmentGroupDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static SubhireEquipmentGroupDto TransferToDto(this SubhireEquipmentGroupEntity entity, string exclude = null)
        {
            SubhireEquipmentGroupDto dto = new SubhireEquipmentGroupDto()
            {
                Id = entity.Id,
                SubhireId = entity.SubhireId,
                Name = entity.Name,
                Equipments = (exclude == "equipments") ? null : entity.Equipments?.ToList().TransferToListDto(entity.Subhire?.UsagePeriodStart, entity.Subhire?.UsagePeriodEnd),
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate
            };

            return dto;
        }

        /// <summary>
        /// Transfer from SubhireEquipmentGroupDto to SubhireEquipmentGroupEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static SubhireEquipmentGroupEntity TransferToEntity(this SubhireEquipmentGroupDto dto)
        {
            SubhireEquipmentGroupEntity entity = new SubhireEquipmentGroupEntity()
            {
                Id = dto.Id,
                SubhireId = dto.SubhireId,
                Name = dto.Name,
                Equipments = dto.Equipments?.ToList().TransferToListEntity(),
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return entity;
        }
    }
}
