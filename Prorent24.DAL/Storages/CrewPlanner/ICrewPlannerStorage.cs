using Prorent24.Entity.CrewPlanner;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.CrewPlanner
{
    public interface ICrewPlannerStorage : IBaseStorage<CrewPlannerEntity>
    {
        Task<List<CrewPlannerEntity>> GetByEntityId(CrewPlannerEntity entity);

        Task<List<CrewPlannerEntity>> GetAll(ProjectFunctionTypeEnum type, List<string> ids);
        Task<List<CrewPlannerEntity>> GetAllForTimeLine(IQueryable<CrewPlannerEntity> queryableEntity, ProjectFunctionTypeEnum type, List<string> ids);

        Task<List<CrewPlannerEntity>> GetAllForModuleTimeLine(DateTime dateFrom, DateTime dateUntil, ProjectFunctionTypeEnum type, List<string> ids);

    }
}
