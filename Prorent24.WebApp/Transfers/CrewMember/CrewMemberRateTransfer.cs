using Prorent24.BLL.Models.CrewMember;
using Prorent24.WebApp.Models.CrewMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.CrewMember
{
    public static class CrewMemberRateTransfer
    {

        /// <summary>
        /// Transfer from List<CrewMemberRateViewModel> to List<CrewMemberRateDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<CrewMemberRateDto> TransferToListDto(this List<CrewMemberRateViewModel> entities)
        {
            List<CrewMemberRateDto> crewMemberDtos = entities.Select(x => x.TransferToDto()).ToList();

            return crewMemberDtos;
        }

        /// <summary>
        /// Transfer from CrewMemberRateViewModel to CrewMemberRateDto
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CrewMemberRateDto TransferToDto(this CrewMemberRateViewModel model)
        {
            CrewMemberRateDto crewMemberDto = new CrewMemberRateDto()
            {
                Id = model.Id,
                Name = model.Name,
                CrewMemberId = model.CrewMemberId,
                DailyRate = model.DailyRate,
                HourlyRate = model.HourlyRate,
                MaxNumberOfTimeUnit = model.MaxNumberOfTimeUnit,
                TimeUnit = model.TimeUnit
            };

            return crewMemberDto;
        }

        /// <summary>
        /// Transfer from CrewMemberRateDto to CrewMemberRateViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static CrewMemberRateViewModel TransferToViewModel(this CrewMemberRateDto dto)
        {
            CrewMemberRateViewModel crewMemberView = new CrewMemberRateViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                CrewMemberId = dto.CrewMemberId,
                DailyRate = dto.DailyRate,
                HourlyRate = dto.HourlyRate,
                MaxNumberOfTimeUnit = dto.MaxNumberOfTimeUnit,
                TimeUnit = dto.TimeUnit
            };

            return crewMemberView;
        }
    }
}
