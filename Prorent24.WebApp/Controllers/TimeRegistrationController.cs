using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.TimeRegistration;
using Prorent24.BLL.Services.TimeRegistration;
using Prorent24.WebApp.Models.General.Filter;
using Prorent24.WebApp.Models.TimeRegistration;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.General;
using Prorent24.WebApp.Transfers.Modules;
using Prorent24.WebApp.Transfers.TimeRegistration;

namespace Prorent24.WebApp.Controllers
{
    [Route("api/time_registration")]
    [ApiController]
    public class TimeRegistrationController : ControllerBase
    {
        private readonly ITimeRegistrationService _timeRegistrationService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeRegistrationService"></param>
        public TimeRegistrationController(ITimeRegistrationService timeRegistrationService)
        {
            _timeRegistrationService = timeRegistrationService;
        }

        /// <summary>
        /// Get list TimeRegistrations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery]string filters)
        {
            List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            BaseListDto dto = await _timeRegistrationService.GetPagedList(resultFilter.TransferToDtoModel());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get TimeRegistration by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            TimeRegistrationDto dto = await _timeRegistrationService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetDetails(int id)
        {
            List<ModuleDetailDto> dtos = await _timeRegistrationService.GetDetails(id);
            return Ok(dtos?.TransferToModuleDetailViewModel());
        }

        /// <summary>
        /// Create TimeRegistration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(TimeRegistrationViewModel model)
        {
            TimeRegistrationDto dto = await _timeRegistrationService.Create(model.TransferToDto());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update TimeRegistration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> Update(int id, TimeRegistrationViewModel model)
        {
            TimeRegistrationDto dto = await _timeRegistrationService.Update(model.TransferToDto());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Delete TimeRegistration
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _timeRegistrationService.Delete(id);
            return Ok(result);
        }

    }
}