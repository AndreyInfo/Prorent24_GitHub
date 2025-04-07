using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Handler;
using Prorent24.BLL.Models.Project;
using Prorent24.WebApp.Models.Project;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Modules;
using Prorent24.WebApp.Transfers.Project;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.Project
{
    public partial class ProjectController
    {

        #region Project Equipment Groups

        /// <summary>
        /// Get ProjectEquipment Details
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectId}/equipments/{id}/details")]
        public async Task<IActionResult> GetProjectEquipmentDetails([FromRoute]int projectId, [FromRoute]int id)
        {
            List<ModuleDetailDto> dtos = await _projectEquipmentService.GetDetails(id);
            return Ok(dtos?.TransferToModuleDetailViewModel());
        }

        /// <summary>
        /// Create ProjectEquipmentGroup
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/groups")]
        public async Task<IActionResult> CreateProjectEquipmentGroup([FromRoute]int projectId, [FromBody]ProjectEquipmentGroupViewModel model)
        {
            try
            {
                ProjectEquipmentGroupDto dto = model.TransferToDto();
                dto.ProjectId = projectId;
                ProjectEquipmentGridDto result = await _projectEquipmentService.CreateGroup(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Update ProjectEquipmentGroup
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/groups/{id}")]
        public async Task<IActionResult> UpdateProjectEquipmentGroup([FromRoute]int projectId, [FromRoute]int id, ProjectEquipmentGroupViewModel model)
        {
            try
            {
                ProjectEquipmentGroupDto dto = model.TransferToDto();
                dto.Id = id;
                dto.ProjectId = projectId;
                ProjectEquipmentGridDto result = await _projectEquipmentService.UpdateGroup(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Get ProjectEquipmentGroups by projectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}/groups")]
        public async Task<IActionResult> GetEquipmentGroupByProject([FromRoute]int projectId)
        {
            //List<ProjectEquipmentGroupDto> dtos = await _projectEquipmentService.GetAllGroupByProject(projectId);
            //return Ok(dtos?.TransferToListViewModel());
            var dtos = await _projectEquipmentService.GetAllGroupByProject(projectId);
            return Ok(dtos?.TransferToListViewModel());
        }



        /// <summary>
        /// Delete ProjectEquipmentGroups
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/groups/{id}/delete")]
        public async Task<IActionResult> DeleteProjectEquipmentGroup([FromRoute]int projectId, [FromRoute]int id)
        {
            bool result = await _projectEquipmentService.DeleteGroup(id);
            if (result == true)
            {
                await _projectFinancialService.SaveSaleRentalSubCategoriesByProject(projectId, id, isDeleteGroup: true);
            }
            return Ok(result);
        }
        #endregion


        #region ProjectEquipment
        /// <summary>
        /// Get ProjectEquipments
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}/equipments")]
        public async Task<IActionResult> GetProjectEquipments([FromRoute]int projectId)
        {
            try
            {
                BaseListDto result = await _projectEquipmentService.GetBaseGridAllEquipmentsByProjectId(projectId);
                return Ok(result?.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Create ProjectEquipment
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/equipments")]
        public async Task<IActionResult> CreateProjectEquipment([FromRoute]int projectId, [FromBody]ProjectEquipmentViewModel model)
        {
            try
            {
                ProjectEquipmentDto dto = model.TransferToDto();
                dto.ProjectId = projectId;
                //dto.GroupId = groupId;
                ProjectEquipmentGridDto result = await _projectEquipmentService.CreateEquipment(dto);
                await _projectFinancialService.SaveSaleRentalSubCategoriesByProject(projectId, result.GroupId);

                //Add to rezerv equipment
                await _mediator.Publish(new EquipmentReservedHandlerModel()
                {
                    ProjectEquipmentId = result.Id,
                    From = result.Group.StartPlanPeriod.Value,
                    Until = result.Group.EndPlanPeriod.Value,
                    Quantity = result.Quantity??0
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Update ProjectEquipment
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/equipments/{id}")]
        public async Task<IActionResult> UpdateProjectEquipment([FromRoute]int projectId, [FromRoute]int id, ProjectEquipmentViewModel model)
        {
            try
            {
                ProjectEquipmentDto dto = model.TransferToDto();
                dto.Id = id;
                dto.ProjectId = projectId;
                //dto.GroupId = groupId;
                ProjectEquipmentGridDto result = await _projectEquipmentService.UpdateEquipment(dto);
                await _projectFinancialService.SaveSaleRentalSubCategoriesByProject(projectId, result.GroupId);

                //Add to rezerv equipment
                await _mediator.Publish(new EquipmentReservedHandlerModel()
                {
                    ProjectEquipmentId = result.Id,
                    From = result.Group.StartPlanPeriod.Value,
                    Until = result.Group.EndPlanPeriod.Value,
                    Quantity = result.Quantity??0
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Delete ProjectEquipment
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/equipments/{id}/delete")]
        public async Task<IActionResult> DeleteProjectEquipment([FromRoute]int projectId, [FromRoute]int id)
        {
            ProjectEquipmentDto dto = await _projectEquipmentService.GetById(id);
            bool result = await _projectEquipmentService.DeleteEquipment(id);
            if(result == true)
            {
                await _projectFinancialService.SaveSaleRentalSubCategoriesByProject(projectId, dto.GroupId);
            }
            return Ok(result);
        }
        #endregion

        #region ProjectWarhouseEquipment


        /// <summary>
        /// Get ProjectWarhouseEquipmentGroups by projectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}/warhouse/groups")]
        public async Task<IActionResult> GetWarhouseEquipmentGroupByProject([FromRoute]int projectId)
        {
            var dtos = await _projectEquipmentService.GetAllGroupByProject(projectId);
            return Ok(dtos?.TransferToListViewModel());
        }

        [HttpGet("{projectId}/shortages")]
        public async Task<IActionResult> GetShortages([FromRoute]int projectId)
        {
            var dtos = await _projectEquipmentService.GetSublist(projectId);
            return Ok(dtos);
        }


        #endregion
    }
}
