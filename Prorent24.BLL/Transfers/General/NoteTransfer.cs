using Prorent24.BLL.Models.General.Note;
using Prorent24.Entity.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.General
{
    public static class NoteTransfer
    {
        /// <summary>
        /// Transfer from NoteDto to NoteEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static NoteDto TransferToDto(this NoteEntity entity)
        {
            NoteDto dto = new NoteDto()
            {
                Id = entity.Id,
                Text = entity.Text,
                BelongsTo = entity.BelongsTo,
                Confidential = entity.Confidential,
                TaskId = entity.TaskId,
                ContactId = entity.ContactId,
                CrewMemberId = entity.CrewMemberId,
                VehicleId = entity.VehicleId,
                EquipmentId = entity.EquipmentId,
                InspectionId = entity.InspectionId,
                InvoiceId = entity.InvoiceEntityId,
                RepairId = entity.RepairId,
                ProjectId = entity.ProjectId,
                SubhireId = entity.SubhireId
            };

            return dto;
        }

        /// <summary>
        /// Transfer from List<NoteEntity to List<NoteDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<NoteDto> TransferToListDto(this List<NoteEntity> entities)
        {
            List<NoteDto> dtos = entities.Select(x=>new NoteDto()
            {
                Id = x.Id,
                Text = x.Text,
                BelongsTo = x.BelongsTo,
                Confidential = x.Confidential,
                TaskId = x.TaskId,
                ContactId = x.ContactId,
                CrewMemberId = x.CrewMemberId,
                VehicleId = x.VehicleId,
                EquipmentId = x.EquipmentId,
                InspectionId = x.InspectionId,
                InvoiceId = x.InvoiceEntityId,
                RepairId = x.RepairId,
                ProjectId = x.ProjectId,
                SubhireId = x.SubhireId
            }).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from NoteEntity to NoteDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static NoteEntity TransferToEntity(this NoteDto dto)
        {
            NoteEntity entity = new NoteEntity()
            {
                Id = dto.Id,
                Text = dto.Text,
                BelongsTo = dto.BelongsTo,
                Confidential = dto.Confidential,
                TaskId = dto.TaskId,
                ContactId = dto.ContactId,
                CrewMemberId = dto.CrewMemberId,
                VehicleId = dto.VehicleId,
                EquipmentId = dto.EquipmentId,
                InspectionId = dto.InspectionId,
                InvoiceEntityId = dto.InvoiceId,
                RepairId = dto.RepairId,
                ProjectId = dto.ProjectId,
                SubhireId = dto.SubhireId
            };
            return entity;
        }

        /// <summary>
        /// Transfer from List<NoteEntity> to List<NoteDto>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<NoteEntity> TransferToListEntity(this List<NoteDto> dtos)
        {
            List<NoteEntity> entities = dtos.Select(x=>new NoteEntity()
            {
                Id = x.Id,
                Text = x.Text,
                BelongsTo = x.BelongsTo,
                Confidential = x.Confidential,
                TaskId = x.TaskId,
                ContactId = x.ContactId,
                CrewMemberId = x.CrewMemberId,
                VehicleId = x.VehicleId,
                EquipmentId = x.EquipmentId,
                InspectionId = x.InspectionId,
                InvoiceEntityId = x.InvoiceId,
                RepairId = x.RepairId,
                ProjectId = x.ProjectId,
                SubhireId = x.SubhireId
            }).ToList();

            return entities;
        }
    }
}
