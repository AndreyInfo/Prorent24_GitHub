using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Project;
using Prorent24.WebApp.Models.Project;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Project;

namespace Prorent24.WebApp.Controllers.Project
{

    public partial class ProjectController
    {
        #region Financial
        /// <summary>
        /// Get ProjectFinancial by Id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}/financial")]
        public async Task<IActionResult> GetProjectFinancialById(int projectId)
        {
            ProjectFinancialDto dto = await _projectFinancialService.GetByProjectId(projectId);
            return Ok(dto?.TransferToViewModel());

        }

        /// <summary>
        /// Get ProjectFinancial by Id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}/financialScheme")]
        public async Task<IActionResult> GetProjectScheme(int projectId)
        {
            VatSchemeRateDto dto = await _projectFinancialService.GetVatScheme(projectId);
            return Ok(dto);

        }

        /// <summary>
        /// Save ProjectFinancial
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/financial")]
        public async Task<IActionResult> SaveProjectFinancial([FromRoute]int projectId, ProjectFinancialViewModel model)
        {
            try
            {
                ProjectFinancialDto dto = model.TransferToDto();
                dto.ProjectId = projectId;
                ProjectFinancialDto result = await _projectFinancialService.Update(dto);
                return Ok(result.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
        #endregion


        #region FinancialCategory
        /// <summary>
        /// Update ProjectFinancialCategory
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/financial_categories/{id}")]
        public async Task<IActionResult> UpdateProjectFinancialCategory(int id, ProjectFinancialCategoryViewModel model)
        {
            try
            {
                ProjectFinancialCategoryDto dto = model.TransferToDto();
                dto.Id = id;
                ProjectFinancialCategoryUpdateDto res = await _projectFinancialService.UpdateCategory(dto);
                ProjectFinancialCategoryUpdateModel result = new ProjectFinancialCategoryUpdateModel()
                {
                    UpdateCategory = res.UpdateCategory.TransferToViewModel(),
                    TotalExcludeVatCategory = res.TotalExcludeVatCategory.TransferToViewModel(),
                    TotalIncludeVat = res.TotalIncludeVat
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Calculate Total ProjectFinancialCategory
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/financial_categories/total")]
        public async Task<IActionResult> CalculateTotalFinancialCategory([FromRoute]int projectId)
        {
            try
            {
                ProjectFinancialCategoryDto result = await _projectFinancialService.CalculateTotalFinancialCategory(projectId);
                return Ok(result.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Get ProjectFinancialCategories by projectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}/financial_categories")]
        public async Task<IActionResult> GetFinancialCategoriesByProject([FromRoute]int projectId)
        {
            List<ProjectFinancialCategoryDto> dtos = await _projectFinancialService.GetCategoriesByProject(projectId);
            return Ok(dtos.TransferToListViewModel());
        }


        /// <summary>
        /// Get ProjectFinancialCategories by projectId include children
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}/financial_categories_with_children")]
        public async Task<IActionResult> GetFinancialCategoriesIncludeChildrenByProject([FromRoute]int projectId)
        {
            List<ProjectFinancialCategoryDto> dtos = await _projectFinancialService.GetCategoriesIncludeChildrenByProject(projectId);
            return Ok(dtos.TransferToListViewModel());
        }


        #endregion
    }

}