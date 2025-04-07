using Prorent24.Common.Models.Filters;
using Prorent24.DAL.Models.Project;
using Prorent24.Entity.Project;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Project
{
    public interface IProjectPlanningStorage : IBaseStorage<ProjectPlanningEntity>
    {
        Task<List<ProjectPlanningEntity>> GetAllByProject(ProjectPlanningFilterModel filter);
        Task<List<ProjectPlanningEntity>> GetAllGroupedByFuctions(int projectId);
        Task<List<ProjectPlanningEntity>> GetAllByDate(DateTime selectedDate);

        Task<List<ProjectPlanningEntity>> GetAllByCrewMembers(List<string> crewMembers);

        Task<List<ProjectPlanningEntity>> GetAllByProjectFuntion(List<ProjectFunctionFilterModel> filters);

        Task<List<ProjectPlanningEntity>> GetAll();

        Task<List<ProjectPlanningEntity>> GetAllByEntities(IQueryable<ProjectPlanningEntity> queryableEntity, ProjectFunctionTypeEnum type, List<string> entityIds);

        Task<List<ProjectPlanningEntity>> GetAllForModuleTimeLine(DateTime from, DateTime until, ProjectFunctionTypeEnum type, List<string> entityIds);
        Task<List<ProjectPlanningEntity>> GetAllByProjectFuntionByType(int functionId, object entityId, ProjectFunctionTypeEnum type);
    }
}
