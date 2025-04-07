using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Vehicle;
using Prorent24.BLL.Services.CrewPlanner;
using Prorent24.BLL.Services.Vehicle;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Warehouse;
using Prorent24.Enum.Project;
using Prorent24.WebApp.Models.General.Filter;
using Prorent24.WebApp.Models.General.Period;
using Prorent24.WebApp.Models.Vehicle;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.General;
using Prorent24.WebApp.Transfers.Modules;
using Prorent24.WebApp.Transfers.Vehicle;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers
{
    /// <summary>
    /// Vehicles
    /// </summary>
    [Route("api/vehicles")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly ICrewPlannerService _crewPlannerService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="vehicleService"></param>
        /// <param name="crewPlannerService"></param>
        public VehiclesController(IVehicleService vehicleService, ICrewPlannerService crewPlannerService)
        {
            _vehicleService = vehicleService;
            _crewPlannerService = crewPlannerService;
        }

        /// <summary>
        /// Get list Vehicles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery]string filters)
        {
            List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            BaseListDto dto = await _vehicleService.GetPagedList(resultFilter.TransferToDtoModel());
            return Ok(dto.TransferToViewModel());
        }


        /// <summary>
        /// Get list Vehicles for project planning
        /// </summary>
        /// <returns></returns>
        [HttpGet("groups")]
        public async Task<IActionResult> GetVehiclesForPlanning()
        {
            BaseListDto dto = await _vehicleService.GetForPlanning();
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get Vehicle by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            VehicleDto dto = await _vehicleService.GetById(id);
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
            List<ModuleDetailDto> dtos = await _vehicleService.GetDetails(id);
            return Ok(dtos?.TransferToModuleDetailViewModel());
        }

        /// <summary>
        /// Create Vehicle
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(VehicleViewModel model)
        {
            VehicleDto dto = await _vehicleService.Create(model.TransferToDto());
            return Ok(dto?.TransferToViewModel());
        }

        /// <summary>
        /// Update Vehicle
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> Update(VehicleViewModel model)
        {
            VehicleDto dto = await _vehicleService.Update(model.TransferToDto());
            return Ok(dto?.TransferToViewModel());
        }

        /// <summary>
        /// Delete Vehicle
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _vehicleService.Delete(id);
            return Ok(result);
        }

        /// <summary>
        /// Get TimeLine
        /// </summary>
        /// <returns></returns>
        [HttpGet("timeLine")]
        public async Task<IActionResult> GetWarhouseEventResourseModel([FromQuery]string period, [FromQuery]List<string> ids)
        {
            ShortPeriodViewModel resultPeriod = !string.IsNullOrWhiteSpace(period) ? JsonConvert.DeserializeObject<ShortPeriodViewModel>(period) : new ShortPeriodViewModel();
            WarhouseEventResourseModel result = await _crewPlannerService.GetAllForModuleTimeLine(ProjectFunctionTypeEnum.Transport, ids, resultPeriod.FromDate, resultPeriod.ToDate);
            return Ok(result);
        }


        /// <summary>
        /// Export Filtered Equipments
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost("export")]
        public async Task<FileStream> ExportVehicles([FromForm] IFormCollection form)
        {
            List<SelectedFilter> filters = new List<SelectedFilter>();
            if (form.TryGetValue("filters", out StringValues formFilters))
            {
                string _filters = formFilters.ToString();
                filters = (JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(_filters)).TransferToDtoModel();
            }

            string[] columns = new string[0];
            if (form.TryGetValue("columns", out StringValues formColumns))
            {
                string _columns = formColumns.ToString();
                columns = JsonConvert.DeserializeObject<string[]>(_columns);
            }

            string path = await _vehicleService.Export(columns, filters);

            return new FileStream(path, FileMode.Open, FileAccess.Read);
        }
    }
}