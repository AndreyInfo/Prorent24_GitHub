using Prorent24.BLL.Models.TimeRegistration;
using Prorent24.Entity.TimeRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.TimeRegistration
{
    public static class TimeRegistrationTransfer
    {
        /// <summary>
        /// Transfer from List<TimeRegistrationEntity> to List<TimeRegistrationDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<TimeRegistrationDto> TransferToListDto(this List<TimeRegistrationEntity> entities)
        {
            List<TimeRegistrationDto> vehicleDtos = entities.Select(x => x.TransferToDto()).ToList();

            return vehicleDtos;
        }

        /// <summary>
        /// Transfer from TimeRegistrationDto to TimeRegistrationEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static TimeRegistrationDto TransferToDto(this TimeRegistrationEntity entity)
        {
            TimeRegistrationDto timeRegistrationDto = new TimeRegistrationDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                CrewMemberId = entity.CrewMemberId,
                CrewMember = string.IsNullOrWhiteSpace(entity.CrewMember?.User?.FirstName) ? entity.CrewMember?.User?.Email : string.Concat(entity.CrewMember?.User?.LastName, " ", entity.CrewMember?.User?.FirstName, " ", entity.CrewMember?.User?.MiddleName),
                BreakDuration = entity.BreakDuration,
                BreakTimeUnit = entity.BreakTimeUnit,
                From = entity.From,
                Until = entity.Until,
                HourRegistrationType = entity.HourRegistrationType,
                Lunch = entity.Lunch,
                Distance = entity.Distance,
                Remark = entity.Remark,
                TravelDuration = entity.TravelDuration,
                TravelTimeUnit = entity.TravelTimeUnit,
                Activities = entity.Activities != null ? entity.Activities.TransferToListDto() : null
            };

            return timeRegistrationDto;
        }

        /// <summary>
        /// Transfer from TimeRegistrationEntity to TimeRegistrationDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static TimeRegistrationEntity TransferToEntity(this TimeRegistrationDto dto, string crewMemberId=null)
        {
            TimeRegistrationEntity timeRegistrationEntity = new TimeRegistrationEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
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
                CrewMemberId = crewMemberId,
                Activities = dto.Activities != null ? dto.Activities.TransferToListEntity() : null

            };
            return timeRegistrationEntity;
        }

        /// <summary>
        /// Transfer from TimeRegistrationEntity to TimeRegistrationDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<TimeRegistrationEntity> TransferToListEntity(this TimeRegistrationDto dto)
        {
            List<TimeRegistrationEntity> timeRegistrationEntities = dto.CrewMembers.Select(x => dto.TransferToEntity(x.Id)).ToList();

            return timeRegistrationEntities;
        }
    }
}
