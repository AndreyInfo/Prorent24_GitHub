using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Builders;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Services.Project;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Schedulers;
using Prorent24.DAL.Models.Project;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.CrewPlanner;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.Project;
using Prorent24.Entity.CrewPlanner;
using Prorent24.Entity.Project;
using Prorent24.Enum.Project;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Schedule
{
    internal class ScheduleService : BaseService, IScheduleService
    {
        private readonly IProjectService _projectService;
        private readonly ICrewPlannerStorage _crewPlannerStorage;
        private readonly IProjectPlanningStorage _projectPlanningStorage;

        public ScheduleService(IProjectService projectService,
            IHttpContextAccessor httpContextAccessor,
            IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage, ICrewPlannerStorage crewPlannerStorage, IProjectPlanningStorage projectPlanningStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _projectService = projectService;
            _crewPlannerStorage = crewPlannerStorage;
            _projectPlanningStorage = projectPlanningStorage;
        }

        public async Task<List<SchedulerModel>> GetSchedules(string userId)
        {
            try
            {
                userId = userId ?? GetUserId();
                List<ProjectDto> projList = await _projectService.GeList();
                List<ProjectPlanningEntity> listPlanningEntitiesForCrew = await _projectPlanningStorage.GetAll();
                List<int?> projects = listPlanningEntitiesForCrew.Where(x => x.CrewMemberId == userId).Select(x => x.Function?.ProjectId).ToList();
                List<SchedulerModel> schedulers = projList.Where(x => x.StartPlanPeriod.HasValue && x.EndPlanPeriod.HasValue
                && (projects.Any(c => c == x.Id) || x.AccountManagerId == userId)).Select(x => new SchedulerModel()
                {
                    Id = x.Id.ToString(),
                    Subject = x.Name,
                    Description = x.Name,
                    Start = x.StartPlanPeriod.Value,
                    End = x.EndPlanPeriod.Value,
                    Background = x.Color
                }).ToList();


                IQueryable<CrewPlannerEntity> queryableEntity = _crewPlannerStorage.QueryableEntity.CreateFilterForGeneralTimeLineCrewPlanner(new List<SelectedFilter>());
                List<CrewPlannerEntity> entities = await _crewPlannerStorage.GetAllForTimeLine(queryableEntity, ProjectFunctionTypeEnum.Crew, new List<string>() { userId });
                schedulers.AddRange(entities.Select(x => new SchedulerModel()
                {
                    Id = x.Id.ToString(),
                    Subject = Char.ToLowerInvariant(x.Action.ToString()[0]) + x.Action.ToString().Substring(1),
                    Start = x.From,
                    End = x.Until,
                    IsAvailable = true,
                    Comment = x.Comment
                }).ToList());

                return schedulers;
            }
            catch (Exception ex)
            {
                throw new Exception("errors.module_loading_error_has_occurred");
            }
        }
    }
}
