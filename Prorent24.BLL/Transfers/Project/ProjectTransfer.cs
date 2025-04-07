using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Transfers.Configuration.Settings;
using Prorent24.BLL.Transfers.Contact;
using Prorent24.BLL.Transfers.ContactPerson;
using Prorent24.BLL.Transfers.CrewMember;
using Prorent24.BLL.Transfers.General;
using Prorent24.BLL.Transfers.Tasks;
using Prorent24.Entity.Project;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Project
{
    public static class ProjectTransfer
    {
        /// <summary>
        /// Transfer from List<ProjectEntity> to List<ProjectDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectDto> TransferToListDto(this List<ProjectEntity> entities)
        {
            List<ProjectDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from  List<ProjectDto> to List<ProjectEntity>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ProjectEntity> TransferToListEntity(this List<ProjectDto> dtos)
        {
            List<ProjectEntity> entities = dtos.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }


        /// <summary>
        /// Transfer from ProjectEntity to ProjectDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectDto TransferToDto(this ProjectEntity entity, string exclude = null)
        {
            if (entity == null)
            {
                return null;
            }
            else
            {
                ProjectDto dto = new ProjectDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Number = entity.Number,
                    Status = entity.Status,
                    ClientContactId = entity.ClientContactId,
                    ClientContact = entity.ClientContact?.TransferToDto(),
                    ClientContactPersonId = entity.ClientContactPersonId,
                    ClientContactPerson = entity.ClientContactPerson?.TransferToDto(),
                    LocationContactId = entity.LocationContactId,
                    LocationContact = entity.LocationContact?.TransferToDto(),
                    LocationContactPersonId = entity.LocationContactPersonId,
                    LocationContactPerson = entity.LocationContactPerson?.TransferToDto(),
                    AccountManagerId = entity.AccountManagerId,
                    AccountManager = entity.CrewMember?.TransferToDto(),
                    Reference = entity.Reference,
                    Color = entity.Color,
                    TypeId = entity.TypeId,
                    Type = entity.Type?.TransferToDto(),
                    Times = entity.Times?.ToList().TransferToListDto(),
                    CreationDate = entity.CreationDate,
                    LastModifiedDate = entity.LastModifiedDate,

                    Tasks = exclude == "Project" ? null : entity.Tasks?.ToList().TransferToListDto("Project"),
                    Notes = entity.Notes?.ToList().TransferToListDto(),
                    Tags = entity.Tags?.ToList().TransferToListTagDto(),
                    Files = entity.Files?.ToList().TransferToListDto(),
                    IsPublic = entity.IsPublic ?? true,
                    CrewMembers = entity.CrewMembers != null ? entity.CrewMembers.ToList().TransferToDto() : new List<CrewMemberShortDto>(),
                    Description = entity.Description
                    // Invoices
                };

                return dto;
            }
        }

        /// <summary>
        /// Transfer from ProjectDto to ProjectEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectEntity TransferToEntity(this ProjectDto dto)
        {
            ProjectEntity entity = new ProjectEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                Number = dto.Number,
                Status = dto.Status,
                ClientContactId = dto.ClientContactId,
                ClientContactPersonId = dto.ClientContactPersonId,
                LocationContactId = dto.LocationContactId,
                LocationContactPersonId = dto.LocationContactPersonId,
                AccountManagerId = dto.AccountManagerId,
                Reference = dto.Reference,
                Color = dto.Color,
                TypeId = dto.TypeId,
                //Type = dto.Type?.TransferToEntity(),
                Times = dto.Times?.ToList().TransferToListEntity(),
                Notes = dto.Notes?.TransferToListEntity(),
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate,
                IsPublic = dto.IsPublic,
                Description = dto.Description
            };

            return entity;
        }

        /// <summary>
        /// Transfer from ProjectChangeStatusEnum to ProjectStatusEnum
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectStatusEnum TransferToStatus(this ProjectChangeStatusEnum changeStatus)
        {
            switch (changeStatus)
            {
                case ProjectChangeStatusEnum.Default:
                    {
                        return ProjectStatusEnum.Confirmed;
                    }
                case ProjectChangeStatusEnum.Pack:
                    {
                        return ProjectStatusEnum.Confirmed;
                    }
                case ProjectChangeStatusEnum.Prepped:
                    {
                        return ProjectStatusEnum.Packed;
                    }
                case ProjectChangeStatusEnum.OnLocation:
                    {
                        return ProjectStatusEnum.OnLocation;
                    }
                case ProjectChangeStatusEnum.InUse:
                    {
                        return ProjectStatusEnum.InUse;
                    }
            }

            return ProjectStatusEnum.Confirmed;
        }

        public static List<CrewMemberShortDto> TransferToDto(this List<ProjectVisibilityCrewMemberEntity> entities)
        {
            List<CrewMemberShortDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        public static CrewMemberShortDto TransferToDto(this ProjectVisibilityCrewMemberEntity entity)
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

        public static List<ProjectVisibilityCrewMemberEntity> TransferToProjectVisibilityEntity(this List<CrewMemberShortDto> dtos)
        {
            List<ProjectVisibilityCrewMemberEntity> entities = dtos.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }

        public static ProjectVisibilityCrewMemberEntity TransferToEntity(this CrewMemberShortDto dto)
        {
            ProjectVisibilityCrewMemberEntity entity = new ProjectVisibilityCrewMemberEntity()
            {
                CrewMemberId = dto.Id
            };
            return entity;
        }
    }
}
