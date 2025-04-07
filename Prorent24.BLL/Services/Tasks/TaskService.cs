using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Prorent24.BLL.Builders;
using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.General.Period;
using Prorent24.BLL.Models.Tasks;
using Prorent24.BLL.Services.General.Tag;
using Prorent24.BLL.Transfers.General;
using Prorent24.BLL.Transfers.Tasks;
using Prorent24.Common.Extentions;
using Prorent24.Common.Hubs;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.Tasks;
using Prorent24.Entity.Tasks;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Tasks
{
    internal class TaskService : BaseService, ITaskService
    {
        private readonly ITaskStorage _taskStorage;
        private readonly ITaskCrewMemberStorage _taskCrewMemberStorage;
        private readonly ITaskExecutorStorage _taskExecutorStorage;
        private readonly IHubContext<HubConnector> _hubContext;
        private readonly ITagService _tagService;

        public TaskService(IHttpContextAccessor httpContextAccessor, IUserRoleStorage userRoleStorage,
            ITaskStorage taskStorage, ITaskCrewMemberStorage taskCrewMemberStorage,
            ITaskExecutorStorage taskExecutorStorage, IHubContext<HubConnector> hubContext,
            IColumnStorage сolumnStorage, ITagService tagService) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _taskStorage = taskStorage;
            _taskCrewMemberStorage = taskCrewMemberStorage;
            _taskExecutorStorage = taskExecutorStorage;
            _hubContext = hubContext;
            _tagService = tagService;
        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            string searchText;
            string userId = base.GetUserId();
            IQueryable<TaskEntity> queryableEntity = _taskStorage.QueryableEntity.CreateFilterForTaskEntity(filterList, out searchText);
            IPagedList<TaskEntity> query = await _taskStorage.GetAllByFilter(queryableEntity, userId, searchText);
            
            BaseGrid grid = query.Items.ToList().TransferToListDto().CreateGrid<TaskDto>(await GetUserColumns(EntityEnum.TaskEntity));

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.TaskEntity
            };
        }

        public async Task<TaskDto> GetById(int id)
        {
            TaskEntity entity = await _taskStorage.GetById(id);
            TaskDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<List<ModuleDetailDto>> GetDetails(int id)
        {
            TaskEntity entity = await _taskStorage.GetDetails(id);
            TaskDto dto = entity?.TransferToDto();
            List<ModuleDetailDto> moduleDetails = dto?.CreateDetails<TaskDto>(EntityEnum.TaskEntity);

            return moduleDetails;
        }

        public async Task<TaskDto> Create(TaskDto model)
        {
            TaskEntity transferEntity = model.TransferToEntity();
            transferEntity.CrewMembers = model.CrewMembers.TransferToEntity();
            transferEntity.Executors = model.Executors.TransferToExecutorEntity();
            TaskEntity entity = await _taskStorage.Create(transferEntity);
            TaskDto dto = await GetById(entity.Id);
            return dto;
        }

        public async Task<TaskDto> Update(TaskDto model)
        {
            TaskEntity transferEntity = model.TransferToEntity();
            //transferEntity.CompleatedByMember = null;
            //transferEntity.CompletedDate = null;
            //transferEntity.CompleatedBy = null;
            TaskEntity entity = await _taskStorage.Update(transferEntity);
            TaskDto dto = entity.TransferToDto();

            if (model.CrewMembers != null)
            {
                var isDeleted = await this._taskCrewMemberStorage.DeleteAllByTaskId(dto.Id);

                List<TaskVisibilityCrewMemberEntity> visibilityTransferEntities = model.CrewMembers.TransferToEntity();
                List<CrewMemberShortDto> crewMembersList = new List<CrewMemberShortDto>();

                foreach (var _entity in visibilityTransferEntities)
                {
                    _entity.TaskId = entity.Id;
                    TaskVisibilityCrewMemberEntity visibilityEntity = await this._taskCrewMemberStorage.Create(_entity);
                    var transferedDto = visibilityEntity.TransferToDto();
                    crewMembersList.Add(transferedDto);
                }

                dto.CrewMembers = crewMembersList;
            }

            if (model.Executors != null)
            {
                var isDeleted = await _taskExecutorStorage.DeleteAllByTaskId(dto.Id);

                List<TaskExecutorCrewMemberEntity> executorTransferEntities = model.Executors.TransferToExecutorEntity();
                List<CrewMemberShortDto> crewMembersList = new List<CrewMemberShortDto>();

                foreach (var _entity in executorTransferEntities)
                {
                    _entity.TaskId = entity.Id;
                    TaskExecutorCrewMemberEntity executorEntity = await _taskExecutorStorage.Create(_entity);
                    var transferedDto = executorEntity.TransferToExecutorDto();
                    crewMembersList.Add(transferedDto);
                }

                dto.Executors = crewMembersList;
            }

            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            await _tagService.Delete(id);
            bool result = await _taskStorage.Delete(id);
            return result;
        }

        public async Task<TaskDto> CloseTask(int id, DateTime? date)
        {
            TaskEntity entity = await _taskStorage.GetById(id);
            entity.CompletedDate = date.HasValue ? date : DateTime.UtcNow;
            entity.CompleatedBy = GetUserId();
            await _taskStorage.Update(entity);
            return entity.TransferToDto();
        }
    }
}
