using Prorent24.BLL.Models.Project;
using Prorent24.Enum.Project;
using Prorent24.WebApp.Models.Project;
using Prorent24.WebApp.Transfers.Configurations.Financials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Project
{
    public static class ProjectFunctionTransfer
    {
        /// <summary>
        /// Transfer from List<ProjectFunctionViewModel> to List<ProjectFunctionDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectFunctionDto> TransferToListDto(this List<ProjectFunctionViewModel> entities)
        {
            List<ProjectFunctionDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from  List<ProjectFunctionDto> to List<ProjectFunctionViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ProjectFunctionViewModel> TransferToListViewModel(this List<ProjectFunctionDto> dtos)
        {
            List<ProjectFunctionViewModel> view = dtos.Select(x => x.TransferToViewModel()).ToList();
            return view;
        }


        /// <summary>
        /// Transfer from ProjectFunctionViewModel to ProjectFunctionDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectFunctionDto TransferToDto(this ProjectFunctionViewModel view, string exclude = null)
        {
            ProjectFunctionDto dto = new ProjectFunctionDto()
            {
                //Id = view.Id,
                ProjectId = view.ProjectId,
                Type = view.Type,
                InternalName = view.InternalName,
                ExternalName = view.ExternalName,
                TimeBeforeInMinutes = view.TimeBefore.TransferToMinutes(view.TimeBeforeType),
                TimeBeforeType = view.TimeBeforeType,
                TimeAfterInMinutes = view.TimeAfter.TransferToMinutes(view.TimeAfterType),
                TimeAfterType = view.TimeAfterType,
                TakeTimeFromLocation = view.TakeTimeFromLocation,
                DurationInMinutes = view.Duration.TransferToMinutes(view.DurationType),
                DurationType = view.DurationType,
                BreakInMinutes = view.Break.TransferToMinutes(view.BreakType),
                BreakType = view.BreakType,
                SubhireFixed = view.SubhireFixed,
                SubhireHourRate = view.SubhireHourRate,
                RentalFixed = view.RentalFixed,
                RentalHourRate = view.RentalHourRate,
                VatClassId = view.VatClassId,
                VatClass = view.VatClass?.TransferToDtoModel(),
                ShowInPlaner = view.ShowInPlaner,
                IncludeInPrice = view.IncludeInPrice,
                CrewMemberRemark = view.CrewMemberRemark,
                CustomerRemark = view.CustomerRemark,
                PlannerRemark = view.PlannerRemark,
                Quantity = view.Quantity,
                PlanFromTimeType = view .PlanFromTimeType,
                PlanFrom = view.PlanFrom,
                PlanUntilTimeType = view.PlanUntilTimeType,
                PlanUntil = view.PlanUntil,
                UseFromTimeType = view.UseFromTimeType,
                UseFrom = view.UseFrom,
                UseUntilTimeType = view.UseUntilTimeType,
                UseUntil = view.UseUntil,
                TimeFrameId = view.TimeFrameId,
                Distance = view.Distance,
                ProjectFunctionGroupId = view.GroupId,
                ParentFunctionId = view.ParentFunctionId,
                TotalIncVat = view.TotalIncVat
            };

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectFunctionDto to ProjectFunctionViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectFunctionViewModel TransferToViewModel(this ProjectFunctionDto dto)
        {
            ProjectFunctionViewModel entity = new ProjectFunctionViewModel()
            {
                Id = dto.Id,
                ProjectId = dto.ProjectId,
                Type = dto.Type,
                InternalName = dto.InternalName,
                ExternalName = dto.ExternalName,
                TimeBefore = dto.TimeBeforeInMinutes.TransferToViewTime(dto.TimeBeforeType),
                TimeBeforeType = dto.TimeBeforeType,
                TimeAfter = dto.TimeAfterInMinutes.TransferToViewTime(dto.TimeAfterType),
                TimeAfterType = dto.TimeAfterType,
                TakeTimeFromLocation = dto.TakeTimeFromLocation,
                Duration = dto.DurationInMinutes.TransferToViewTime(dto.DurationType),
                DurationType = dto.DurationType,
                Break = dto.BreakInMinutes.TransferToViewTime(dto.BreakType),
                BreakType = dto.BreakType,
                SubhireFixed = dto.SubhireFixed,
                SubhireHourRate = dto.SubhireHourRate,
                RentalFixed = dto.RentalFixed,
                RentalHourRate = dto.RentalHourRate,
                VatClassId = dto.VatClassId,
                VatClass = dto.VatClass?.TransferToViewModel(),
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
                ProjectTime = dto.ProjectTime?.TransferToViewModel(),
                Distance = dto.Distance,
                GroupId = dto.ProjectFunctionGroupId,
                ParentFunctionId = dto.ParentFunctionId,
                TotalIncVat = dto.TotalIncVat
            };

            return entity;
        }

        public static int TransferToMinutes(this int time, TimeTypeEnum type)
        {
            switch (type)
            {
                case TimeTypeEnum.Minutes:
                    return time;
                case TimeTypeEnum.Hours:
                    return time * 60;
                case TimeTypeEnum.Days:
                    return time * 60 * 24;
                default:
                    return 0;
            }

        }

        public static int TransferToViewTime(this int time, TimeTypeEnum type)
        {
            decimal preResult = 0;
            switch (type)
            {
                case TimeTypeEnum.Minutes:
                    return time;
                case TimeTypeEnum.Hours:
                    preResult = time / 60;
                    return (int)Math.Round(preResult, MidpointRounding.AwayFromZero);
                case TimeTypeEnum.Days:
                    preResult = time / 60 / 24;
                    return (int)Math.Round(preResult, MidpointRounding.AwayFromZero);
                default:
                    return 0;
            }

        }
    }
}
