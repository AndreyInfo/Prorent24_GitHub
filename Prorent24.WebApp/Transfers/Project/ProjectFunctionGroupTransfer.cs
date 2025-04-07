using Prorent24.BLL.Models.Project;
using Prorent24.WebApp.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Project
{
    public static class ProjectProjectFunctionGroupTransfer
    {
        /// <summary>
        /// Transfer from List<ProjectFunctionGroupViewModel> to List<ProjectFunctionGroupDto>
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<ProjectFunctionGroupDto> TransferToListDto(this List<ProjectFunctionGroupViewModel> models)
        {
            List<ProjectFunctionGroupDto> dtos = models.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from ProjectFunctionGroupDto to ProjectFunctionGroupViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ProjectFunctionGroupDto TransferToDto(this ProjectFunctionGroupViewModel model)
        {
            ProjectFunctionGroupDto dto = new ProjectFunctionGroupDto()
            {
                Id = model.Id,
                Name = model.Name,
                UseFrom = model.UseFrom,
                UseUntil = model.UseUntil,
                PlanFrom = model.PlanFrom,
                PlanUntil = model.PlanUntil

            };

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectFunctionGroupViewModel to ProjectFunctionGroupDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectFunctionGroupViewModel TransferToViewModel(this ProjectFunctionGroupDto dto)
        {
            ProjectFunctionGroupViewModel model = new ProjectFunctionGroupViewModel()
            {
                Id = dto.Id,
                UseFrom = dto.UseFrom,
                UseUntil = dto.UseUntil,
                PlanFrom = dto.PlanFrom,
                PlanUntil = dto.PlanUntil,
                Name = dto.Name

            };
            return model;
        }

        /// <summary>
        /// Transfer from List<ProjectFunctionGroupDto> to List<ProjectFunctionGroupViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ProjectFunctionGroupViewModel> TransferToViewModel(this List<ProjectFunctionGroupDto> dtos)
        {
            List<ProjectFunctionGroupViewModel> models = dtos.Select(x => x.TransferToViewModel()).ToList();
            return models;
        }
    }
}
