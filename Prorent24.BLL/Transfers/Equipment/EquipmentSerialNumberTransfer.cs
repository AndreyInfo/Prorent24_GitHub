using Prorent24.BLL.Models.Equipment;
using Prorent24.Entity.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.Equipment
{
    public static class EquipmentSerialNumberTransfer
    {
        /// <summary>
        /// Transfer from List<EquipmentSerialNumberEntity> to List<EquipmentSerialNumberDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<EquipmentSerialNumberDto> TransferToListDto(this List<EquipmentSerialNumberEntity> entities)
        {
            List<EquipmentSerialNumberDto> EquipmentSerialNumberDtos = entities.Select(x => x.TransferToDto()).ToList();

            return EquipmentSerialNumberDtos;
        }

        /// <summary>
        /// Transfer from EquipmentSerialNumberDto to EquipmentSerialNumberEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static EquipmentSerialNumberDto TransferToDto(this EquipmentSerialNumberEntity entity)
        {
            EquipmentSerialNumberDto EquipmentSerialNumberDto = new EquipmentSerialNumberDto()
            {
                Id = entity.Id,
                EquipmentId = entity.EquipmentId,
                InternalReference = entity.InternalReference,
                SerialNumber = entity.SerialNumber,
                BookValue = entity.BookValue,
                Active = entity.Active,
                CalculateBookValueAutomatically = entity.CalculateBookValueAutomatically,

                //PurchaseDate = entity.PurchaseDate,
                //New
                PurchaseDate = entity.PurchaseDate != null ? ((DateTime)entity.PurchaseDate).ToString("dd.MM.yyyy") : null,

                PurchasePrice = entity.PurchasePrice,
                DepreciationPerMonth = entity.DepreciationPerMonth,
                Remark = entity.Remark,
                SuppliersInfoJson = entity.SuppliersInfo

            };

            return EquipmentSerialNumberDto;
        }

        /// <summary>
        /// Transfer from EquipmentSerialNumberEntity to EquipmentSerialNumberDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentSerialNumberEntity TransferToEntity(this EquipmentSerialNumberDto dto)
        {
            EquipmentSerialNumberEntity EquipmentSerialNumberEntity = new EquipmentSerialNumberEntity()
            {
                Id = dto.Id,
                EquipmentId = dto.EquipmentId,
                InternalReference = dto.InternalReference,
                SerialNumber = dto.SerialNumber,
                BookValue = dto.BookValue,
                Active = dto.Active,
                CalculateBookValueAutomatically = dto.CalculateBookValueAutomatically,

                //PurchaseDate = dto.PurchaseDate,
                //New
                PurchaseDate = DateTime.TryParse(dto.PurchaseDate, out DateTime resultPurchaseDate) ? (DateTime?)resultPurchaseDate : null,

                PurchasePrice = dto.PurchasePrice,
                DepreciationPerMonth = dto.DepreciationPerMonth,
                Remark = dto.Remark,
                SuppliersInfo = dto.SuppliersInfoJson

            };
            return EquipmentSerialNumberEntity;
        }

        /// <summary>
        /// Transfer from List<EquipmentSerialNumberDto> to List<EquipmentSerialNumberEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<EquipmentSerialNumberEntity> TransferToViewModel(this List<EquipmentSerialNumberDto> dto)
        {
            List<EquipmentSerialNumberEntity> EquipmentSerialNumberEntitys = dto.Select(x => x.TransferToEntity()).ToList();
            return EquipmentSerialNumberEntitys;
        }
    }
}
