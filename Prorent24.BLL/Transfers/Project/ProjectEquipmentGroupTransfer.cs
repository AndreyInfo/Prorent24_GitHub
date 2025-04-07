using Prorent24.BLL.Models.Project;
using Prorent24.Entity.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Project
{
    public static class ProjectEquipmentGroupTransfer
    {
        /// <summary>
        /// Transfer from List<ProjectEquipmentGroupEntity> to List<ProjectEquipmentGroupDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectEquipmentGroupDto> TransferToListDto(this List<ProjectEquipmentGroupEntity> entities)
        {
            List<ProjectEquipmentGroupDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from  List<ProjectEquipmentGroupDto> to List<ProjectEquipmentGroupEntity>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ProjectEquipmentGroupEntity> TransferToListEntity(this List<ProjectEquipmentGroupDto> dtos)
        {
            List<ProjectEquipmentGroupEntity> entities = dtos.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }


        /// <summary>
        /// Transfer from ProjectEquipmentGroupEntity to ProjectEquipmentGroupDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectEquipmentGroupDto TransferToDto(this ProjectEquipmentGroupEntity entity, string exclude = null)
        {
            ProjectEquipmentGroupDto dto = new ProjectEquipmentGroupDto()
            {
                Id = entity.Id,
                ProjectId = entity.ProjectId,
                Name = entity.Name, //+ ((entity.StartUsePeriod != null && entity.EndUsePeriod != null) ? "(" + entity.StartUsePeriod + " - " + entity.EndUsePeriod + ")" : ""),
                Equipments = (exclude == "Equipments") ? null : entity.Equipments?.ToList().TransferToListDto(),
                StartPlanPeriod = entity.StartPlanPeriod,
                EndPlanPeriod = entity.EndPlanPeriod,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate,
                StartUsePeriod = entity.StartUsePeriod,
                EndUsePeriod = entity.EndUsePeriod
            };

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectEquipmentGroupDto to ProjectEquipmentGroupEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectEquipmentGroupEntity TransferToEntity(this ProjectEquipmentGroupDto dto)
        {
            ProjectEquipmentGroupEntity entity = new ProjectEquipmentGroupEntity()
            {
                Id = dto.Id,
                ProjectId = dto.ProjectId,
                Name = dto.Name,
                Equipments = dto.Equipments?.ToList().TransferToListEntity(),
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return entity;
        }

        /// <summary>
        /// Transfer from List<ProjectEquipmentGroupDto> to List<ProjectEquipmentGridDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectEquipmentGridDto> TransferToListGridDto(this List<ProjectEquipmentGroupDto> entities)
        {

            List<ProjectEquipmentGridDto> dtos = entities.Select(x => x.TransferToGridDto(null)).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from ProjectEquipmentGroupDto to ProjectFunctionGridDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectEquipmentGridDto TransferToGridDto(this ProjectEquipmentGroupDto dto, string exclude = null)
        {
            ProjectEquipmentGridDto grid = new ProjectEquipmentGridDto()
            {
                Id = dto.Id,
                GroupId = dto.Id,
                GroupName = dto.Name,
                StartUsePeriod = dto.StartUsePeriod,
                EndUsePeriod =  dto.EndUsePeriod,
                //Period = (dto.StartUsePeriod != null && dto.EndUsePeriod != null) ?  dto.StartUsePeriod.ToString() + " - " + dto.EndUsePeriod.ToString()  : "",
                StartPlanPeriod = dto.StartPlanPeriod,
                EndPlanPeriod = dto.EndPlanPeriod
                
            };

            grid.Group = new ProjectEquipmentGroupDto()
            {
                Name = dto.Name
            };

            return grid;
        }

    }
}
