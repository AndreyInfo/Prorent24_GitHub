using Microsoft.EntityFrameworkCore;
using Prorent24.Common.Models.Filters;
using Prorent24.DAL.Models.Project;
using Prorent24.Entity.Project;
using Prorent24.Enum.Project;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Project
{
    internal class ProjectPlanningStorage : BaseStorage<ProjectPlanningEntity>, IProjectPlanningStorage
    {
        public ProjectPlanningStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<List<ProjectPlanningEntity>> GetAllByDate(DateTime selectedDate)
        {
            return await _repos.TableNoTracking
                .Include(x => x.CrewMember)
                .ThenInclude(x => x.User)
                .Include(x => x.Vehicle)
                .Include(x => x.Function)
                .ThenInclude(x => x.Project)
                .Where(x => x.PlanFrom.HasValue && x.PlanUntil.HasValue && x.PlanFrom.Value.Date <= selectedDate.Date && x.PlanUntil.Value.Date >= selectedDate.Date)
                .ToListAsync();
        }

        public Task<IPagedList<ProjectPlanningEntity>> GetAll(List<Predicate<ProjectPlanningEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProjectPlanningEntity>> GetAll()
        {
            return await _repos.TableNoTracking
                .Include(x => x.Function)
                  .ToListAsync();
        }


        public async Task<List<ProjectPlanningEntity>> GetAllByProject(ProjectPlanningFilterModel filter)
        {
            if (filter.FunctionGroupId != null)
            {
                return await _repos.TableNoTracking.Include(x => x.Function)
                    .Include(y => y.CrewMember).ThenInclude(y => y.User)
                    .Include(z => z.Vehicle)
                    .Where(z => z.Function.ProjectId == filter.ProjectId
                   && z.Function.Type == filter.Type
                   && z.Function.ProjectFunctionGroupId == filter.FunctionGroupId).ToListAsync();

            }
            else
            {
                return await _repos.TableNoTracking.Include(x => x.Function)
                    .Include(y => y.CrewMember).ThenInclude(y => y.User)
                    .Include(z => z.Vehicle)
                    .Where(z => z.Function.ProjectId == filter.ProjectId
                    && z.Function.Type == filter.Type).ToListAsync();
            }
        }

        public async Task<List<ProjectPlanningEntity>> GetAllGroupedByFuctions(int projectId)
        {

            return await _repos.TableNoTracking
                .Include(x => x.Function)
                .Include(y => y.CrewMember).ThenInclude(y => y.User)
                .Include(z => z.Vehicle)
                .Where(z => z.Function.ProjectId == projectId).ToListAsync();

        }

        public override async Task<ProjectPlanningEntity> GetById(object id)
        {
            return await _repos.Table
                .Include(x => x.Function).ThenInclude(x => x.Project)
                .Include(y => y.CrewMember).ThenInclude(y => y.User)
                .Include(z => z.Vehicle)
                .FirstOrDefaultAsync(x => x.Id == (int)id);
        }

        public async Task<List<ProjectPlanningEntity>> GetAllByCrewMembers(List<string> crewMembers)
        {
            return await _repos.TableNoTracking.Include(x => x.Function).ThenInclude(y => y.Project).ThenInclude(y => y.Times)
                .Include(x => x.Function).ThenInclude(y => y.ProjectFunctionGroup)//.Include(y => y.CrewMember)
                .Where(z => crewMembers.Contains(z.CrewMemberId) && z.Function != null && z.Function.Project != null).ToListAsync();
        }

        //not corect working
        public async Task<List<ProjectPlanningEntity>> GetAllByProjectFuntion(List<ProjectFunctionFilterModel> filters)
        {
            List<ProjectPlanningEntity> res = await _repos.TableNoTracking.Include(x => x.Function).ThenInclude(y => y.ProjectFunctionGroup)
                .Include(x => x.Function).ThenInclude(y => y.Project)
                .Include(x => x.CrewMember).ThenInclude(z => z.User)
                .Include(y => y.Vehicle).ToListAsync();
            var filterQuery = from plan in res
                        join filter in filters  on plan.FunctionId+"_"+plan.Function?.ProjectId equals filter.FunctionId + "_" + filter.ProjectId 
                        select plan;
            return filterQuery.ToList();
        }

        public async Task<List<ProjectPlanningEntity>> GetAllByProjectFuntionByType(int functionId, object entityId, ProjectFunctionTypeEnum type)
        {
            if (type == ProjectFunctionTypeEnum.Crew)
            {
                return await _repos.TableNoTracking
                .Where(x => x.FunctionId == functionId && x.CrewMemberId == entityId.ToString())
                .ToListAsync();
            }
            else
            {
                return await _repos.TableNoTracking
                .Where(x => x.FunctionId == functionId && x.VehicleId == (int)entityId)
                .ToListAsync();
            }
        }


        public async Task<List<ProjectPlanningEntity>> GetAllByEntities(IQueryable<ProjectPlanningEntity> queryableEntity, ProjectFunctionTypeEnum type, List<string> entityIds)
        {
            if (type == ProjectFunctionTypeEnum.Crew)
            {
                return await queryableEntity.Include(x => x.Function).ThenInclude(y => y.Project)
                    .Include(x => x.CrewMember).ThenInclude(z => z.User)
                    .Where(z => entityIds.Contains(z.CrewMemberId))
                    .ToListAsync();
            }
            else
            {
                return await queryableEntity.Include(x => x.Function).ThenInclude(y => y.Project)
                   .Include(y => y.Vehicle)
                   .Where(z => entityIds.Contains(z.VehicleId.ToString()))
                   .ToListAsync();

            }
        }

        public virtual async Task<ProjectPlanningEntity> Update(ProjectPlanningEntity model)
        {
            await Task.Run(() =>
            {
                _repos.Update(model, f => f.ProjectLeader, f => f.PlanFrom, f => f.PlanUntil, f => f.TransportType,
                    f => f.RateType, f => f.Costs, f => f.PlannedCosts, f => f.CrewMemberHourlyRate, f => f.Remark);
                _unitOfWork.SaveChanges();
            });

            return model;
        }

        public async Task<List<ProjectPlanningEntity>> GetAllForModuleTimeLine(DateTime from, DateTime until, ProjectFunctionTypeEnum type, List<string> entityIds)
        {
            if (type == ProjectFunctionTypeEnum.Crew)
            {
                return await _repos.TableNoTracking.Include(x => x.Function).ThenInclude(y => y.Project)
                    .Include(x => x.CrewMember).ThenInclude(z => z.User)
                    .Where(z => entityIds.Contains(z.CrewMemberId) && ((from >= z.PlanFrom && from <= z.PlanUntil) || (until >= z.PlanFrom && until <= z.PlanUntil)))
                    .ToListAsync();
            }
            else
            {
                return await _repos.TableNoTracking.Include(x => x.Function).ThenInclude(y => y.Project)
                    .Include(y => y.Vehicle)
                    .Where(z => entityIds.Contains(z.VehicleId.ToString()) && ((from >= z.PlanFrom && from <= z.PlanUntil) || (until >= z.PlanFrom && until <= z.PlanUntil)))
                    .ToListAsync();

            }
        }
    }
}
