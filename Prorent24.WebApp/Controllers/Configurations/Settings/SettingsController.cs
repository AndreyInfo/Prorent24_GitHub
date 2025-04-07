using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Services.Configuration.Settings;
using Prorent24.BLL.Services.Configuration.Settings.TimeAndLocation;

namespace Prorent24.WebApp.Controllers.Configurations.Settings
{
    public partial class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;
        private readonly ITimeAndLocationService _timeAndLocationService;

        public SettingsController(ISettingsService settingsService, ITimeAndLocationService timeAndLocationService)
        {
            this._settingsService = settingsService;
            this._timeAndLocationService = timeAndLocationService;
        }
    }
}
