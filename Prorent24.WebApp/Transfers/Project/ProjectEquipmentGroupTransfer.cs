using Prorent24.BLL.Models.Project;
using Prorent24.WebApp.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Project
{
    public static class ProjectEquipmentGroupTransfer
    {
        /// <summary>
        /// Transfer from List<ProjectEquipmentGroupViewModel> to List<ProjectEquipmentGroupDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectEquipmentGroupDto> TransferToListDto(this List<ProjectEquipmentGroupViewModel> views)
        {
            List<ProjectEquipmentGroupDto> dtos = views.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from  List<ProjectEquipmentGroupDto> to List<ProjectEquipmentGroupViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ProjectEquipmentGroupViewModel> TransferToListViewModel(this List<ProjectEquipmentGroupDto> dtos)
        {
            List<ProjectEquipmentGroupViewModel> views = dtos.Select(x => x.TransferToViewModel()).ToList();
            return views;
        }


        /// <summary>
        /// Transfer from ProjectEquipmentGroupViewModel to ProjectEquipmentGroupDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static ProjectEquipmentGroupDto TransferToDto(this ProjectEquipmentGroupViewModel view, string exclude = null)
        {
            ProjectEquipmentGroupDto dto = new ProjectEquipmentGroupDto()
            {
                Id = view.Id,
                ProjectId = view.ProjectId,
                Name = view.Name,
                Equipments = view.Equipments?.ToList().TransferToListDto(),
                StartPlanPeriod = view.StartPlanPeriod,
                StartUsePeriod = view.StartUsePeriod,
                EndPlanPeriod = view.EndPlanPeriod,
                EndUsePeriod = view.EndUsePeriod
            };

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectEquipmentGroupDto to ProjectEquipmentGroupViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectEquipmentGroupViewModel TransferToViewModel(this ProjectEquipmentGroupDto dto)
        {
            ProjectEquipmentGroupViewModel entity = new ProjectEquipmentGroupViewModel()
            {
                Id = dto.Id,
                ProjectId = dto.ProjectId,
                Name = dto.Name,
                Equipments = dto.Equipments?.ToList().TransferToListViewModel(),
            };

            return entity;
        }
    }
}
