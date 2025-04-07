using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Handler;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Services;
using Prorent24.BLL.Services.Equipment.SerialNumber;
using Prorent24.BLL.Services.General.Document;
using Prorent24.BLL.Services.Project;
using Prorent24.BLL.Services.Project.AdditionalCost;
using Prorent24.BLL.Services.Project.Equipment;
using Prorent24.BLL.Services.Project.Financial;
using Prorent24.BLL.Services.Project.Function;
using Prorent24.BLL.Services.Project.Movement;
using Prorent24.BLL.Services.Project.Planning;
using Prorent24.Enum.General;
using Prorent24.Enum.Project;
using Prorent24.WebApp.Models.General.Filter;
using Prorent24.WebApp.Models.Project;
using Prorent24.WebApp.Models.Project.ProjectEquipmentMovement;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.General;
using Prorent24.WebApp.Transfers.Modules;
using Prorent24.WebApp.Transfers.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.Project
{
    [Route("api/projects")]
    [ApiController]
    public partial class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IProjectEquipmentService _projectEquipmentService;
        private readonly IProjectFunctionService _projectFunctionService;
        private readonly IProjectAdditionalCostService _projectAdditionalCostService;
        private readonly IProjectPlanningService _projectPlanningService;
        private readonly IProjectFinancialService _projectFinancialService;
        private readonly IDocumentService _documentService;
        private readonly IProjectEquipmentMovementService _projectEquipmentMovementService;
        private readonly IMediator _mediator;
        private readonly IPermissionService _permissionService;
        private readonly IEquipmentSerialNumberService _equipmentSerialNumberService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectService"></param>
        /// <param name="projectEquipmentService"></param>
        /// <param name="projectFunctionService"></param>
        /// <param name="projectAdditionalCostService"></param>
        /// <param name="projectPlanningService"></param>
        /// <param name="projectFinancialService"></param>
        /// <param name="projectEquipmentMovementService"></param>
        /// <param name="documentService"></param>
        /// <param name="mediator"></param>
        public ProjectController(IProjectService projectService,
            IProjectEquipmentService projectEquipmentService,
            IProjectFunctionService projectFunctionService,
            IProjectAdditionalCostService projectAdditionalCostService,
            IProjectPlanningService projectPlanningService,
            IProjectFinancialService projectFinancialService,
            IProjectEquipmentMovementService projectEquipmentMovementService,
            IDocumentService documentService,
            IMediator mediator,
            IPermissionService permissionService,
            IEquipmentSerialNumberService equipmentSerialNumberService)
        {
            _projectService = projectService;
            _projectEquipmentService = projectEquipmentService;
            _projectFunctionService = projectFunctionService;
            _projectAdditionalCostService = projectAdditionalCostService;
            _projectPlanningService = projectPlanningService;
            _projectFinancialService = projectFinancialService;
            _projectEquipmentMovementService = projectEquipmentMovementService;
            _documentService = documentService;
            _mediator = mediator;
            _permissionService = permissionService;
            _equipmentSerialNumberService = equipmentSerialNumberService;
        }


        #region Project

        /// <summary>
        /// Get list Projects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProjects([FromQuery]string filters)
        {
            List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ?
                JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            BaseListDto dto = await _projectService.GetPagedList(resultFilter.TransferToDtoModel());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get list Projects for warhouse
        /// </summary>
        /// <returns></returns>
        [HttpGet("warehouse")]
        public async Task<IActionResult> GetProjectsForWarhouse(DateTime? date)
        {
            List<ProjectDto> dtos = await _projectService.GeList();
            return Ok(dtos.TransferToListView());
        }

        /// <summary>
        /// Get Project Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetProjectDetails(int id)
        {
            List<ModuleDetailDto> dtos = await _projectService.GetDetails(id);
            return Ok(dtos?.TransferToModuleDetailViewModel());
        }

        /// <summary>
        /// Get Project by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            ProjectDto dto = await _projectService.GetById(id);
            return Ok(dto?.TransferToViewModel());
        }

        /// <summary>
        /// Create Project
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectViewModel model)
        {
            var permission = await _permissionService.GetModulePermission(ModuleTypeEnum.Projects);

            if (permission.Add)
            {
                try
                {
                    ProjectDto dto = await _projectService.Create(model.TransferToDto());
                    await _projectFinancialService.CreateBaseCategoriesByProject(dto.Id);
                    ProjectFinancialDto financial = await _projectFinancialService.CreateDefaultFinancial(dto.Id);
                    await _projectEquipmentMovementService.MovementEquipmentsByProjectId(dto.Id, dto.Status);
                    return Ok(dto.TransferToViewModel());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.InnerException.Message);
                }
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }

        }

        /// <summary>
        /// Update Project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectViewModel model)
        {
            var permission = await _permissionService.GetModulePermission(ModuleTypeEnum.Projects);

            if (permission.Edit)
            {
                try
                {
                    ProjectDto dto = model.TransferToDto();
                    dto.Id = id;
                    ProjectDto result = await _projectService.Update(dto);

                    await _projectEquipmentMovementService.MovementEquipmentsByProjectId(id, dto.Status);

                    return Ok(result.TransferToViewModel());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }

        }


        /// <summary>
        /// Delete Project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var permission = await _permissionService.GetModulePermission(ModuleTypeEnum.Projects);

            if (permission.Delete)
            {
                bool result = await _projectService.Delete(id);
                return Ok(result);
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }
        #endregion

        #region ProjectTime
        /// <summary>
        /// Create ProjectTime
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/times")]
        public async Task<IActionResult> CreateProjectTime([FromRoute]int projectId, ProjectTimeViewModel model)
        {
            try
            {
                ProjectTimeDto dto = await _projectService.CreateTime(model.TransferToDto());
                dto.ProjectId = projectId;
                return Ok(dto.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Update ProjectTime
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/times/{id}")]
        public async Task<IActionResult> UpdateProjectTime([FromRoute]int projectId, [FromRoute]int id, ProjectTimeViewModel model)
        {
            try
            {
                ProjectTimeDto dto = model.TransferToDto();
                dto.Id = id;
                dto.ProjectId = projectId;
                ProjectTimeDto result = await _projectService.UpdateTime(dto);
                return Ok(result.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// Delete ProjectTime 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/times/{id}/delete")]
        public async Task<IActionResult> DeleteProjectTime([FromRoute]int projectId, [FromRoute]int id)
        {
            bool result = await _projectService.DeleteTime(id);
            return Ok(result);
        }
        #endregion

        #region Project Equipment Movement

        /// <summary>
        /// Get List EquipmentMovement by projectId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("{id}/warehouse")]
        public async Task<IActionResult> GetListEquipmentMovementByProjectId(int id, [FromQuery] DateTime? date)
        {
            List<ProjectEquipmentMovementDto> dtos = await _projectEquipmentMovementService.GetListEquipmentMovement(id);
            return Ok(dtos.TransfetToListViewModel());
        }

        /// <summary>
        /// Change status projects in warhouse
        /// </summary>
        /// <returns></returns>
        [HttpPost("warehouse")]
        public async Task<IActionResult> ChangeStatus(ProjectWarhouseChangeStatusViewModel model)
        {
            await _mediator.Publish(new ChangeStatusProjectModel()
            {
                ProjectId = model.ProjectId,
                Status = model.Status
            });

            await _projectEquipmentMovementService.MovementEquipmentsByProjectId(model.ProjectId, model.Status);

            return Ok();
        }


        /// <summary>
        /// Move Equipment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="groupId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}/warehouse/movement")]
        public async Task<IActionResult> MoveEquipment(int id, ProjectEquipmentMovementViewModel model)
        {
            try
            {
                ProjectEquipmentMovementDto dto = await _projectEquipmentMovementService.MovementEquipment(id, model.GroupId, model.TransferToDto());
                List<ProjectEquipmentMovementDto> dtos = await _projectEquipmentMovementService.GetListEquipmentMovement(id);
                if (!dtos.Any(x => x.MovementStatus != model.MovementStatus))
                {
                    await _projectService.SetStatus(id, GetProjectStatus(model.MovementStatus));
                }
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Move Equipments
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}/warehouse/movements")]
        public async Task<IActionResult> MoveEquipments(int id, List<ProjectGroupMovementViewModel> model)
        {
            List<ProjectEquipmentMovementViewModel> toMove = model.SelectMany(x => x.Equipments).ToList();
            ProjectEquipmentMovementStatusEnum status = model.FirstOrDefault().MovementStatus;
            if (toMove != null && toMove.Count > 0)
            {
                List<ProjectEquipmentMovementDto> result = await _projectEquipmentMovementService.MovementEquipments(id, toMove.TransferToListDto());
            }
            List<ProjectEquipmentGroupDto> groupDtos = await _projectEquipmentService.GetAllGroupByProject(id);
            List<ProjectEquipmentMovementDto> dtos = await _projectEquipmentMovementService.GetListEquipmentMovement(id);
            var resultView = dtos?.Where(x => x.MovementStatus == status).ToList().TransferToListGroupViewModel(status, groupDtos);

            if (resultView.Any())
            {
                await _projectService.SetStatus(id, GetProjectStatus(resultView.First().MovementStatus));
            }

            return Ok(resultView);

        }

        #endregion

        #region PRIVATE

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private ProjectStatusEnum GetProjectStatus(ProjectEquipmentMovementStatusEnum status)
        {
            switch (status)
            {
                case ProjectEquipmentMovementStatusEnum.Pack:
                    {
                        return ProjectStatusEnum.Confirmed;
                    }
                case ProjectEquipmentMovementStatusEnum.Packed:
                    {
                        return ProjectStatusEnum.Packed;
                    }
                case ProjectEquipmentMovementStatusEnum.Transportation:
                    {
                        return ProjectStatusEnum.OnLocation;
                    }
                case ProjectEquipmentMovementStatusEnum.Dismantling:
                case ProjectEquipmentMovementStatusEnum.InUse:
                case ProjectEquipmentMovementStatusEnum.Mounting:
                case ProjectEquipmentMovementStatusEnum.OrderIsOver:
                    {
                        return ProjectStatusEnum.InUse;
                    }
                case ProjectEquipmentMovementStatusEnum.Returned:
                    {
                        return ProjectStatusEnum.Returned;
                    }
            }

            return ProjectStatusEnum.Confirmed;
        }

        #endregion

    }
}