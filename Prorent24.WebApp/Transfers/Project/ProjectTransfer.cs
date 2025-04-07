using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.Project;
using Prorent24.Enum.Project;
using Prorent24.WebApp.Models.Project;
using Prorent24.WebApp.Transfers.Configurations.Settings;
using Prorent24.WebApp.Transfers.Contact;
using Prorent24.WebApp.Transfers.ContactPerson;
using Prorent24.WebApp.Transfers.CrewMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Project
{
    /// <summary>
    /// 
    /// </summary>
    public static class ProjectTransfer
    {
        /// <summary>
        /// Transfer from List<ProjectViewModel> to List<ProjectDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static ProjectWarhouseListViewModel TransferToListView(this List<ProjectDto> dtos)
        {
            ProjectWarhouseListViewModel projectWarhouseListViewModel = new ProjectWarhouseListViewModel()
            {
                Pack = dtos.TransferToView(ProjectStatusEnum.Confirmed),
                Prepped = dtos.TransferToView(ProjectStatusEnum.Packed),
                OnLocation = dtos.TransferToView(ProjectStatusEnum.OnLocation),
                InUse = dtos.TransferToView(ProjectStatusEnum.InUse)
            };

            return projectWarhouseListViewModel;

        }

        /// <summary>
        /// Transfer from List<ProjectDto> & ProjectStatusEnum to ProjectWarhouseViewModel
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectWarhouseViewModel> TransferToView(this List<ProjectDto> dtos, ProjectStatusEnum status)
        {
            List<ProjectWarhouseViewModel> projectWarhouseViewModels = dtos.Where(x => DateTime.UtcNow > x.EndPlanPeriod && x.Status == status || x.Status == status).Select(x => new ProjectWarhouseViewModel()
            {
                Id = x.Id,
                Code = x.Number,
                Title = x.Name,
                EndPlaning = x.StartPlanPeriod,
                StartPlaning = x.EndPlanPeriod,
                Resource = x.Reference,
            }).ToList();

            return projectWarhouseViewModels;

        }

        /// <summary>
        /// Transfer from List<ProjectViewModel> to List<ProjectDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectDto> TransferToListDto(this List<ProjectViewModel> models)
        {
            List<ProjectDto> dtos = models.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from ProjectViewModel to ProjectDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectDto TransferToDto(this ProjectViewModel model, string exclude = null)
        {
            // 
            ProjectDto dto = new ProjectDto()
            {
                Id = model.Id,
                Name = model.Name,
                Number = model.Number,
                Status = model.Status,
                ClientContactId = model.ClientContactId,
                //ClientContact = model.ClientContact?.TransferToDto(),
                ClientContactPersonId = model.ClientContactPersonId,
                //ClientContactPerson = model.ClientContactPerson?.TransferToDto(),
                LocationContactId = model.LocationContactId,
                //LocationContact = model.LocationContact?.TransferToDto(),
                LocationContactPersonId = model.LocationContactPersonId,
                //LocationContactPerson = model.LocationContactPerson?.TransferToDto(),
                AccountManagerId = model.AccountManagerId,
                //AccountManager = model.AccountManager?.TransferToDto(),
                Reference = model.Reference,
                Color = model.Color,
                TypeId = model.TypeId,
                // Type = model.Type,
                Times = model.Times?.TransferToListDto(),
                //Notes = model.Notes?.TransferToListDto(),
                IsPublic = model.IsPublic,
                CrewMembers = model.CrewMembers != null ? model.CrewMembers.TransferToListDto() : new List<CrewMemberShortDto>(),
                Description = model.Description
            };

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectDto to ProjectViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectViewModel TransferToViewModel(this ProjectDto dto)
        {
            ProjectViewModel model = new ProjectViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                Number = dto.Number,
                Status = dto.Status,
                ClientContactId = dto.ClientContactId,
                ClientContact = dto.ClientContact?.TransferToViewModel(),
                ClientContactPersonId = dto.ClientContactPersonId,
                ClientContactPerson = dto.ClientContactPerson?.TransferToViewModel(dto.ClientContact),
                LocationContactId = dto.LocationContactId,
                LocationContact = dto.LocationContact?.TransferToViewModel(),
                LocationContactPersonId = dto.LocationContactPersonId,
                LocationContactPerson = dto.LocationContactPerson?.TransferToViewModel(dto.LocationContact),
                AccountManagerId = dto.AccountManagerId,
                AccountManager = dto.AccountManager?.TransferToViewModel(),
                Reference = dto.Reference,
                Color = dto.Color,
                TypeId = dto.TypeId,
                Type = dto.Type?.TransferToViewModel(),
                Times = dto.Times?.TransferToListViewModel(),
                //Notes = dto.Notes?.ToList().TransferToListEntity(),
                IsPublic = dto.IsPublic,
                CrewMembers = dto.CrewMembers?.TransferToListModel(),
                Description = dto.Description
            };

            return model;
        }
    }
}
