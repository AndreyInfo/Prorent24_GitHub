using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Project;
using Prorent24.WebApp.Models.Project;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Project;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.Project
{
    public partial class ProjectController
    {
        #region ProjectAdditionalCost
        /// <summary>
        /// Create ProjectAdditionalCost
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/additional_costs")]
        public async Task<IActionResult> CreateProjectAdditionalCost([FromRoute]int projectId, ProjectAdditionalCostViewModel model)
        {
            try
            {
                ProjectAdditionalCostDto dto = model.TransferToDto();
                dto.ProjectId = projectId;
                ProjectAdditionalCostDto result = await _projectAdditionalCostService.Create(dto);
                //перерахунок фінансів
                await _projectFinancialService.CalculateAdditionalCostFinancialCategory(projectId);
                return Ok(result.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Update ProjectAdditionalCost
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/additional_costs/{id}")]
        public async Task<IActionResult> UpdateProjectAdditionalCost([FromRoute]int projectId, [FromRoute]int id, ProjectAdditionalCostViewModel model)
        {
            try
            {
                ProjectAdditionalCostDto dto = model.TransferToDto();
                dto.Id = id;
                dto.ProjectId = projectId;
                ProjectAdditionalCostDto result = await _projectAdditionalCostService.Update(dto);
                //перерахунок фінансів
                await _projectFinancialService.CalculateAdditionalCostFinancialCategory(projectId);
                return Ok(result.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Delete ProjectAdditionalCost
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/additional_costs/{id}/delete")]
        public async Task<IActionResult> DeleteProjectAdditionalCost([FromRoute]int projectId, int id)
        {
            bool result = await _projectAdditionalCostService.Delete(id);
            //перерахунок фінансів
            await _projectFinancialService.CalculateAdditionalCostFinancialCategory(projectId);
            return Ok(result);
        }

        /// <summary>
        /// Get ProjectAdditionalCost By Id
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectId}/additional_costs/{id}")]
        public async Task<IActionResult> GetProjectAdditionalCost([FromRoute]int projectId, [FromRoute]int id)
        {
            ProjectAdditionalCostDto result = await _projectAdditionalCostService.GetById(id);
            return Ok(result.TransferToViewModel());
        }

        /// <summary>
        /// Delete ProjectAdditionalCost Multiple
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/additional_costs/delete")]
        public async Task<IActionResult> DeleteProjectAdditionalCostMultiple([FromRoute]int projectId, [FromBody]List<int> ids)
        {
            //bool result = await _projectAdditionalCostService.DeleteMultiple(ids);
            //return Ok(result);
            return Ok();
        }

        /// <summary>
        /// Get ProjectAdditionalCosts list by projectId
        /// </summary>
        /// <returns></returns>
        [HttpGet("{projectId}/additional_costs")]
        public async Task<IActionResult> GetProjectAdditionalCosts(int projectId)
        {
            BaseListDto dto = await _projectAdditionalCostService.GetPagedList(projectId);
            return Ok(dto.TransferToViewModel());
        }
        #endregion
    }
}
