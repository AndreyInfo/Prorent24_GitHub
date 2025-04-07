using Prorent24.BLL.Models.Project;
using Prorent24.WebApp.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Project
{
    public static class ProjectTimeTransfer
    {
        /// <summary>
        /// Transfer from List<ProjectTimeViewModel> to List<ProjectTimeDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectTimeDto> TransferToListDto(this List<ProjectTimeViewModel> entities)
        {
            List<ProjectTimeDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from  List<ProjectTimeDto> to List<ProjectTimeViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ProjectTimeViewModel> TransferToListViewModel(this List<ProjectTimeDto> dtos)
        {
            List<ProjectTimeViewModel> view = dtos.Select(x => x.TransferToViewModel()).ToList();
            return view;
        }


        /// <summary>
        /// Transfer from ProjectTimeViewModel to ProjectTimeDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectTimeDto TransferToDto(this ProjectTimeViewModel view, string exclude = null)
        {
            ProjectTimeDto dto = new ProjectTimeDto()
            {
                Id = view.Id,
                Name = view.Name,
                ProjectId = view.ProjectId,
                From = view.From,
                Until = view.Until,
                DisplayContract = view.DisplayContract,
                DisplayQuotation = view.DisplayQuotation,
                DisplayPackSlip = view.DisplayPackSlip,
                DefaultPlanPeriod = view.DefaultPlanPeriod,
                DefaultUsagePeriod = view.DefaultUsagePeriod,
            };

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectTimeDto to ProjectTimeViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectTimeViewModel TransferToViewModel(this ProjectTimeDto dto)
        {
            ProjectTimeViewModel entity = new ProjectTimeViewModel()
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
            };

            return entity;
        }
    }
}
