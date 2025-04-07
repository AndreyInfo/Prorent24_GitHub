using Prorent24.BLL.Models.Configuration.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.Settings.TimeRegistrationSettings
{
    public interface ITimeRegistrationSettingsService
    {
        Task<TimeRegistrationSettingsDto> GetAsync();
        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<TimeRegistrationSettingsDto> Update(TimeRegistrationSettingsDto model);
    }
}
