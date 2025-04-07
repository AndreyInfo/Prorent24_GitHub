using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Prorent24.WebApp.Controllers.Configurations.Settings
{
    [Route("api/configuration/settings/number_series")]
    [ApiController]
    [SwaggerTag("Configuration -> Settings")]
    public class NumberSeriesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetNumberSeries()
        {
            return Ok();
        }
    }
}