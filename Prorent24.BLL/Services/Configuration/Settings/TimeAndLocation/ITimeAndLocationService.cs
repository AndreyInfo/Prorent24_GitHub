using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Models.Configuration.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.Settings.TimeAndLocation
{
    public interface ITimeAndLocationService
    {
        Task<TimeAndLocationDto> GetAsync();
        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<TimeAndLocationDto> Update(TimeAndLocationDto model);
    }
}
