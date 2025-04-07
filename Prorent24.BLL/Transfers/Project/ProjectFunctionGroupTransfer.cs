using Prorent24.BLL.Models.Project;
using Prorent24.Entity.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Project
{
    public static class ProjectFunctionGroupTransfer
    {
        /// <summary>
        /// Transfer from List<ProjectFunctionGroupEntity> to List<ProjectFunctionGroupDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectFunctionGroupDto> TransferToListDto(this List<ProjectFunctionGroupEntity> entities)
        {
            List<ProjectFunctionGroupDto> dtos = entities.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from ProjectFunctionGroupDto to ProjectFunctionGroupEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectFunctionGroupDto TransferToDto(this ProjectFunctionGroupEntity entity)
        {
            ProjectFunctionGroupDto dto = new ProjectFunctionGroupDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                UseFrom = entity.UseFrom,
                UseUntil = entity.UseUntil,
                PlanFrom = entity.PlanFrom,
                PlanUntil = entity.PlanUntil,
                ProjectId = entity.SubprojectId

            };

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectFunctionGroupEntity to ProjectFunctionGroupDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectFunctionGroupEntity TransferToEntity(this ProjectFunctionGroupDto dto)
        {
            ProjectFunctionGroupEntity entity = new ProjectFunctionGroupEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                UseFrom = dto.UseFrom,
                UseUntil = dto.UseUntil,
                PlanFrom = dto.PlanFrom,
                PlanUntil = dto.PlanUntil,
                SubprojectId = dto.SubprojectId
                
            };
            return entity;
        }

        /// <summary>
        /// Transfer from List<ProjectFunctionGroupDto> to List<ProjectFunctionGroupEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<ProjectFunctionGroupEntity> TransferToViewModel(this List<ProjectFunctionGroupDto> dto)
        {
            List<ProjectFunctionGroupEntity> entities = dto.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }

        /// <summary>
        /// Transfer from List<ProjectFunctionGroupEntity> to List<ProjectFunctionGridDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectFunctionGridDto> TransferToListGridDto(this List<ProjectFunctionGroupEntity> entities)
        {

            List<ProjectFunctionGridDto> dtos = entities.Select(x => x.TransferToGridDto(null)).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from ProjectFunctionGroupEntity to ProjectFunctionGridDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectFunctionGridDto TransferToGridDto(this ProjectFunctionGroupEntity entity, string exclude = null)
        {
            ProjectFunctionGridDto dto = new ProjectFunctionGridDto()
            {
                Id = entity.Id,
                ProjectId = entity.SubprojectId,
                GroupId = entity.Id,
                GroupName = entity.Name,
                UseFrom = entity.UseFrom,
                UseUntil = entity.UseUntil,
                PlanFrom = entity.PlanFrom,
                PlanUntil = entity.PlanUntil
           
            };
            return dto;
        }

    }
}
