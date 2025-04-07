using Prorent24.BLL.Models.Maintenance;
using Prorent24.BLL.Transfers.Contact;
using Prorent24.BLL.Transfers.CrewMember;
using Prorent24.BLL.Transfers.Equipment;
using Prorent24.BLL.Transfers.General;
using Prorent24.BLL.Transfers.Tasks;
using Prorent24.Entity.Maintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Maintenance.Repair
{
    public static class RepairTransfer
    {
        /// <summary>
        /// Transfer from List<RepairEntity> to List<RepairDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<RepairDto> TransferToListDto(this List<RepairEntity> entities)
        {
            List<RepairDto> dtos = entities.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from RepairDto to RepairEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static RepairDto TransferToDto(this RepairEntity entity, string exclude = "")
        {
            RepairDto dto = new RepairDto()
            {
                Id = entity.Id,
                EquipmentId = entity.EquipmentId,
                SerialNumberId = entity.SerialNumberId,
                AssignToId = entity.AssignToId,
                ReportedById = entity.ReportedById,
                ExternalRepairId = entity.ExternalRepairId,
                Cost = entity.Cost,
                From = entity.From,
                Until = entity.Until,
                Quantity = entity.Quantity,
                Usable = entity.Usable,
                Remark = entity.Remark,
                EquipmentName = entity.Equipment?.Name,
                Tasks = exclude == "Repairs" ? null : entity.Tasks?.ToList().TransferToListDto("Repairs"),
                Notes = entity.Notes?.ToList().TransferToListDto(),
                Tags = entity.Tags?.ToList().TransferToListTagDto(),
                Files = entity.Files?.ToList().TransferToListDto(),
                Equipment = entity.Equipment?.TransferToDto(),
                ExternalRepair = entity.ExternalRepair?.TransferToDto(),
                AssignTo = entity.AssignTo?.TransferForCrewMemberShortDto(),
                ReportedBy = entity.ReportedBy?.TransferForCrewMemberShortDto()
            };

            return dto;
        }

        /// <summary>
        /// Transfer from RepairEntity to RepairDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static RepairEntity TransferToEntity(this RepairDto dto)
        {
            RepairEntity entity = new RepairEntity()
            {
                Id = dto.Id,
                EquipmentId = dto.EquipmentId,
                SerialNumberId = dto.SerialNumberId,
                AssignToId = dto.AssignToId,
                ReportedById = dto.ReportedById,

                ExternalRepairId = dto.ExternalRepairId,
                Cost = dto.Cost,
                From = dto.From,
                Until = dto.Until,
                Quantity = dto.Quantity,
                Usable = dto.Usable,
                Remark = dto.Remark
            };
            return entity;
        }

        /// <summary>
        /// Transfer from List<RepairDto> to List<RepairEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<RepairEntity> TransferToViewModel(this List<RepairDto> dto)
        {
            List<RepairEntity> entities = dto.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }
    }
}
