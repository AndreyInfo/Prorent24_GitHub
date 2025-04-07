using Prorent24.BLL.Models.Equipment;
using Prorent24.Entity.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Equipment
{
    public static class EquipmentQRCodeTransfer
    {
        /// <summary>
        /// Transfer from List<EquipmentQRCodeEntity> to List<EquipmentQRCodeDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<EquipmentQRCodeDto> TransferToListDto(this List<EquipmentQRCodeEntity> entities)
        {
            List<EquipmentQRCodeDto> EquipmentQRCodeDtos = entities.Select(x => x.TransferToDto()).ToList();

            return EquipmentQRCodeDtos;
        }

        /// <summary>
        /// Transfer from EquipmentQRCodeDto to EquipmentQRCodeEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static EquipmentQRCodeDto TransferToDto(this EquipmentQRCodeEntity entity)
        {
            EquipmentQRCodeDto EquipmentQRCodeDto = new EquipmentQRCodeDto()
            {
                Id = entity.Id,
                Code = entity.Code,
                EquipmentId = entity.EquipmentId,
                EquipmentSerialNumberId = entity.SerialNumberId,
                Image = entity.BarCode
            };

            return EquipmentQRCodeDto;
        }

        /// <summary>
        /// Transfer from EquipmentQRCodeEntity to EquipmentQRCodeDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentQRCodeEntity TransferToEntity(this EquipmentQRCodeDto dto)
        {
            EquipmentQRCodeEntity EquipmentQRCodeEntity = new EquipmentQRCodeEntity()
            {
                Id = dto.Id,
                Code = dto.Code,
                EquipmentId = dto.EquipmentId,
                SerialNumberId = dto.EquipmentSerialNumberId,
                BarCode = dto.Image
            };
            return EquipmentQRCodeEntity;
        }

        /// <summary>
        /// Transfer from List<EquipmentQRCodeDto> to List<EquipmentQRCodeEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<EquipmentQRCodeEntity> TransferToViewModel(this List<EquipmentQRCodeDto> dto)
        {
            List<EquipmentQRCodeEntity> EquipmentQRCodeEntitys = dto.Select(x => x.TransferToEntity()).ToList();
            return EquipmentQRCodeEntitys;
        }
    }
}
