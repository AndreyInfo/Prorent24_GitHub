using Prorent24.BLL.Models.TimeRegistration;
using Prorent24.WebApp.Models.TimeRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.TimeRegistrationActivity
{
    public static class TimeRegistrationActivityActivityTransfer
    {
        /// <summary>
        /// Transfer from List<TimeRegistrationActivityDto> to List<TimeRegistrationActivityViewModel>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<TimeRegistrationActivityViewModel> TransferToListViewModels(this List<TimeRegistrationActivityDto> entities)
        {
            List<TimeRegistrationActivityViewModel> timeRegistrationViewModels = entities.Select(x => x.TransferToViewModel()).ToList();

            return timeRegistrationViewModels;
        }

        /// <summary>
        /// Transfer from List<TimeRegistrationActivityViewModel> to List<TimeRegistrationActivityDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<TimeRegistrationActivityDto> TransferToListDto(this List<TimeRegistrationActivityViewModel> models)
        {
            List<TimeRegistrationActivityDto> timeRegitrationDtos = models.Select(x => x.TransferToDto()).ToList();

            return timeRegitrationDtos;
        }

        /// <summary>
        /// Transfer from TimeRegistrationActivityDto to TimeRegistrationActivityViewModel
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static TimeRegistrationActivityViewModel TransferToViewModel(this TimeRegistrationActivityDto dto)
        {
            TimeRegistrationActivityViewModel timeRegistrationViewModel = new TimeRegistrationActivityViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                Duration = dto.Duration
            };

            return timeRegistrationViewModel;
        }

        /// <summary>
        /// Transfer from TimeRegistrationActivityViewModel to TimeRegistrationActivityDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static TimeRegistrationActivityDto TransferToDto(this TimeRegistrationActivityViewModel model)
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
