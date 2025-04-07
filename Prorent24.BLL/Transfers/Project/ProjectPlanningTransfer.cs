using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Transfers.CrewMemberRate;
using Prorent24.Common.Extentions;
using Prorent24.DAL.Models.Project;
using Prorent24.Entity.Project;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Project
{
    public static class ProjectPlanningTransfer
    {
        /// <summary>
        /// Transfer from List<ProjectPlanningEntity> to List<ProjectPlanningDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectPlanningDto> TransferToListDto(this List<ProjectPlanningEntity> entities)
        {
            List<ProjectPlanningDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from  List<ProjectPlanningDto> to List<ProjectPlanningEntity>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ProjectPlanningEntity> TransferToListEntity(this List<ProjectPlanningDto> dtos)
        {
            List<ProjectPlanningEntity> entities = dtos.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }


        /// <summary>
        /// Transfer from ProjectPlanningEntity to ProjectPlanningDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectPlanningDto TransferToDto(this ProjectPlanningEntity entity, ProjectFunctionTypeEnum? type = null, string exclude = null)
        {
            ProjectPlanningDto dto = new ProjectPlanningDto()
            {
                Id = entity.Id,
                Type = entity.Function.Type,
                FunctionId = entity.FunctionId,
                FunctionName = entity.Function?.ExternalName,
                EntityId = entity.VehicleId != null ? Convert.ToString(entity.VehicleId) : entity.CrewMemberId,
                EntityName = entity.VehicleId != null ? entity.Vehicle?.Name : entity.CrewMember?.User?.FirstName + " " + entity.CrewMember?.User?.LastName,
                VisibleToCrewMember = entity.VisibleToCrewMember,
                ProjectLeader = entity.ProjectLeader,
                RateType = entity.RateType,
                CrewMemberHourlyRate = entity.CrewMemberHourlyRate,
                CrewMemberRateId = entity.CrewMemberRateId,
                CrewMemberRate = entity.CrewMemberRate?.TransferToDto(),
                Costs = entity.Costs,
                PlannedCosts = entity.PlannedCosts,
                ActualCosts = entity.ActualCosts,
                TransportType = entity.TransportType,
                PlanFrom = entity.PlanFrom??entity.Function?.ProjectFunctionGroup?.PlanFrom,
                PlanUntil = entity.PlanUntil??entity.Function?.ProjectFunctionGroup?.PlanUntil,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate,
                Remark = entity.Remark,
                ProjectFunction = entity.Function?.TransferToDto()
                

    };

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectPlanningDto to ProjectPlanningEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectPlanningEntity TransferToEntity(this ProjectPlanningDto dto)
        {
            ProjectPlanningEntity entity = new ProjectPlanningEntity()
            {
                Id = dto.Id,
                FunctionId = dto.FunctionId,
                CrewMemberId = dto.Type == ProjectFunctionTypeEnum.Crew ? dto.EntityId : null,
                VehicleId = dto.Type == ProjectFunctionTypeEnum.Transport ? (int?)Convert.ToInt32(dto.EntityId) : null,
                VisibleToCrewMember = dto.VisibleToCrewMember,
                ProjectLeader = dto.ProjectLeader,
                RateType = dto.RateType,
                CrewMemberHourlyRate = dto.CrewMemberHourlyRate,
                CrewMemberRateId = dto.CrewMemberRateId,
                Costs = dto.Costs,
                PlannedCosts = dto.PlannedCosts,
                ActualCosts = dto.ActualCosts,
                TransportType = dto.TransportType,
                PlanFrom = dto.PlanFrom,
                PlanUntil = dto.PlanUntil,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate,
                Remark = dto.Remark
            };

            return entity;
        }

        public static ProjectPlanningFilterModel TransferToDalModel(this ProjectPlanningFilter dto)
        {
            ProjectPlanningFilterModel filter = new ProjectPlanningFilterModel()
            {
                Type = dto.Type,
                ProjectId = dto.ProjectId,
                FunctionGroupId = dto.FunctionGroupId
            };

            return filter;
        }

        public static int CalculateDurationPlanned(this ProjectPlanningDto model)
        {
            try
            {
                TimeSpan time = (DateTime)model.PlanUntil - (DateTime)model.PlanFrom;
                return (int)time.TotalMinutes;
            }
            catch
            {
                return 0;
            }
        }


        public static decimal CalculatePlannedCosts(this ProjectPlanningDto model, int durationUsage)
        {
            try
            {
                if (model.RateType == PlanningCrewMemberRateEnum.PriceAgreement)
                {
                    return Convert.ToDecimal(model.Costs);
                }
                else
                {
                    return (model.CrewMemberHourlyRate.HasValue ? model.CrewMemberHourlyRate.Value : 0) * durationUsage / 60;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static ProjectPlanningGridDto TransferToTreeGridDto(this ProjectPlanningDto planningDto)
        {
            ProjectPlanningGridDto dto = new ProjectPlanningGridDto()
            {
                ParentId = planningDto.FunctionId,
                EntityId = planningDto.EntityId,
                EntityName = planningDto.EntityName,
                PlanFrom = planningDto.PlanFrom,
                PlanUntil = planningDto.PlanUntil,
                ProjectLeader = planningDto.ProjectLeader,
                VisibleToCrewMember = planningDto.VisibleToCrewMember,
                Remark = planningDto.Remark,
                Id = planningDto.Id,
                FunctionType = planningDto.Type,
                ProjectFunction = planningDto.ProjectFunction
            };

            return dto;
        }

        public static List<ProjectPlanningGridDto> TransferToTreeGridDto(this List<ProjectPlanningDto> planningDtos)
        {
            List<ProjectPlanningGridDto> dtos = planningDtos.Select(x => x.TransferToTreeGridDto()).ToList();

            return dtos;
        }
        public static ProjectPlanningGridDto TransferToTreeGridDto(this ProjectFunctionDto functionDto)
        {
            ProjectPlanningGridDto dto = new ProjectPlanningGridDto()
            {
                Quantity = functionDto.Quantity.HasValue ? functionDto.Quantity.Value : 1,
                ProjectFunction = functionDto,
                PlanFrom = functionDto.PlanFrom,
                PlanUntil = functionDto.PlanUntil,
                Childrens = functionDto.Childrens.TransferToTreeGridDto(),
                Id = functionDto.Id,
                FunctionQuantity = null,
                FunctionType = functionDto.Type
            };

            return dto;
        }

        public static List<ProjectPlanningGridDto> TransferToTreeGridDto(this List<ProjectFunctionDto> functionDtos)
        {
            List<ProjectPlanningGridDto> dtos = functionDtos.Select(x => x.TransferToTreeGridDto()).ToList();
            return dtos;
        }
    }
}
