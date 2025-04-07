using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Project;
using Prorent24.Common.Models.Warehouse;
using Prorent24.Enum.Project;
using Prorent24.WebApp.Models.Project;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Project;
using System;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.Project
{
    public partial class ProjectController : ControllerBase
    {
        /// <summary>
        /// Create ProjectPlanning
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/plannings")]
        public async Task<IActionResult> CreateProjectPlanning([FromRoute]int projectId, ProjectPlanningViewModel model)
        {
            try
            {
                ProjectPlanningGridDto dto = await _projectPlanningService.CreatePlanning(model.TransferToDto());
                if (dto != null)
                {
                    ProjectFunctionDto function = await _projectFunctionService.GetFunctionById((int)dto.FunctionId);
                    await _projectFinancialService.CalculateCrewTransporFinancialCategory((int)projectId, function.Type);
                    return Ok(dto);
                }
                else
                {
                    return BadRequest("Can't insert duplicate");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Update ProjectPlanning
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/plannings/{id}")]
        public async Task<IActionResult> UpdateProjectPlanning([FromRoute]int projectId, int id, ProjectPlanningViewModel model)
        {
            try
            {
                ProjectPlanningDto dto = model.TransferToDto();
                dto.Id = id;
                ProjectPlanningGridDto result = await _projectPlanningService.UpdatePlanning(dto);
                ProjectFunctionDto function = await _projectFunctionService.GetFunctionById((int)result.ParentId);
                await _projectFinancialService.CalculateCrewTransporFinancialCategory((int)projectId, function.Type);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Delete ProjectPlanning
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/plannings/{id}/delete")]
        public async Task<IActionResult> DeleteProjectPlanning([FromRoute] int projectId, [FromRoute] int id)
        {
            bool result = await _projectPlanningService.DeletePlanning(id);

            return Ok(result);
        }


        /// <summary>
        /// Get ProjectPlanning by projectId
        /// </summary>
        /// <param name = "projectId" ></param>
        /// <returns></returns>
        [HttpGet("{projectId}/plannings/{id}")]
        public async Task<IActionResult> GetPlanningById([FromRoute]int projectId, [FromRoute]int id)
        {
            ProjectPlanningDto dto = await _projectPlanningService.GetPlanningById(id);
            return Ok(dto?.TransferToViewModel());
        }

        /// <summary>
        /// Get ProjectPlanning by projectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("{projectId}/plannings")]
        public async Task<IActionResult> GetPlanningGoupedByFunctions([FromRoute]int projectId, [FromQuery]string filters)
        {
            ProjectPlanningFilter resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<ProjectPlanningFilter>(filters) : new ProjectPlanningFilter();
            resultFilter.ProjectId = projectId;
            BaseListDto dto = await _projectPlanningService.GetGridPlanningByGroupedFunctions(resultFilter);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get Warhouse TimeLine Crew Or Transport
        /// </summary>
        /// <returns></returns>
        [HttpGet("plannings/warehouse/{type}")]
        public async Task<IActionResult> GetWarhouseTimeLineCrewOrTransport([FromRoute]ProjectFunctionTypeEnum type,[FromQuery] DateTime? date)
        {
            date = date.HasValue ? date : DateTime.UtcNow;

            WarhouseEventResourseModel result  = await _projectPlanningService.GetWarhouseTimeLineCrewOrTransport(date.Value, type);
            return Ok(result);
        }
        
    }
}