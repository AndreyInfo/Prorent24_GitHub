using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Project;
using Prorent24.Enum.Project;
using Prorent24.WebApp.Models.Project;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Project;

namespace Prorent24.WebApp.Controllers.Project
{

    public partial class ProjectController
    {
        #region Function Groups
        /// <summary>
        /// Create ProjectFunctionGroup
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/function_groups")]
        public async Task<IActionResult> CreateProjectFunctionGroup([FromRoute]int projectId, [FromBody]ProjectFunctionGroupViewModel model)
        {
            try
            {
                ProjectFunctionGroupDto dto = model.TransferToDto();
                dto.ProjectId = projectId;
                dto.SubprojectId = projectId;
                ProjectFunctionGridDto result = await _projectFunctionService.CreateGroup(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Update ProjectFunctionGroup
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/function_groups/{id}")]
        public async Task<IActionResult> UpdateProjectFunctionGroup([FromRoute]int projectId, [FromRoute]int id, ProjectFunctionGroupViewModel model)
        {
            try
            {
                ProjectFunctionGroupDto dto = model.TransferToDto();
                dto.Id = id;
                dto.ProjectId = projectId;
                ProjectFunctionGridDto result = await _projectFunctionService.UpdateGroup(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Get ProjectFunctionGroups by projectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}/function_groups")]
        public async Task<IActionResult> GetFunctionGroupByProject([FromRoute]int projectId)
        {
            BaseListDto dto = await _projectFunctionService.GetAllGroupByProject(projectId);
            return Ok(dto.TransferToViewModel());
        }


        /// <summary>
        /// Delete ProjectFunctionGroup by Id
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/function_groups/{id}/delete")]
        public async Task<IActionResult> DeleteProjectFunctionGroup([FromRoute]int projectId, [FromRoute]int id)
        {
            bool result = await _projectFunctionService.DeleteGroup(id);
            return Ok(result);
        }

        #endregion

        #region Function

        /// <summary>
        /// Get ProjectFunction by Id
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("functions/{id}")]
        [HttpGet("{projectId}/functions/{id}")]
        public async Task<IActionResult> GetProjectFunctionById([FromRoute]int? projectId, int id)
        {
            ProjectFunctionDto dto = await _projectFunctionService.GetFunctionById(id);
            return Ok(dto?.TransferToViewModel());
        }

        /// <summary>
        /// Create ProjectFunction
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("functions", Name = "Create Function")]
        [HttpPost("{projectId}/functions", Name = "Create Project Function")]
        public async Task<IActionResult> CreateProjectFunction([FromRoute]int? projectId, [FromBody]ProjectFunctionViewModel model)
        {
            try
            {
                ProjectFunctionDto dto = model.TransferToDto();

                if (projectId != null)
                {
                    dto.ProjectId = projectId;
                    VatSchemeRateDto vatScheme = await _projectFinancialService.GetVatScheme((int)projectId);
                    ProjectFunctionGridDto result = await _projectFunctionService.CreateFunctionForProject(dto, vatScheme);
                    await _projectFinancialService.CalculateCrewTransporFinancialCategory((int)projectId, (ProjectFunctionTypeEnum)result.Type);
                    return Ok(result);
                }
                else
                {
                    ProjectFunctionDto result = await _projectFunctionService.CreateFunction(dto);
                    return Ok(result.TransferToViewModel());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Update ProjectFunction
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("functions/{id}", Name = "UpdateFunctions")]
        [HttpPost("{projectId}/functions/{id}", Name = "UpdateProjectFunction")]
        public async Task<IActionResult> UpdateProjectFunction([FromRoute]int? projectId, [FromRoute]  int id, [FromBody] ProjectFunctionViewModel model)
        {
            try
            {
                ProjectFunctionDto dto = model.TransferToDto();
                dto.Id = id;
                if (projectId != null)
                {
                    dto.ProjectId = projectId;
                    VatSchemeRateDto vatScheme = await _projectFinancialService.GetVatScheme((int)projectId);
                    ProjectFunctionGridDto result = await _projectFunctionService.UpdateFunctionForProject(dto, vatScheme);
                    await _projectFinancialService.CalculateCrewTransporFinancialCategory((int)projectId, (ProjectFunctionTypeEnum)result.Type);
                    return Ok(result);
                }
                else
                {
                    ProjectFunctionDto result = await _projectFunctionService.UpdateFunction(dto);
                    return Ok(result.TransferToViewModel());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Delete ProjectFunction
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("functions/{id}/delete", Name = "DeleteFunction")]
        [HttpPost("{projectId}/functions/{id}/delete", Name = "DeleteProjectFunction")]
        public async Task<IActionResult> DeleteProjectFunction([FromRoute]int? projectId, int id)
        {
            //todo первырити чи гет не блокує
            ProjectFunctionDto dto = await _projectFunctionService.GetFunctionById(id);
            bool result = await _projectFunctionService.DeleteFunction(id);
            if (projectId != null)
            {
                await _projectFinancialService.CalculateCrewTransporFinancialCategory((int)projectId, dto.Type);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get ProjectFunctions by projectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("functions", Name = "Get Functions")]
        [HttpGet("{projectId}/functions", Name = "Get Function By Project")]
        public async Task<IActionResult> GetFunctionByProject([FromRoute]int? projectId)
        {
            BaseListDto dto = await _projectFunctionService.GetAllFunctionsByProject(projectId);
            return Ok(dto.TransferToViewModel());
        }
        #endregion
    }
}