using Prorent24.BLL.Models.Equipment;
using Prorent24.Entity.Equipment;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.Equipment
{
    public static class EquipmentAccessoryTransfer
    {
        /// <summary>
        /// Transfer from List<EquipmentAccessoryEntity> to List<EquipmentAccessoryDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<EquipmentAccessoryDto> TransferToListDto(this List<EquipmentAccessoryEntity> entities)
        {
            List<EquipmentAccessoryDto> EquipmentAccessoryDtos = entities.Select(x => x.TransferToDto()).ToList();

            return EquipmentAccessoryDtos;
        }

        /// <summary>
        /// Transfer from EquipmentAccessoryDto to EquipmentAccessoryEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static EquipmentAccessoryDto TransferToDto(this EquipmentAccessoryEntity entity)
        {
            EquipmentAccessoryDto EquipmentAccessoryDto = new EquipmentAccessoryDto()
            {
                Id = entity.Id,
                EquipmentId = entity.EquipmentId,
                AccessoryId = entity.AccessoryId,
                AccessoryName = entity.Accessory?.Name,
                Automatic = entity.Automatic,
                Free = entity.Free,
                Quantity = entity.Quantity,
                SkipIfAlreadyPresent = entity.SkipIfAlreadyPresent
            };

            return EquipmentAccessoryDto;
        }

        /// <summary>
        /// Transfer from EquipmentAccessoryEntity to EquipmentAccessoryDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentAccessoryEntity TransferToEntity(this EquipmentAccessoryDto dto)
        {
            EquipmentAccessoryEntity EquipmentAccessoryEntity = new EquipmentAccessoryEntity()
            {
                Id = dto.Id,
                EquipmentId = dto.EquipmentId,
                AccessoryId = dto.AccessoryId,
                Automatic = dto.Automatic,
                Free = dto.Free,
                Quantity = dto.Quantity,
                SkipIfAlreadyPresent = dto.SkipIfAlreadyPresent
            };
            return EquipmentAccessoryEntity;
        }

        /// <summary>
        /// Transfer from List<EquipmentAccessoryDto> to List<EquipmentAccessoryEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<EquipmentAccessoryEntity> TransferToViewModel(this List<EquipmentAccessoryDto> dto)
        {
            List<EquipmentAccessoryEntity> EquipmentAccessoryEntitys = dto.Select(x => x.TransferToEntity()).ToList();
            return EquipmentAccessoryEntitys;
        }
    }
}
