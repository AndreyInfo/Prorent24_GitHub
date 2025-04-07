using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.WebApp.Models.Configuration.Settings;
using Swashbuckle.AspNetCore.Annotations;
using Prorent24.WebApp.Transfers.Configurations.Settings;
using Prorent24.BLL.Services.Configuration.Settings.TimeRegistrationSettings;

namespace Prorent24.WebApp.Controllers.Configurations.Settings
{
    [Route("api/configuration/settings/time_registration")]
    [ApiController]
    [SwaggerTag("Configuration -> Settings")]
    public class TimeRegistrationSettingsController : ControllerBase
    {
        private readonly ITimeRegistrationSettingsService _timeRegistrationSettingsService;

        public TimeRegistrationSettingsController(ITimeRegistrationSettingsService timeRegistrationSettingsService)
        {
            _timeRegistrationSettingsService = timeRegistrationSettingsService;
        }

        /// <summary>
        /// Get Time Registration Data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTimeRegistrationInfo()
        {
            TimeRegistrationSettingsDto dto = await _timeRegistrationSettingsService.GetAsync();
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update Time registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateTimeRegistrationInfo([FromBody]TimeRegistrationSettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                TimeRegistrationSettingsDto dto = await _timeRegistrationSettingsService.Update(model.TransferToDto());
                return Ok(dto.TransferToViewModel());
            }

            var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
            return BadRequest(errors);
        }
    }
}