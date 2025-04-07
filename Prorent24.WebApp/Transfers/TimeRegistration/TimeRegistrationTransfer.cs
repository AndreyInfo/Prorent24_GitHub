using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.TimeRegistration;
using Prorent24.WebApp.Models.TimeRegistration;
using Prorent24.WebApp.Transfers.CrewMember;
using Prorent24.WebApp.Transfers.TimeRegistrationActivity;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.WebApp.Transfers.TimeRegistration
{
    public static class TimeRegistrationTransfer
    {
        /// <summary>
        /// Transfer from List<TimeRegistrationDto> to List<TimeRegistrationViewModel>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<TimeRegistrationViewModel> TransferToListModels(this List<TimeRegistrationDto> dtos)
        {
            List<TimeRegistrationViewModel> timeRegistrationModels = dtos.Select(x => x.TransferToViewModel()).ToList();

            return timeRegistrationModels;
        }

        /// <summary>
        /// Transfer from TimeRegistrationViewModel to TimeRegistrationDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static TimeRegistrationViewModel TransferToViewModel(this TimeRegistrationDto dto)
        {
            TimeRegistrationViewModel timeRegistrationDto = new TimeRegistrationViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                CrewMemberId = dto.CrewMemberId,
                CrewMember = dto.CrewMember,
                BreakDuration = dto.BreakDuration,
                BreakTimeUnit = dto.BreakTimeUnit,
                From = dto.From,
                Until = dto.Until,
                HourRegistrationType = dto.HourRegistrationType,
                Lunch = dto.Lunch,
                Distance = dto.Distance,
                Remark = dto.Remark,
                TravelDuration = dto.TravelDuration,
                TravelTimeUnit = dto.TravelTimeUnit,
                Activities = dto.Activities!=null? dto.Activities.TransferToListViewModels() : null,
                CrewMembers = dto.CrewMembers!=null ? dto.CrewMembers.Select(x=> x.Id).ToList() : new List<string>()
            };

            return timeRegistrationDto;
        }

        /// <summary>
        /// Transfer from TimeRegistrationDto to TimeRegistrationViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static TimeRegistrationDto TransferToDto(this TimeRegistrationViewModel model)
        {
            TimeRegistrationDto timeRegistrationEntity = new TimeRegistrationDto()
            {
                Id = model.Id,
                Name = model.Name,
                CrewMemberId = model.CrewMembers?.FirstOrDefault() ?? model.CrewMemberId,
                BreakDuration = model.BreakDuration,
                BreakTimeUnit = model.BreakTimeUnit,
                From = model.From,
                Until = model.Until,
                HourRegistrationType = model.HourRegistrationType,
                Lunch = model.Lunch,
                Distance = model.Distance,
                Remark = model.Remark,
                TravelDuration = model.TravelDuration,
                TravelTimeUnit = model.TravelTimeUnit,
                Activities = model.Activities != null ? model.Activities.TransferToListDto() : null,
                CrewMembers = model.CrewMembers != null ? model.CrewMembers.Select(x=>new CrewMemberShortDto()
                {
                    Id = x
                }).ToList(): new List<CrewMemberShortDto>()
            };
            return timeRegistrationEntity;
        }

    }
}
