using Prorent24.BLL.Models.Project;
using Prorent24.Entity.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Project
{
    public static class ProjectTimeTransfer
    {
        /// <summary>
        /// Transfer from List<ProjectTimeEntity> to List<ProjectTimeDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectTimeDto> TransferToListDto(this List<ProjectTimeEntity> entities)
        {
            List<ProjectTimeDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from  List<ProjectTimeDto> to List<ProjectTimeEntity>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ProjectTimeEntity> TransferToListEntity(this List<ProjectTimeDto> dtos)
        {
            List<ProjectTimeEntity> entities = dtos.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }


        /// <summary>
        /// Transfer from ProjectTimeEntity to ProjectTimeDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectTimeDto TransferToDto(this ProjectTimeEntity entity, string exclude = null)
        {
            ProjectTimeDto dto = new ProjectTimeDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                ProjectId = entity.ProjectId,
                From = entity.From,
                Until = entity.Until,
                DisplayContract = entity.DisplayContract,
                DisplayQuotation = entity.DisplayQuotation,
                DisplayPackSlip = entity.DisplayPackSlip,
                DefaultPlanPeriod = entity.DefaultPlanPeriod,
                DefaultUsagePeriod = entity.DefaultUsagePeriod,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate
            };

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectTimeDto to ProjectTimeEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectTimeEntity TransferToEntity(this ProjectTimeDto dto)
        {
            ProjectTimeEntity entity = new ProjectTimeEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                ProjectId = dto.ProjectId,
                From = dto.From,
                Until = dto.Until,
                DisplayContract = dto.DisplayContract,
                DisplayQuotation = dto.DisplayQuotation,
                DisplayPackSlip = dto.DisplayPackSlip,
                DefaultPlanPeriod = dto.DefaultPlanPeriod,
                DefaultUsagePeriod = dto.DefaultUsagePeriod,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return entity;
        }
    }
}
