using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Services.Schedule;

namespace Prorent24.WebApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleService"></param>
        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        /// <summary>
        /// Get list entities for schedules
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetSchedules(string userId)
        {
            return Ok(await _scheduleService.GetSchedules(userId));
        }
    }
}