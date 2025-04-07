using Prorent24.BLL.Models.Project;
using Prorent24.WebApp.Models.CrewPlanner;
using Prorent24.WebApp.Models.Project;
using Prorent24.WebApp.Transfers.CrewMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Project
{
    public static class ProjectPlanningTransfer
    {
        /// <summary>
        /// Transfer from List<ProjectPlanningViewModel> to List<ProjectPlanningDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectPlanningDto> TransferToListDto(this List<ProjectPlanningViewModel> entities)
        {
            List<ProjectPlanningDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from  List<ProjectPlanningDto> to List<ProjectPlanningViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ProjectPlanningViewModel> TransferToListViewModel(this List<ProjectPlanningDto> dtos)
        {
            List<ProjectPlanningViewModel> view = dtos.Select(x => x.TransferToViewModel()).ToList();
            return view;
        }


        /// <summary>
        /// Transfer from ProjectPlanningViewModel to ProjectPlanningDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectPlanningDto TransferToDto(this ProjectPlanningViewModel view, string exclude = null)
        {
            ProjectPlanningDto dto = new ProjectPlanningDto()
            {
                Id = view.Id,
                Type = view.FunctionType!=0 ? view.FunctionType : view.Type,
                FunctionId = view.FunctionId,
                EntityId = view.EntityId,
                VisibleToCrewMember = view.VisibleToCrewMember,
                ProjectLeader = view.ProjectLeader,
                RateType = view.RateType,
                Costs = view.Costs,
                PlannedCosts = view.PlannedCosts,
                ActualCosts = view.ActualCosts,
                TransportType = view.TransportType,
                PlanFrom = view.PlanFrom,
                PlanUntil =  view.PlanUntil ,
                Remark = view.Remark
            };

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectPlanningDto to ProjectPlanningViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectPlanningViewModel TransferToViewModel(this ProjectPlanningDto dto)
        {
            ProjectPlanningViewModel entity = new ProjectPlanningViewModel()
            {
                Id = dto.Id,
                FunctionType = dto.Type,
                FunctionId = dto.FunctionId,
                FunctionName = dto.FunctionName,
                EntityId = dto.EntityId,
                Name = dto.EntityName,
                VisibleToCrewMember = dto.VisibleToCrewMember,
                ProjectLeader = dto.ProjectLeader,
                CrewMemberRate = dto.CrewMemberRate?.TransferToViewModel(),
                RateType = dto.RateType,
                Costs = dto.Costs,
                PlannedCosts = dto.PlannedCosts,
                ActualCosts = dto.ActualCosts,
                TransportType = dto.TransportType,
                PlanFrom = dto.PlanFrom,
                PlanUntil = dto.PlanUntil,
                Remark = dto.Remark
            };

            return entity;
        }

        /// <summary>
        /// Transfer from ProjectPlanningViewModel to ProjectPlanningDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<ProjectPlanningDto> TransferToListDto(this CrewPlannerProjectPlanningModel view)
        {
            List<ProjectPlanningDto> dto = view.PlanningEntities.Select(x=> new ProjectPlanningDto
            {
                Type = view.Type,
                FunctionId = view.FunctionId,
                EntityId = x.EntityId,
                PlanFrom = view.Start,
                PlanUntil = view.End,
                
            }).ToList();

            return dto;
        }
    }
}
