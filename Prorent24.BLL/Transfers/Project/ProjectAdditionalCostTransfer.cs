using Prorent24.BLL.Models.Project;
using Prorent24.Entity.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Project
{
    public static class ProjectAdditionalCostTransfer
    {
        /// <summary>
        /// Transfer from List<ProjectAdditionalCostEntity> to List<ProjectAdditionalCostDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectAdditionalCostDto> TransferToListDto(this List<ProjectAdditionalCostEntity> entities)
        {
            List<ProjectAdditionalCostDto> dtos = entities.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from ProjectAdditionalCostDto to ProjectAdditionalCostEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectAdditionalCostDto TransferToDto(this ProjectAdditionalCostEntity entity)
        {
            ProjectAdditionalCostDto dto = new ProjectAdditionalCostDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                ProjectId = entity.ProjectId,
                Details = entity.Details,
                PurchasePrice = entity.PurchasePrice,
                SalePrice = entity.SalePrice,
                VatClassId = entity.VatClassId
            };

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectAdditionalCostEntity to ProjectAdditionalCostDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectAdditionalCostEntity TransferToEntity(this ProjectAdditionalCostDto dto)
        {
            ProjectAdditionalCostEntity entity = new ProjectAdditionalCostEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                ProjectId = dto.ProjectId,
                Details = dto.Details,
                PurchasePrice = dto.PurchasePrice,
                SalePrice = dto.SalePrice,
                VatClassId = dto.VatClassId
            };
            return entity;
        }

        /// <summary>
        /// Transfer from List<ProjectAdditionalCostDto> to List<ProjectAdditionalCostEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<ProjectAdditionalCostEntity> TransferToDtoList(this List<ProjectAdditionalCostDto> dto)
        {
            List<ProjectAdditionalCostEntity> entities = dto.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }
    }
}
