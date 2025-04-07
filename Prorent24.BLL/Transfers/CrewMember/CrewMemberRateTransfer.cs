using Prorent24.BLL.Models.CrewMember;
using Prorent24.Entity.CrewMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.CrewMemberRate
{
    public static class CrewMemberRateTransfer
    {
        /// <summary>
        /// Transfer from List<CrewMemberRateEntity> to List<CrewMemberRateDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<CrewMemberRateDto> TransferToListDto(this List<CrewMemberRateEntity> entities)
        {
            List<CrewMemberRateDto> crewMemberDtos = entities.Select(x => x.TransferToDto()).ToList();

            return crewMemberDtos;
        }

        /// <summary>
        /// Transfer from CrewMemberRateDto to CrewMemberRateEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static CrewMemberRateDto TransferToDto(this CrewMemberRateEntity entity)
        {
            CrewMemberRateDto crewMemberDto = new CrewMemberRateDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                CrewMemberId = entity.CrewMemberId,
                DailyRate = entity.DailyRate,
                HourlyRate = entity.HourlyRate,
                MaxNumberOfTimeUnit = entity.MaxNumberOfTimeUnit,
                TimeUnit = entity.TimeUnit
            };

            return crewMemberDto;
        }

        /// <summary>
        /// Transfer from CrewMemberRateEntity to CrewMemberRateDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static CrewMemberRateEntity TransferToEntity(this CrewMemberRateDto dto)
        {
            CrewMemberRateEntity crewMemberEntity = new CrewMemberRateEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                CrewMemberId = dto.CrewMemberId,
                DailyRate = dto.DailyRate,
                HourlyRate = dto.HourlyRate,
                MaxNumberOfTimeUnit = dto.MaxNumberOfTimeUnit,
                TimeUnit = dto.TimeUnit
            };

            return crewMemberEntity;
        }
    }
}
