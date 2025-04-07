using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Maintenance;
using Prorent24.BLL.Services.Maintenance.Repair;
using Prorent24.WebApp.Models.General.Filter;
using Prorent24.WebApp.Models.Maintenance.Repair;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.General;
using Prorent24.WebApp.Transfers.Maintenance.Repair;
using Prorent24.WebApp.Transfers.Modules;

namespace Prorent24.WebApp.Controllers.Maintenance
{
    [Route("api/repairs")]
    [ApiController]
    public class RepairController : ControllerBase
    {
        private readonly IRepairService _repairService;

        public RepairController(IRepairService repairService)
        {
            _repairService = repairService ?? throw new ArgumentNullException(nameof(repairService));
        }
        #region REPAIRS
        /// <summary>
        /// Get Repairs list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] string filters)
        {
            List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            BaseListDto dto = await _repairService.GetPagedList(resultFilter.TransferToDtoModel());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get Repair by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            RepairDto dto = await _repairService.GetById(id);
            return Ok(dto?.TransferToViewModel());
        }


        /// <summary>
        /// Get Repair Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetRepairDetails([FromRoute] int id)
        {
            List<ModuleDetailDto> dtos = await _repairService.GetDetails(id);
            return Ok(dtos?.TransferToModuleDetailViewModel());
        }


        /// <summary>
        /// Create Repair
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(RepairViewModel model)
        {
            RepairDto dto = await _repairService.Create(model.TransferToDto());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update Repair
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, RepairViewModel model)
        {
            RepairDto dto = model.TransferToDto();
            dto.Id = id;
            RepairDto result = await _repairService.Update(model.TransferToDto());
            return Ok(result.TransferToViewModel());
        }

        /// <summary>
        /// Delete Repair
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _repairService.Delete(id);
            return Ok(result);
        }

        #endregion
    }
}