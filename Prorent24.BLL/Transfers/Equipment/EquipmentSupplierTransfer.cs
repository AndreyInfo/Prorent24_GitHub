using Prorent24.BLL.Models.Equipment;
using Prorent24.Entity.Equipment;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.Equipment
{
    public static class EquipmentSupplierTransfer
    {
        /// <summary>
        /// Transfer from List<EquipmentSupplierEntity> to List<EquipmentSupplierDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<EquipmentSupplierDto> TransferToListDto(this List<EquipmentSupplierEntity> entities)
        {
            List<EquipmentSupplierDto> EquipmentSupplierDtos = entities.Select(x => x.TransferToDto()).ToList();

            return EquipmentSupplierDtos;
        }

        /// <summary>
        /// Transfer from EquipmentSupplierDto to EquipmentSupplierEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static EquipmentSupplierDto TransferToDto(this EquipmentSupplierEntity entity)
        {
            EquipmentSupplierDto EquipmentSupplierDto = new EquipmentSupplierDto()
            {
                Id = entity.Id,
                EquipmentId = entity.EquipmentId,
                ContactId = entity.ContactId,
                Details = entity.Details,
                Price = entity.Price,
                ContactName = entity.Contact?.CompanyName
            };

            return EquipmentSupplierDto;
        }

        /// <summary>
        /// Transfer from EquipmentSupplierEntity to EquipmentSupplierDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentSupplierEntity TransferToEntity(this EquipmentSupplierDto dto)
        {
            EquipmentSupplierEntity EquipmentSupplierEntity = new EquipmentSupplierEntity()
            {
                Id = dto.Id,
                EquipmentId = dto.EquipmentId,
                ContactId = dto.ContactId,
                Details = dto.Details,
                Price = dto.Price
            };
            return EquipmentSupplierEntity;
        }

        /// <summary>
        /// Transfer from List<EquipmentSupplierDto> to List<EquipmentSupplierEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<EquipmentSupplierEntity> TransferToViewModel(this List<EquipmentSupplierDto> dto)
        {
            List<EquipmentSupplierEntity> EquipmentSupplierEntitys = dto.Select(x => x.TransferToEntity()).ToList();
            return EquipmentSupplierEntitys;
        }
    }
}
