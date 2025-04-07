using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.BLL.Services;
using Prorent24.BLL.Services.Configuration.Settings.TimeAndLocation;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.Configuration.Settings;
using Prorent24.WebApp.Transfers.Configurations.Settings;
using Swashbuckle.AspNetCore.Annotations;

namespace Prorent24.WebApp.Controllers.Configurations.Settings
{
    [Route("api/configuration/settings/time_location")]
    [ApiController]
    [SwaggerTag("Configuration -> Settings")]
    public class TimeAndLocation : ControllerBase
    {
        private readonly ITimeAndLocationService _timeAndLocationService;
        private readonly IPermissionService _permissionService;


        public TimeAndLocation(ITimeAndLocationService timeAndLocationService, IPermissionService permissionService)
        {
            this._timeAndLocationService = timeAndLocationService;
            _permissionService = permissionService;
        }
        /// <summary>
        /// Get Time And Location
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTimeAndLocation()
        {
            TimeAndLocationDto dto = await _timeAndLocationService.GetAsync();
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update Time And Location
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateTimeAndLocation([FromBody]TimeAndLocationViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.TimeAndLocation).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                if (ModelState.IsValid)
                {
                    TimeAndLocationDto dto = await _timeAndLocationService.Update(model.TransferToDto());
                    return Ok(dto.TransferToViewModel());
                }
                var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                return BadRequest(errors);
            }
            else
            {
                return Forbid();
            }
        }
    }
}