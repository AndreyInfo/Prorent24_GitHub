using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Handler;
using Prorent24.BLL.Models.Tasks;
using Prorent24.BLL.Services;
using Prorent24.BLL.Services.Tasks;
using Prorent24.Common.Hubs;
using Prorent24.Common.Models.HelpersModels;
using Prorent24.Enum.General;
using Prorent24.Enum.Notification;
using Prorent24.WebApp.Models.General.Filter;
using Prorent24.WebApp.Models.Tasks;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.General;
using Prorent24.WebApp.Transfers.Modules;
using Prorent24.WebApp.Transfers.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers
{
    /// <summary>
    /// Tasks
    /// </summary>
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IPermissionService _permissionService;
        private readonly IMediator _mediator;
        private readonly INotifier _notifier;

        /// <summary>
        /// Conctructor
        /// </summary>
        /// <param name="taskService"></param>
        /// <param name="permissionService"></param>
        /// <param name="mediator"></param>
        /// <param name="notifier"></param>
        public TaskController(ITaskService taskService, IPermissionService permissionService, IMediator mediator, INotifier notifier)
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _permissionService = permissionService;
            _mediator = mediator;
            _notifier = notifier;
        }

        /// <summary>
        /// Get list Tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery]string filters)
        {
            List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            BaseListDto dto = await _taskService.GetPagedList(resultFilter.TransferToDtoModel());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get Task by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            TaskDto dto = await _taskService.GetById(id);
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
            List<ModuleDetailDto> dtos = await _taskService.GetDetails(id);
            return Ok(dtos?.TransferToModuleDetailViewModel());
        }
        /// <summary>
        /// Create Task
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        /// <param name="referenceId"></param>
        /// <returns></returns>
        [HttpPost("{entity?}/{referenceId?}")]
        public async Task<IActionResult> Create(TaskViewModel model, [FromRoute] ModuleTypeEnum entity = ModuleTypeEnum.Tasks, [FromRoute] string referenceId = null)
        {
            var permission = await _permissionService.GetModulePermission(ModuleTypeEnum.Tasks);

            if (permission.Add)
            {
                try
                {
                    model.BelongsTo = string.IsNullOrEmpty(referenceId) ? ModuleTypeEnum.General : entity;
                    model.ReferenceId = referenceId;

                    TaskDto dto = await _taskService.Create(model.TransferToDto());

                    List<string> userIds = model.Executors?.Count > 0 ? model.Executors.Select(x => x.Id).ToList() : new List<string>();
                    //userIds.Add(dto.CreationUserId);
                    if (userIds.Count() > 0)
                    {
                        await _mediator.Publish(new NotificationHandlerModel()
                        {
                            Type = NotificationTypeEnum.TaskCreatedPrivate,
                            EntityId = dto.Id,
                            UserId = dto.CrewMemberId,
                            Theme = "Новая задача",
                            Message = "Поставлена новая задача " + dto.Name,
                            UserIds = userIds.Distinct().ToList()
                        });
                    }
                    return Ok(dto.TransferToViewModel());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.InnerException.Message);
                }
            }
            else
            {
                return Forbid();
            }

        }

        /// <summary>
        /// Update Task
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> Update(TaskViewModel model)
        {
            var permission = await _permissionService.GetModulePermission(ModuleTypeEnum.Tasks);

            if (permission.Edit)
            {
                try
                {
                    TaskDto dto = await _taskService.Update(model.TransferToDto());
                    return Ok(dto.TransferToViewModel());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.InnerException.Message);
                }
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// Delete Task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _taskService.Delete(id);
            return Ok(result);
        }

        /// <summary>
        /// Close Task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpPost("{id}/close")]
        public async Task<IActionResult> CloseTask([FromRoute]int id, [FromBody]DateModel model)
        {
            TaskDto result = await _taskService.CloseTask(id, model.Date);
            return Ok(result.TransferToViewModel());
        }
    }
}