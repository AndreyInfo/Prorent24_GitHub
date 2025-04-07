using Prorent24.BLL.Models.TimeRegistration;
using Prorent24.Entity.TimeRegistration;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.TimeRegistration
{
    public static class TimeRegistrationActivityActivityTransfer
    {
        /// <summary>
        /// Transfer from List<TimeRegistrationActivityDto> to List<TimeRegistrationActivityEntity>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<TimeRegistrationActivityEntity> TransferToListEntity(this List<TimeRegistrationActivityDto> dtos)
        {
            List<TimeRegistrationActivityEntity> timeRegistrationViewModels = dtos.Select(x => x.TransferToViewModel()).ToList();

            return timeRegistrationViewModels;
        }

        /// <summary>
        /// Transfer from List<TimeRegistrationActivityEntity> to List<TimeRegistrationActivityDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<TimeRegistrationActivityDto> TransferToListDto(this List<TimeRegistrationActivityEntity> entities)
        {
            List<TimeRegistrationActivityDto> timeRegitrationDtos = entities.Select(x => x.TransferToDto()).ToList();

            return timeRegitrationDtos;
        }

        /// <summary>
        /// Transfer from TimeRegistrationActivityDto to TimeRegistrationActivityEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static TimeRegistrationActivityEntity TransferToViewModel(this TimeRegistrationActivityDto dto)
        {
            TimeRegistrationActivityEntity timeRegistrationViewModel = new TimeRegistrationActivityEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                Duration = dto.Duration
            };

            return timeRegistrationViewModel;
        }

        /// <summary>
        /// Transfer from TimeRegistrationActivityEntity to TimeRegistrationActivityDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static TimeRegistrationActivityDto TransferToDto(this TimeRegistrationActivityEntity model)
        {
            TimeRegistrationActivityDto timeRegistrationDto = new TimeRegistrationActivityDto()
            {
                Id = model.Id,
                Name = model.Name,
                Duration = model.Duration
            };
            return timeRegistrationDto;
        }
    }
}
