using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Transfers.Configuration.Financial;
using Prorent24.Common.Extentions;
using Prorent24.Entity.Project;
using Prorent24.Enum.Configuration;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Project
{
    public static class ProjectFunctionTransfer
    {

        /// <summary>
        /// Transfer from List<ProjectFunctionEntity> to List<ProjectFunctionDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectFunctionDto> TransferToListDto(this List<ProjectFunctionEntity> entities, List<ProjectPlanningEntity> plannings = null)
        {

            List<ProjectFunctionDto> dtos = entities.Select(x => x.TransferToDto(null, plannings == null ? null : plannings.Where(y => y.FunctionId == x.Id).ToList())).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from  List<ProjectFunctionDto> to List<ProjectFunctionEntity>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ProjectFunctionEntity> TransferToListEntity(this List<ProjectFunctionDto> dtos)
        {



            List<ProjectFunctionEntity> entities = dtos.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }


        /// <summary>
        /// Transfer from ProjectFunctionEntity to ProjectFunctionDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectFunctionDto TransferToDto(this ProjectFunctionEntity entity, string exclude = null, List<ProjectPlanningEntity> plannings = null)
        {
            ProjectFunctionDto dto = new ProjectFunctionDto()
            {
                Id = entity.Id,
                ProjectId = entity.ProjectId,
                Type = entity.Type,
                InternalName = entity.InternalName,
                ExternalName = entity.ExternalName,
                TimeBeforeInMinutes = entity.TimeBeforeInMinutes,
                TimeBeforeType = entity.TimeBeforeType,
                TimeAfterInMinutes = entity.TimeAfterInMinutes,
                TimeAfterType = entity.TimeAfterType,
                TakeTimeFromLocation = entity.TakeTimeFromLocation,
                DurationInMinutes = entity.DurationInMinutes,
                DurationType = entity.DurationType,
                BreakInMinutes = entity.BreakInMinutes,
                BreakType = entity.BreakType,
                SubhireFixed = entity.SubhireFixed,
                SubhireHourRate = entity.SubhireHourRate,
                RentalFixed = entity.RentalFixed,
                RentalHourRate = entity.RentalHourRate,
                VatClassId = entity.VatClassId,
                VatClass = entity.VatClass?.TransferToVatClassDto(),
                ShowInPlaner = entity.ShowInPlaner,
                IncludeInPrice = entity.IncludeInPrice,
                CrewMemberRemark = entity.CrewMemberRemark,
                CustomerRemark = entity.CustomerRemark,
                PlannerRemark = entity.PlannerRemark,
                Quantity = entity.Quantity,
                PlanFromTimeType = entity.PlanFromTimeType,
                PlanFrom = entity.PlanFrom,
                PlanUntilTimeType = entity.PlanUntilTimeType,
                PlanUntil = entity.PlanUntil,
                UseFromTimeType = entity.UseFromTimeType,
                UseFrom = entity.UseFrom,
                UseUntilTimeType = entity.UseUntilTimeType,
                UseUntil = entity.UseUntil,
                TimeFrameId = entity.TimeFrameId,
                ProjectTime = entity.ProjectTime?.TransferToDto(),
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate,
                //Distance = (decimal)entity.Distance
                Distance = entity.Distance.HasValue ? entity.Distance.Value : 0,
                ProjectFunctionGroupId = entity.ProjectFunctionGroupId,
                ParentFunctionId = entity.ParentFunctionId,
                TotalIncVat = entity.TotalIncVat,
                TotalCosts = entity.TotalCosts,
                TotalPrice = entity.TotalPrice

            };

            if (plannings != null)
            {
                dto.Childrens = plannings.TransferToListDto();
            }

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectFunctionDto to ProjectFunctionEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectFunctionEntity TransferToEntity(this ProjectFunctionDto dto)
        {
            ProjectFunctionEntity entity = new ProjectFunctionEntity()
            {
                Id = dto.Id,
                ProjectId = dto.ProjectId,
                Type = dto.Type,
                InternalName = dto.InternalName,
                ExternalName = dto.ExternalName,
                TimeBeforeInMinutes = dto.TimeBeforeInMinutes,
                TimeBeforeType = dto.TimeBeforeType,
                TimeAfterInMinutes = dto.TimeAfterInMinutes,
                TimeAfterType = dto.TimeAfterType,
                TakeTimeFromLocation = dto.TakeTimeFromLocation,
                DurationInMinutes = dto.DurationInMinutes,
                DurationType = dto.DurationType,
                BreakInMinutes = dto.BreakInMinutes,
                BreakType = dto.BreakType,
                SubhireFixed = dto.SubhireFixed,
                SubhireHourRate = dto.SubhireHourRate,
                RentalFixed = dto.RentalFixed,
                RentalHourRate = dto.RentalHourRate,
                VatClassId = dto.VatClassId,
                VatClass = dto.VatClass?.TransferToVatClassEntity(),
                ShowInPlaner = dto.ShowInPlaner,
                IncludeInPrice = dto.IncludeInPrice,
                CrewMemberRemark = dto.CrewMemberRemark,
                CustomerRemark = dto.CustomerRemark,
                PlannerRemark = dto.PlannerRemark,
                Quantity = dto.Quantity,
                PlanFromTimeType = dto.PlanFromTimeType,
                PlanFrom = dto.PlanFrom,
                PlanUntilTimeType = dto.PlanUntilTimeType,
                PlanUntil = dto.PlanUntil,
                UseFromTimeType = dto.UseFromTimeType,
                UseFrom = dto.UseFrom,
                UseUntilTimeType = dto.UseUntilTimeType,
                UseUntil = dto.UseUntil,
                TimeFrameId = dto.TimeFrameId,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate,
                Distance = dto.Distance,
                ProjectFunctionGroupId = dto.ProjectFunctionGroupId,
                ParentFunctionId = dto.ParentFunctionId,
                TotalIncVat = dto.TotalIncVat
            };

            return entity;
        }

        public static int CalculateFunctionDurationUsage(this ProjectFunctionDto model)
        {
            try
            {
                if (model.GlobalAddTimeType == ProjectFunctionAddTimeEnum.Duration)
                {
                    return model.DurationInMinutes;
                }
                else if (model.UseFrom != null && model.UseUntil != null && model.UseUntil > model.UseFrom)//(model.GlobalAddTimeType == ProjectFunctionAddTimeEnum.ExactTime)
                {
                    TimeSpan time = (DateTime)model.UseUntil - (DateTime)model.UseFrom;
                    return (int)time.TotalMinutes;
                }
                else
                {
                    return model.DurationInMinutes;
                    //return null;
                }
            }
            catch
            {
                return 0;
            }


        }

        public static decimal CalculateFunctionCrewTotalCosts(this ProjectFunctionDto model, int durationUsage)
        {
            try
            {
                return (model.SubhireHourRate * durationUsage / 60 + model.SubhireFixed) * (int)model.Quantity;
            }
            catch
            {
                return 0;
            }
        }

        public static decimal CalculateFunctionCrewTotalPrice(this ProjectFunctionDto model, int durationUsage)
        {
            try
            {
                return (model.RentalHourRate * durationUsage / 60 + model.RentalFixed) * (int)model.Quantity;
            }
            catch
            {
                return 0;
            }
        }

        public static decimal CalculateFunctionTransportTotalCosts(this ProjectFunctionDto model, decimal distance)
        {
            try
            {
                return (model.SubhireHourRate * distance + model.SubhireFixed) * (int)model.Quantity;
            }
            catch
            {
                return 0;
            }
        }

        public static decimal CalculateFunctionTransportTotalPrice(this ProjectFunctionDto model, decimal distance)
        {
            try
            {
                return (model.RentalHourRate * distance + model.RentalFixed) * (int)model.Quantity;
            }
            catch
            {
                return 0;
            }
        }
        public static decimal CalculateTotalIncVat(this ProjectFunctionEntity model, VatSchemeRateDto vatScheme)
        {
            try
            {
                decimal totalIncVat = 0;
                if (vatScheme.Type == VatSchemeTypeEnum.Rates)
                {
                    totalIncVat = Convert.ToDecimal(model.TotalPrice * (100 + vatScheme.VatScheme.VatClassSchemeRates?.Where(y => y.VatClassId == model.VatClassId).FirstOrDefault()?.Rate) / 100);
                }
                else if (vatScheme.Type == VatSchemeTypeEnum.FixedRate)
                {
                    totalIncVat = Convert.ToDecimal(model.TotalPrice) * (100 + vatScheme.Rate) / 100;

                }
                else if (vatScheme.Type == VatSchemeTypeEnum.VatReverseCharge)
                {
                    totalIncVat = Convert.ToDecimal(model.TotalPrice) * 100 / (100 + vatScheme.Rate);

                }
                return totalIncVat;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Transfer from List<ProjectFunctionEntity> to List<FunctionDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<FunctionDto> TransferToListFunctionDto(this List<ProjectFunctionEntity> entities)
        {
            List<FunctionDto> dtos = entities.Select(x => x.TransferToFunctionDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from ProjectFunctionEntity to FunctionDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static FunctionDto TransferToFunctionDto(this ProjectFunctionEntity entity, string exclude = null)
        {
            FunctionDto dto = new FunctionDto()
            {
                Id = entity.Id,
                Type = entity.Type,
                InternalName = entity.InternalName,
                Quantity = entity.Quantity,
                IncludeInPrice = entity.IncludeInPrice,
                ShowInPlaner = entity.ShowInPlaner,
                GroupType = entity.Type

            };

            return dto;
        }

        /// <summary>
        /// Transfer from List<ProjectFunctionGroupEntity> to List<ProjectFunctionGridDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectFunctionGridDto> TransferToListGridDto(this List<ProjectFunctionEntity> entities)
        {

            List<ProjectFunctionGridDto> dtos = entities.Select(x => x.TransferToGridDto(null)).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from ProjectFunctionGroupEntity to ProjectFunctionGridDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectFunctionGridDto TransferToGridDto(this ProjectFunctionEntity entity, string exclude = null)
        {
            ProjectFunctionGridDto dto = new ProjectFunctionGridDto()
            {
                Id = entity.Id,
                ProjectId = entity.ProjectId,
                FunctionId = entity.Id,
                FunctionName = entity.InternalName,
                UseFrom = entity.UseFrom,
                UseUntil = entity.UseUntil,
                Type = entity.Type,
                IncludeInPrice = entity.IncludeInPrice,
                ShowInPlaner = entity.ShowInPlaner,
                Quantity = entity.Quantity,
                TotalCosts = entity.TotalCosts,
                TotalPrice = entity.TotalPrice,
                RentalFixed = entity.RentalFixed,
                RentalHourRate = entity.RentalHourRate,
                SubhireFixed = entity.SubhireFixed,
                SubhireHourRate = entity.SubhireHourRate

            };
            return dto;
        }
    }
}
