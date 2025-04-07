using Prorent24.BLL.Models.Maintenance;
using Prorent24.BLL.Transfers.Equipment;
using Prorent24.Entity.Maintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Maintenance.InspectionSerialNumber
{
    public static class InspectionSerialNumberSerialNumberTrunsfer
    {
        /// <summary>
        /// Transfer from List<InspectionSerialNumberEntity> to List<InspectionSerialNumberDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<InspectionSerialNumberDto> TransferToListDto(this List<InspectionSerialNumberEntity> entities)
        {
            List<InspectionSerialNumberDto> dtos = entities.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from InspectionSerialNumberDto to InspectionSerialNumberEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static InspectionSerialNumberDto TransferToDto(this InspectionSerialNumberEntity entity)
        {
            InspectionSerialNumberDto dto = new InspectionSerialNumberDto()
            {
                Id = entity.Id,
                InspectionId = entity.InspectionId,
                EquipmentId = entity.EquipmentId,
                Equipment = entity.Equipment?.TransferToDto(),
                SerialNumberId = entity.SerialNumberId,
                SerialNumber = entity.SerialNumber?.TransferToDto()
            };

            return dto;
        }

        /// <summary>
        /// Transfer from InspectionSerialNumberEntity to InspectionSerialNumberDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static InspectionSerialNumberEntity TransferToEntity(this InspectionSerialNumberDto dto)
        {
            InspectionSerialNumberEntity entity = new InspectionSerialNumberEntity()
            {
                Id = dto.Id,
                InspectionId = dto.InspectionId,
                EquipmentId = dto.EquipmentId,
                //Equipment = dto.Equipment?.TransferToEntity(),
                SerialNumberId = dto.SerialNumberId,
                //SerialNumber = dto.SerialNumber.TransferToEntity()
            };
            return entity;
        }

        /// <summary>
        /// Transfer from List<InspectionSerialNumberDto> to List<InspectionSerialNumberEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<InspectionSerialNumberEntity> TransferToViewModel(this List<InspectionSerialNumberDto> dto)
        {
            List<InspectionSerialNumberEntity> entities = dto.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }
    }
}
