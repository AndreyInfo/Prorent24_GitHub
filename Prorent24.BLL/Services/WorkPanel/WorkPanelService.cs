using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prorent24.BLL.Models.WorkPanel;
using Prorent24.Common.Models.WorkPanel;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.CrewPlanner;
using Prorent24.DAL.Storages.Equipment;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.Invoice;
using Prorent24.DAL.Storages.Maintenance.Repair;
using Prorent24.DAL.Storages.Project;
using Prorent24.DAL.Storages.WorkPanelStorage;
using Prorent24.Entity.CrewPlanner;
using Prorent24.Entity.Equipment;
using Prorent24.Entity.Invoice;
using Prorent24.Entity.Maintenance;
using Prorent24.Entity.Project;
using Prorent24.Enum.Invoice;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.WorkPanel
{
    internal class WorkPanelService : BaseService, IWorkPanelService
    {
        private readonly ICrewPlannerStorage _crewPlannerStorage;
        private readonly IProjectPlanningStorage _projectPlanningStorage;
        private readonly IProjectStorage _projectStorage;
        private readonly IProjectFinancialStorage _projectFinancialStorage;
        private readonly IRepairStorage _repairStorage;
        private readonly IEquipmentStorage _equipmentStorage;
        private readonly IProjectFunctionStorage _projectFunctionStorage;
        private readonly IInvoiceStorage _invoiceStorage;


        private readonly IWorkPanelStorage _workPanelStorage;

        public WorkPanelService(IWorkPanelStorage workPanelStorage,
            ICrewPlannerStorage crewPlannerStorage,
            IProjectPlanningStorage projectPlanningStorage,
            IProjectStorage projectStorage,
            IProjectFinancialStorage projectFinancialStorage,
            IRepairStorage repairStorage,
            IEquipmentStorage equipmentStorage,
            IProjectFunctionStorage projectFunctionStorage,
            IInvoiceStorage invoiceStorage,
            IHttpContextAccessor httpContextAccessor,
            IUserRoleStorage userRoleStorag,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorag, сolumnStorage)
        {
            _workPanelStorage = workPanelStorage;
            _crewPlannerStorage = crewPlannerStorage;
            _projectPlanningStorage = projectPlanningStorage;
            _projectStorage = projectStorage;
            _projectFinancialStorage = projectFinancialStorage;
            _repairStorage = repairStorage;
            _equipmentStorage = equipmentStorage;
            _projectFunctionStorage = projectFunctionStorage;
            _invoiceStorage = invoiceStorage;
        }

        public async Task<WorkPanelModel> GetWorkPanel()
        {
            WorkPanelModel workPanelModel = new WorkPanelModel()
            {
                Today = new WorkPanelToday()
                {
                    Date = DateTime.Now.ToLongDateString(),
                    BlockData = new WorkPanelBlockShortModel()
                    {
                        Timeline = await GetPanelTimeLineByDate(DateTime.Now),
                        CrewTransport = await GetPanelCrewTransportByDate(DateTime.Now)
                    }
                },
                Tomorrow = new WorkPanelToday()
                {
                    Date = DateTime.Now.AddDays(1).ToLongDateString(),
                    BlockData = new WorkPanelBlockShortModel()
                    {
                        Timeline = await GetPanelTimeLineByDate(DateTime.Now.AddDays(1)),
                        CrewTransport = await GetPanelCrewTransportByDate(DateTime.Now.AddDays(1))
                    }
                },
                BlockData = new WorkPanelBlockModel()
                {
                    Projects = await GetPanelProdjects(),
                    Planning = await GetPanelPlanning(),
                    Todo = await GetPanelTodo()
                }
            };

            return workPanelModel;  // await _workPanelStorage.GetWorkPanelAggregateData(GetUserId());
        }

        private async Task<List<WorkPanelTimeLineModel>> GetPanelTimeLineByDate(DateTime date)
        {
            string userId = this.GetUserId();
            IQueryable<CrewPlannerEntity> crewPlanners = _crewPlannerStorage.QueryableEntity;
            crewPlanners = crewPlanners.Where(x => x.From.Date == date.Date && x.CrewMemberId == userId);
            List<WorkPanelTimeLineModel> workPanelTimeLines = await crewPlanners.Select(x => new WorkPanelTimeLineModel()
            {
                Action = x.Action,
                Title = x.Comment,
                Time = string.Concat(x.From.ToString("HH:mm"), " - ", x.Until.ToString("HH:mm")),
                TimeStart = x.From,
                TimeEnd = x.Until
            }).OrderBy(x=>x.TimeStart).ToListAsync();

            return workPanelTimeLines;
        }

        private async Task<WorkPanelCrewTransportModel> GetPanelCrewTransportByDate(DateTime date)
        {
            IQueryable<ProjectPlanningEntity> projectPlanning = _projectPlanningStorage.QueryableEntity.Include(x => x.Function).ThenInclude(x=>x.Project);
            projectPlanning = projectPlanning.Where(x => x.PlanUntil.HasValue && x.PlanUntil.Value.Date >= date.Date);
            List<ProjectPlanningEntity> result = await projectPlanning.ToListAsync();

            WorkPanelCrewTransportModel workPanelCrewTransport = new WorkPanelCrewTransportModel();

            foreach (ProjectPlanningEntity item in result)
            {
                if (!string.IsNullOrWhiteSpace(item.CrewMemberId))
                {
                    string key = item.Function?.Project?.Id.ToString();
                    if (!String.IsNullOrWhiteSpace(key) && !workPanelCrewTransport.PlannedCrewData.ContainsKey(key))
                    {
                        workPanelCrewTransport.PlannedCrew += 1;
                        string value = item.Function?.Project?.Name;
                        workPanelCrewTransport.PlannedCrewData.Add(key, value);
                    }
                }
                else if (item.VehicleId.HasValue)
                {
                    string key = item.Function?.Project?.Id.ToString();
                    if (!String.IsNullOrWhiteSpace(key) && !workPanelCrewTransport.PlannedTransportData.ContainsKey(key))
                    {
                        workPanelCrewTransport.PlannedTransport += 1;
                        string value = item.Function?.Project?.Name;
                        workPanelCrewTransport.PlannedTransportData.Add(key, value);
                    }
                }

                if (!string.IsNullOrWhiteSpace(item.CrewMemberId) || item.VehicleId.HasValue)
                {
                    string key = item.Function?.Project?.Id.ToString();
                    if (!String.IsNullOrWhiteSpace(key) && !workPanelCrewTransport.ProjectsWithPlanningData.ContainsKey(key))
                    {
                        workPanelCrewTransport.ProjectsWithPlanning += 1;
                        string value = item.Function?.Project?.Name;
                        workPanelCrewTransport.ProjectsWithPlanningData.Add(key, value);
                    }
                }
            }

            return workPanelCrewTransport;
        }

        private async Task<WorkPanelProjectModel> GetPanelProdjects()
        {
            IQueryable<ProjectEntity> projects = _projectStorage.QueryableEntity;
            IQueryable<ProjectFinancialEntity> finansials = _projectFinancialStorage.QueryableEntity.Include(x=>x.Project);
            finansials = finansials.Where(x => !x.InvoiceMomentId.HasValue);
            IQueryable<ProjectPlanningEntity> projectPlanning = _projectPlanningStorage.QueryableEntity.Include(x => x.Function).ThenInclude(x => x.Project);
            WorkPanelProjectModel panelProject = new WorkPanelProjectModel();

            List<ProjectEntity> projectsList = await projects.Where(x => x.Status == ProjectStatusEnum.Option).ToListAsync();
            List<ProjectFinancialEntity> finansialsList = await finansials.ToListAsync();
            List<ProjectPlanningEntity> projectPlanningList = await projectPlanning
                                                        .Where(x => x.Function != null && x.Function.Project != null && !string.IsNullOrWhiteSpace(x.CrewMemberId) && x.Function.Project.Status == ProjectStatusEnum.Cancelled)
                                                        .ToListAsync();
            List<ProjectPlanningEntity> projectPlanningList2 = await projectPlanning
                                                       .Where(x => x.Function != null && x.Function.Project != null && !string.IsNullOrWhiteSpace(x.CrewMemberId) && x.Function.Project.Status == ProjectStatusEnum.Cancelled)
                                                       .ToListAsync();


            panelProject.ProjectsOptionsData = projectsList.Distinct().ToDictionary(x => x.Id.ToString(), x => x.Name);
            panelProject.ProjectsOptions = panelProject.ProjectsOptionsData.Count;

            panelProject.ProjectsInvoiceData = finansialsList.Distinct().ToDictionary(x => x.ProjectId.ToString(), x => x.Project.Name);
            panelProject.ProjectsInvoice = panelProject.ProjectsInvoiceData.Count;

            panelProject.CancelledWithCrewData = projectPlanningList.Distinct().ToDictionary(x => x.Function.Project.Id.ToString(), x => x.Function.Project.Name);
            panelProject.CancelledWithCrew = panelProject.CancelledWithCrewData.Count;

            panelProject.CancelledWithTransportData = projectPlanningList2.Distinct().ToDictionary(x => x.Function.Project.Id.ToString(), x => x.Function.Project.Name);
            panelProject.CancelledWithTransport = panelProject.CancelledWithTransportData.Count;

            return panelProject;
        }

        private async Task<WorkPanelPlanningModel> GetPanelPlanning()
        {
            IQueryable<ProjectFunctionEntity> projectFunctions = _projectFunctionStorage.QueryableEntity.Include(x=>x.Project).Where(x => !x.PlanFrom.HasValue && x.ProjectFunctionGroupId.HasValue);
            List<ProjectFunctionEntity> projectFunctionsCrew = await projectFunctions.Where(x => x.Type == ProjectFunctionTypeEnum.Crew).ToListAsync();
            List<ProjectFunctionEntity> projectFunctionsTransport = await projectFunctions.Where(x => x.Type == ProjectFunctionTypeEnum.Transport).ToListAsync();


            WorkPanelPlanningModel workPanelPlanning = new WorkPanelPlanningModel();

            workPanelPlanning.UnscheduledCrew = new WorkPanelOptiontConfirmedModel();
            workPanelPlanning.UnscheduledCrew.OptionData = projectFunctionsCrew.Distinct().ToDictionary(x => x.ProjectId.ToString(), x => x.InternalName);
            workPanelPlanning.UnscheduledCrew.Option = workPanelPlanning.UnscheduledCrew.OptionData.Count;

            workPanelPlanning.UnscheduledTransport = new WorkPanelOptiontConfirmedModel();
            workPanelPlanning.UnscheduledTransport.OptionData = projectFunctionsTransport.Distinct().ToDictionary(x => x.ProjectId.ToString(), x => x.InternalName);
            workPanelPlanning.UnscheduledTransport.Option = workPanelPlanning.UnscheduledTransport.OptionData.Count;

            workPanelPlanning.NotEnoughCrew = new WorkPanelOptiontConfirmedModel();
            workPanelPlanning.NotEnoughCrew.OptionData = projectFunctionsCrew.Distinct().ToDictionary(x => x.ProjectId.ToString(), x => x.InternalName);
            workPanelPlanning.NotEnoughCrew.Option = workPanelPlanning.UnscheduledCrew.OptionData.Count;

            workPanelPlanning.NotEnoughTransport = new WorkPanelOptiontConfirmedModel();
            workPanelPlanning.NotEnoughTransport.OptionData = projectFunctionsTransport.Distinct().ToDictionary(x => x.ProjectId.ToString(), x => x.InternalName);
            workPanelPlanning.NotEnoughTransport.Option = workPanelPlanning.NotEnoughTransport.OptionData.Count;

            return workPanelPlanning;
        }

        private async Task<WorkPanelTodoModel> GetPanelTodo()
        {
            DateTime date = DateTime.Now;
            IQueryable<RepairEntity> repairs = _repairStorage.QueryableEntity.Include(x=>x.Equipment).Where(x=>!x.Until.HasValue || x.Until.HasValue && x.Until.Value.Date >= date.Date);
            IQueryable<EquipmentEntity> equipments = _equipmentStorage.QueryableEntity.Where(x => x.Quantity < 2);
            IQueryable<InvoiceEntity> invoices = _invoiceStorage.QueryableEntity.Where(x => x.State == InvoiceStateEnum.New || x.State == InvoiceStateEnum.Opened);
            WorkPanelTodoModel workPanelTodo = new WorkPanelTodoModel();

            List<InvoiceEntity> invoicesList = await invoices.ToListAsync();
            List<RepairEntity> repairsList = await repairs.ToListAsync();
            List<EquipmentEntity> equipmentsList = await equipments.ToListAsync();

            workPanelTodo.OpenInvoices.ExpiredData = invoicesList.Distinct().ToDictionary(x => x.Id.ToString(), x => x.Name);
            workPanelTodo.OpenInvoices.Expired = workPanelTodo.OpenInvoices.ExpiredData.Count;

            workPanelTodo.OpenRepairsData = repairsList.Distinct().ToDictionary(x => x.Id.ToString(), x => x.Equipment.Name);
            workPanelTodo.OpenRepairs = workPanelTodo.OpenRepairsData.Count;

            workPanelTodo.CriticalStockData = equipmentsList.Distinct().ToDictionary(x => x.Id.ToString(), x => x.Name);
            workPanelTodo.CriticalStock = equipments.Select(x => x.Quantity).Count();

            return workPanelTodo;
        }
    }
}
