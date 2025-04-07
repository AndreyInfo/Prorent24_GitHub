using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prorent24.BLL.Models.CrewPlanner;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Services.CrewPlanner;
using Prorent24.BLL.Services.Project.Financial;
using Prorent24.BLL.Services.Project.Function;
using Prorent24.BLL.Services.Project.Planning;
using Prorent24.Common.Models.Warehouse;
using Prorent24.Enum.Project;
using Prorent24.WebApp.Models.CrewPlanner;
using Prorent24.WebApp.Models.General.Filter;
using Prorent24.WebApp.Models.Project;
using Prorent24.WebApp.Transfers.CrewPlanner;
using Prorent24.WebApp.Transfers.General;
using Prorent24.WebApp.Transfers.Project;

namespace Prorent24.WebApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/crew_planners")]
    [ApiController]
    public class CrewPlannerController : ControllerBase
    {
        private readonly ICrewPlannerService _crewPlannerService;
        private readonly IProjectPlanningService _projectPlanningService;
        private readonly IProjectFunctionService _projectFunctionService;
        private readonly IProjectFinancialService _projectFinancialService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crewPlannerService"></param>
        /// <param name="projectPlanningService"></param>
        /// <param name="projectFunctionService"></param>
        /// <param name="projectFinancialService"></param>
        public CrewPlannerController(ICrewPlannerService crewPlannerService, IProjectPlanningService projectPlanningService,
            IProjectFunctionService projectFunctionService, IProjectFinancialService projectFinancialService)
        {
            _crewPlannerService = crewPlannerService;
            _projectPlanningService = projectPlanningService;
            _projectFunctionService = projectFunctionService;
            _projectFinancialService = projectFinancialService;
        }


        /// <summary>
        /// Get Scheduler CrewMember
        /// </summary>
        /// <returns></returns>
        [HttpGet("scheduler/crew_members")]
        public async Task<IActionResult> GetSchedulerCrewMember()
        {
            try
            {
                return Ok(await _crewPlannerService.GetShortSchedulerCrewMember());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }


        /// <summary>
        /// Create CrewPlanner
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCrewPlanner(CrewPlannerViewModel model)
        {
            try
            {
                CrewPlannerDto dto = await _crewPlannerService.Create(model.TransferToDto());
                return Ok(dto.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }


        /// <summary>
        /// Update CrewPlanner
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateCrewPlanner(int id, CrewPlannerViewModel model)
        {
            try
            {
                CrewPlannerDto dto = model.TransferToDto();
                dto.Id = id;
                CrewPlannerDto result = await _crewPlannerService.Update(dto);
                return Ok(result.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Get Crewplanner list by type
        /// </summary>
        /// <returns></returns>
        [HttpPost("{type}/byType")]
        public async Task<IActionResult> GetList([FromRoute]ProjectFunctionTypeEnum type, [FromQuery]string filters, [FromBody]List<string> ids)
        {
            List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            WarhouseEventResourseModel result = await _crewPlannerService.GetAll(type, ids, resultFilter.TransferToDtoModel());
            return Ok(result);
        }

        /// <summary>
        /// Get Crewplanner list for Project
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetListProjects([FromQuery]string filters)
        {
            List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            WarhouseEventResourseModel result = await _projectPlanningService.GetForGeneralTimeLine(resultFilter.TransferToDtoModel());
            return Ok(result);
        }

        /// <summary>
        /// Create ProjectPlanning
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/plannings")]
        public async Task<IActionResult> CreateProjectPlanning([FromRoute]int projectId, CrewPlannerProjectPlanningModel model)
        {
            try
            {
                ProjectFunctionDto function = await _projectFunctionService.GetFunctionById((int)model.FunctionId);
                if (function.Type == model.Type)
                {
                    WarhouseEventResourseModel dto = await _projectPlanningService.CreatePlanningFromCrewPlanner(projectId, model.TransferToListDto(), model.Type, model.FunctionId);
                    await _projectFinancialService.CalculateCrewTransporFinancialCategory((int)projectId, function.Type);
                    return Ok(dto);
                }
                else
                {
                    return BadRequest(new Exception("Bad type"));
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }
    }
}