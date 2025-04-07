using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.Tasks;
using Prorent24.BLL.Transfers.Contact;
using Prorent24.BLL.Transfers.CrewMember;
using Prorent24.BLL.Transfers.Equipment;
using Prorent24.BLL.Transfers.General;
using Prorent24.BLL.Transfers.Vehicle;
using Prorent24.Entity.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Tasks
{
    public static class TaskTransfer
    {
        /// <summary>
        /// Transfer from List<TaskEntity> to List<TaskDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<TaskDto> TransferToListDto(this List<TaskEntity> entities, string exclude = null)
        {
            List<TaskDto> dtos = entities.Select(x => x.TransferToDto(exclude)).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from TaskDto to TaskEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static TaskDto TransferToDto(this TaskEntity entity, string exclude = null)
        {
            TaskDto dto = new TaskDto()
            {
                Id = entity.Id,
                Name = entity.Task,
                FirstName = !string.IsNullOrWhiteSpace(entity.CreationUser?.FirstName) ? entity.CreationUser?.FirstName : entity.CreationUser?.Email,
                LastName = !string.IsNullOrWhiteSpace(entity.CreationUser?.LastName) ? entity.CreationUser?.LastName : null,
                MiddleName = !string.IsNullOrWhiteSpace(entity.CreationUser?.MiddleName) ? entity.CreationUser?.MiddleName : null,
                Description = entity.Description,
                BelongsTo = entity.BelongsTo,

                //New
                //DeadLine = entity.DeadLine,
                DeadLine = entity.DeadLine != null ? ((DateTime)entity.DeadLine).ToString("dd.MM.yyyy hh:mm") : null,

                CompletedIn = entity.CompletedDate,
                CompletedBy = !string.IsNullOrWhiteSpace(entity.CompleatedByMember?.FirstName) ? string.Concat(entity.CompleatedByMember?.FirstName, " ", entity.CompleatedByMember?.LastName) : entity.CompleatedByMember?.Email,
                IsPublic = entity.IsPublic,
                ContactId = entity.ContactId,
                CrewMemberId = entity.CrewMemberId,
                VehicleId = entity.VehicleId,
                EquipmentId = entity.EquipmentId,
                InspectionId = entity.InspectionId,
                InvoiceId = entity.InvoiceEntityId,
                RepairId = entity.RepairId,
                ProjectId = entity.ProjectId,
                SubhireId = entity.SubhireId,
                Tags = entity.Tags?.ToList().TransferToListTagDto(),
                Notes = entity.Notes?.ToList().TransferToListDto(),
                Files = entity.Files?.ToList().TransferToListDto(),
                CrewMember = exclude != "CrewMember" ? entity.CrewMember?.TransferToDto("CrewMember") : null,
                Contact = exclude != "Contact" ? entity.Contact?.TransferToDto("Contact") : null,
                Vehicle = exclude != "Vehicle" ? entity.Vehicle?.TransferToDto("Vehicle") : null,
                Equipment = exclude != "Equipment" ? entity.Equipment?.TransferToDto("Equipment") : null,
                CrewMembers = entity.CrewMembers != null ? entity.CrewMembers.ToList().TransferToDto() : new List<CrewMemberShortDto>(),
                CreationUserId = entity.CreationUserId,
                Executors = entity.Executors != null ? entity.Executors.ToList().TransferToListExecutorDto() : new List<CrewMemberShortDto>(),

            };

            return dto;
        }

        /// <summary>
        /// Transfer from TaskEntity to TaskDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static TaskEntity TransferToEntity(this TaskDto dto)
        {
            TaskEntity timeRegistrationEntity = new TaskEntity()
            {
                Id = dto.Id,
                Task = dto.Name,
                Description = dto.Description,
                BelongsTo = dto.BelongsTo,

                //New
                //DeadLine = dto.DeadLine,
                DeadLine = DateTime.TryParse(dto.DeadLine, out DateTime resultDeadLine) ? (DateTime)resultDeadLine : new DateTime(2000,1,1),
                //CompletedDate = dto.CompletedIn,

                IsPublic = dto.IsPublic,
                ContactId = dto.ContactId,
                CrewMemberId = dto.CrewMemberId,
                VehicleId = dto.VehicleId,
                EquipmentId = dto.EquipmentId,

                InspectionId = dto.InspectionId,
                InvoiceEntityId = dto.InvoiceId,
                RepairId = dto.RepairId,
                ProjectId = dto.ProjectId,
                SubhireId = dto.SubhireId
                //CrewMembers = dto.CrewMembers!=null? dto.CrewMembers.TransferToEntity(): new List<TaskVisibilityCrewMemberEntity>()
            };
            return timeRegistrationEntity;
        }

        public static List<TaskVisibilityCrewMemberEntity> TransferToEntity(this List<CrewMemberShortDto> dtos)
        {
            List<TaskVisibilityCrewMemberEntity> entities = dtos.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }
        public static List<TaskExecutorCrewMemberEntity> TransferToExecutorEntity(this List<CrewMemberShortDto> dtos)
        {
            List<TaskExecutorCrewMemberEntity> entities = dtos.Select(x => x.TransferToExecutorEntity()).ToList();
            return entities;
        }

        public static List<CrewMemberShortDto> TransferToDto(this List<TaskVisibilityCrewMemberEntity> entities)
        {
            List<CrewMemberShortDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        public static List<CrewMemberShortDto> TransferToListExecutorDto(this List<TaskExecutorCrewMemberEntity> entities)
        {
            List<CrewMemberShortDto> dtos = entities.Select(x => x.TransferToExecutorDto()).ToList();
            return dtos;
        }

        public static TaskVisibilityCrewMemberEntity TransferToEntity(this CrewMemberShortDto dto)
        {
            TaskVisibilityCrewMemberEntity entity = new TaskVisibilityCrewMemberEntity()
            {
                CrewMemberId = dto.Id
            };
            return entity;
        }

        public static TaskExecutorCrewMemberEntity TransferToExecutorEntity(this CrewMemberShortDto dto)
        {
            TaskExecutorCrewMemberEntity entity = new TaskExecutorCrewMemberEntity()
            {
                CrewMemberId = dto.Id
            };
            return entity;
        }

        public static CrewMemberShortDto TransferToDto(this TaskVisibilityCrewMemberEntity entity)
        {
            CrewMemberShortDto dto = new CrewMemberShortDto()
            {
                Id = entity.CrewMemberId,
                FirstName = entity.CrewMember?.FirstName,
                LastName = entity.CrewMember?.LastName,
                MiddleName = entity.CrewMember?.MiddleName
            };
            return dto;
        }

        public static CrewMemberShortDto TransferToExecutorDto(this TaskExecutorCrewMemberEntity entity)
        {
            CrewMemberShortDto dto = new CrewMemberShortDto()
            {
                Id = entity.CrewMemberId,
                FirstName = entity.CrewMember?.FirstName,
                LastName = entity.CrewMember?.LastName,
                MiddleName = entity.CrewMember?.MiddleName
            };
            return dto;
        }
    }
}
