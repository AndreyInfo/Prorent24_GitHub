using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.WebApp.Models.Configuration.Settings;

namespace Prorent24.WebApp.Transfers.Configurations.Settings
{
    public static class TimeRegistrationSettingsTransfer
    {
        /// <summary>
        /// Transfer from  TimeRegistrationSettingsDto to  TimeRegistrationSettingsViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static TimeRegistrationSettingsViewModel TransferToViewModel(this TimeRegistrationSettingsDto dto)
        {
            TimeRegistrationSettingsViewModel viewModel = new TimeRegistrationSettingsViewModel()
            {
                DaysBackNumber = dto.DaysBackNumber,
                DefaultBrake = dto.DefaultBrake,
                FrequencyUnitType = dto.FrequencyUnitType
            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from  TimeRegistrationSettingsViewModel to  TimeRegistrationSettingsDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static TimeRegistrationSettingsDto TransferToDto(this TimeRegistrationSettingsViewModel view)
        {
            TimeRegistrationSettingsDto dto = new TimeRegistrationSettingsDto()
            {
               DaysBackNumber = view.DaysBackNumber,
               DefaultBrake = view.DefaultBrake,
               FrequencyUnitType = view.FrequencyUnitType
            };

            return dto;
        }
    }
}
