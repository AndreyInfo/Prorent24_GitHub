using Microsoft.EntityFrameworkCore;
using Prorent24.BLL.Models.WorkPanel;
using Prorent24.Common.Models.WorkPanel;
using Prorent24.Entity.CrewPlanner;
using Prorent24.Entity.Project;
using Prorent24.Enum.Project;
using Prorent24.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.WorkPanelStorage
{
    internal class WorkPanelStorage : IWorkPanelStorage
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkPanelStorage(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<WorkPanelModel> GetWorkPanelAggregateData(string userId)
        {
            throw new NotImplementedException();
        }

        //public async Task<WorkPanelModel> GetWorkPanelAggregateData(string userId)
        //{
        //    IRepository<ProjectEntity> projectRepos = _unitOfWork.GetRepository<ProjectEntity>();
        //    IRepository<ProjectPlanningEntity> projectPlanningRepos = _unitOfWork.GetRepository<ProjectPlanningEntity>();
        //    // projectRepos.

        //    return await Task.Run<WorkPanelModel>(() =>
        //     {
        //         WorkPanelModel workPanelModel = new WorkPanelModel()
        //         {
        //             Today = new WorkPanelToday()
        //             {
        //                 BlockData = new WorkPanelBlockShortModel()
        //                 {
        //                     Timeline = GetTimLineList(projectRepos, userId),
        //                     Equipment = new WorkPanelEquipmentModel(),
        //                     CrewTransport = GetCrewTransport(projectPlanningRepos, userId)
        //                 }
        //             },
        //             BlockData = new WorkPanelBlockModel()
        //             {
        //                 Projects = GetProjects(projectRepos, projectPlanningRepos, userId)
        //             }
        //         };

        //         return workPanelModel;

        //     });
        //}

        #region PRIVATE


        //private IEnumerable<WorkPanelTimeLineModel> GetTimLineList(IRepository<ProjectEntity> projectRepos, string userId)
        //{
        //    IEnumerable<WorkPanelTimeLineModel> timeLineModels = projectRepos.TableNoTracking
        //        .Include(x => x.LocationContact)
        //        .ThenInclude(x => x.VisitingAddress)
        //        .Where(x => x.AccountManagerId == userId && x.Times != null && x.Times.Any(t => t.DefaultPlanPeriod && t.From.Date == DateTime.UtcNow.Date))
        //        .Select(x => new WorkPanelTimeLineModel()
        //        {
        //            Title = $"{x.Number} - {x.Name}",
        //            Description = x.LocationContact.VisitingAddress.Address,
        //            Time = x.Times.FirstOrDefault(t => t.DefaultPlanPeriod).From.ToShortTimeString()
        //        });

        //    return timeLineModels;
        //}

        //private WorkPanelCrewTransportModel GetCrewTransport(IRepository<ProjectPlanningEntity> projectPlanningRepos, string userId)
        //{
        //    IQueryable<ProjectPlanningEntity> projectPlanningRes = projectPlanningRepos.TableNoTracking
        //        .Include(x => x.Function)
        //        .ThenInclude(x => x.Project)
        //        .Where(x => x.Function.Project.AccountManagerId == userId && x.Function.Project.Times != null && x.Function.Project.Times.Any(t => t.DefaultPlanPeriod && t.From.Date == DateTime.UtcNow.Date));

        //    WorkPanelCrewTransportModel crewTransport = new WorkPanelCrewTransportModel()
        //    {
        //        PlannedCrew = projectPlanningRes.Count(x => !string.IsNullOrWhiteSpace(x.CrewMemberId)),
        //        PlannedTransport = projectPlanningRes.Count(x => x.VehicleId.HasValue),
        //        ProjectsWithPlanning = projectPlanningRes.Count()
        //    };

        //    return crewTransport;
        //}

        //private WorkPanelProjectModel GetProjects(IRepository<ProjectEntity> projectRepos, IRepository<ProjectPlanningEntity> projectPlanningRepos, string userId)
        //{
        //    IQueryable<ProjectEntity> projectRes = projectRepos.TableNoTracking
        //                                                        .Include(x => x.LocationContact)
        //                                                        .ThenInclude(x => x.VisitingAddress)
        //                                                        .Include(x=>x.Tasks)
        //                                                        .Where(x => x.AccountManagerId == userId && x.Times != null && x.Times.Any(t => t.DefaultPlanPeriod && t.From.Date == DateTime.UtcNow.Date));

        //    IQueryable<ProjectEntity> projectsInOptions = projectRes.Where(x => x.Status == ProjectStatusEnum.Option);
        //    IQueryable<ProjectEntity> projectsConfirmed = projectRes.Where(x => x.Status == ProjectStatusEnum.Confirmed);

        //    IQueryable<ProjectPlanningEntity> projectPlanningRes = projectPlanningRepos.TableNoTracking
        //      .Include(x => x.Function)
        //      .ThenInclude(x => x.Project)
        //      .Where(x => x.Function.Project.AccountManagerId == userId && x.Function.Project.Times != null && x.Function.Project.Times.Any(t => t.DefaultPlanPeriod && t.From.Date == DateTime.UtcNow.Date));

        //    IQueryable<ProjectPlanningEntity> projectsCanceled = projectPlanningRes.Where(x => x.Function.Project.Status == ProjectStatusEnum.Cancelled);

        //    WorkPanelProjectModel workPanelProject = new WorkPanelProjectModel()
        //    {
        //        ProjectsOptions = projectsInOptions.Count(),
        //        OpenTasks = new WorkPanelOpenModel()
        //        {
        //            Expired = projectsInOptions.Count(x =>x.Tasks.Any(t => !t.CompletedDate.HasValue)),
        //            Open = projectsConfirmed.Count(x => x.Tasks.Any(t => !t.CompletedDate.HasValue)),
        //        },
        //        ProjectsInvoice = 0,
        //        ProjectRequests = 0,
        //        CancelledWithCrew = projectsCanceled.Count(x=> !string.IsNullOrWhiteSpace(x.CrewMemberId)),
        //        CancelledWithTransport = projectsCanceled.Count(x => x.VehicleId.HasValue)
        //    };

        //    return workPanelProject;
        //}

        #endregion
    }
}
