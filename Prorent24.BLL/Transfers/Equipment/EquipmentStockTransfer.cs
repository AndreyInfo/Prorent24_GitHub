using Prorent24.BLL.Models.Equipment;
using Prorent24.Entity.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Equipment
{
    public static class EquipmentStockTransfer
    {
        /// <summary>
        /// Transfer from List<EquipmentStockEntity> to List<EquipmentStockDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<EquipmentStockDto> TransferToListDto(this List<EquipmentStockEntity> entities)
        {
            List<EquipmentStockDto> dtos = entities.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from EquipmentStockDto to EquipmentStockEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static EquipmentStockDto TransferToDto(this EquipmentStockEntity entity)
        {
            EquipmentStockDto dto = new EquipmentStockDto()
            {
                Id = entity.Id,
                EquipmentId = entity.EquipmentId,
                DeliveryDate = entity.DeliveryDate,
                Details = entity.Details,
                Quantity = entity.Quantity,
                Description = entity.Description,
                Date = entity.DeliveryDate != null ? ((DateTime)entity.DeliveryDate).ToString("dd.MM.yyyy") : String.Empty
            };

            return dto;
        }



        /// <summary>
        /// Transfer from EquipmentStockEntity to EquipmentStockDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentStockEntity TransferToEntity(this EquipmentStockDto dto)
        {
            EquipmentStockEntity entity = new EquipmentStockEntity()
            {
                Id = dto.Id,
                EquipmentId = dto.EquipmentId,
                DeliveryDate = dto.DeliveryDate,
                Description = dto.Description,
                Details = dto.Details,
                Quantity = dto.Quantity
            };
            return entity;
        }

        /// <summary>
        /// Transfer from EquipmentStockEntity to EquipmentStockDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentStockEntity TransferToEntityFromCorrect(this EquipmentStockCorrectDto dto)
        {
            EquipmentStockEntity entity = new EquipmentStockEntity()
            {
                EquipmentId = dto.EquipmentId,
                DeliveryDate = DateTime.UtcNow,
                Description = dto.Description,
                Details = dto.Details,
                // Quantity = dto.Quantity
            };
            return entity;
        }

        /// <summary>
        /// Transfer from List<EquipmentStockDto> to List<EquipmentStockEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<EquipmentStockEntity> TransferToListEntity(this List<EquipmentStockDto> dto)
        {
            List<EquipmentStockEntity> entities = dto.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityUpdate"></param>
        /// <returns></returns>
        public static EquipmentStockEntity TransferToEntityForUpdate(this EquipmentStockEntity entity, EquipmentStockEntity entityUpdate)
        {
            entity.DeliveryDate = entityUpdate.DeliveryDate;
            entity.Quantity = entityUpdate.Quantity;
            entity.Description = entityUpdate.Description;
            entity.Details = entityUpdate.Details;

            return entity;
        }
    }
}
