using Prorent24.BLL.Models.Project;
using Prorent24.Entity.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Project
{
    public static class ProjectFinancialCategoryTransfer
    {
        /// <summary>
        /// Transfer from ProjectFinancialCategoryEntity to ProjectFinancialCategoryDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectFinancialCategoryEntity TransferToEntity(this ProjectFinancialCategoryDto dto)
        {
            ProjectFinancialCategoryEntity entity = new ProjectFinancialCategoryEntity()
            {
                Id = dto.Id,
                ProjectId = dto.ProjectId,
                Category = dto.Category,
                ParentId = dto.ParentId,
                Name = dto.Name,
                ActualCosts = dto.ActualCosts,
                EstimatedCosts = dto.EstimatedCosts,
                PlannedCosts = dto.PlannedCosts,
                Discount = dto.Discount,
                Profit = dto.Profit,
                Revenue = dto.Revenue,
                Total = dto.Total,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate,
                TotalIncVat = dto.TotalIncVat


            };
            return entity;
        }


        /// <summary>
        /// Transfer from ProjectFinancialCategoryEntity to ProjectFinancialCategoryDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectFinancialCategoryDto TransferToDto(this ProjectFinancialCategoryEntity entity, string exclude = null)
        {
            ProjectFinancialCategoryDto dto = new ProjectFinancialCategoryDto()
            {
                Id = entity.Id,
                ProjectId = entity.ProjectId,
                Category = entity.Category,
                ParentId = entity.ParentId,
                Name = entity.Name,
                ActualCosts = entity.ActualCosts,
                EstimatedCosts = entity.EstimatedCosts,
                PlannedCosts = entity.PlannedCosts,
                Discount = entity.Discount,
                Profit = entity.Profit,
                Revenue = entity.Revenue,
                Total = entity.Total,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate,
                TotalIncVat = entity.TotalIncVat.HasValue ? (decimal)entity.TotalIncVat.Value : 0,
                Children = entity.Children?.ToList().TransferToListDto()
            };

            return dto;
        }

        /// <summary>
        /// Transfer from List<ProjectFinancialCategoryEntity> to List<ProjectFinancialCategoryDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectFinancialCategoryDto> TransferToListDto(this List<ProjectFinancialCategoryEntity> entities)
        {
            List<ProjectFinancialCategoryDto> dtos = entities.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }
    }
}
