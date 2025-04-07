using Prorent24.BLL.Models.Project;
using Prorent24.WebApp.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Project
{
    public static class ProjectAdditionalCostTransfer
    {
        /// <summary>
        /// Transfer from List<ProjectAdditionalCostViewModel> to List<ProjectAdditionalCostDto>
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<ProjectAdditionalCostDto> TransferToListDto(this List<ProjectAdditionalCostViewModel> models)
        {
            List<ProjectAdditionalCostDto> dtos = models.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from ProjectAdditionalCostDto to ProjectAdditionalCostViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ProjectAdditionalCostDto TransferToDto(this ProjectAdditionalCostViewModel model)
        {
            ProjectAdditionalCostDto dto = new ProjectAdditionalCostDto()
            {
                Id = model.Id,
                ProjectId = model.ProjectId,
                Name = model.Name,
                Details = model.Details,
                PurchasePrice = model.PurchasePrice,
                SalePrice = model.SalePrice,
                VatClassId = model.VatClassId
            };

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectAdditionalCostViewModel to ProjectAdditionalCostDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectAdditionalCostViewModel TransferToViewModel(this ProjectAdditionalCostDto dto)
        {
            ProjectAdditionalCostViewModel model = new ProjectAdditionalCostViewModel()
            {
                Id = dto.Id,
                ProjectId = dto.ProjectId,
                Name = dto.Name,
                Details = dto.Details,
                PurchasePrice = dto.PurchasePrice,
                SalePrice = dto.SalePrice,
                VatClassId = dto.VatClassId
            };
            return model;
        }

        /// <summary>
        /// Transfer from List<ProjectAdditionalCostDto> to List<ProjectAdditionalCostViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ProjectAdditionalCostViewModel> TransferToViewModel(this List<ProjectAdditionalCostDto> dtos)
        {
            List<ProjectAdditionalCostViewModel> models = dtos.Select(x => x.TransferToViewModel()).ToList();
            return models;
        }
    }
}
