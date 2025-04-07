using Prorent24.BLL.Models.Project;
using Prorent24.WebApp.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Project
{
    public static class ProjectFinancialCategoryTransfer
    {
        /// <summary>
        /// Transfer from ProjectFinancialCategoryDto to ProjectFinancialCategoryViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ProjectFinancialCategoryDto TransferToDto(this ProjectFinancialCategoryViewModel model)
        {
            ProjectFinancialCategoryDto dto = new ProjectFinancialCategoryDto()
            {
                Id = model.Id,
                ProjectId = model.ProjectId,
                Category = model.Category,
                ParentId = model.ParentId,
                Name = model.Name,
                ActualCosts = model.ActualCosts,
                EstimatedCosts = model.EstimatedCosts,
                PlannedCosts = model.PlannedCosts,
                Discount = model.Discount,
                Profit = model.Profit,
                Revenue = model.Revenue,
                Total = model.Total,
                TotalIncVat = model.TotalIncVat

            };

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectFinancialCategoryViewModel to ProjectFinancialCategoryDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectFinancialCategoryViewModel TransferToViewModel(this ProjectFinancialCategoryDto dto)
        {
            ProjectFinancialCategoryViewModel model = new ProjectFinancialCategoryViewModel()
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
                TotalIncVat = dto.TotalIncVat,
                Children = dto.Children?.ToList().TransferToListViewModel()

            };
            return model;
        }

        /// <summary>
        /// Transfer from List<ProjectFinancialCategoryDto> to List<ProjectFinancialCategoryViewModel>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<ProjectFinancialCategoryViewModel> TransferToListViewModel(this List<ProjectFinancialCategoryDto> dtos)
        {
            List<ProjectFinancialCategoryViewModel> views = dtos.Select(x => x.TransferToViewModel()).ToList();

            return views;
        }
    }
}
