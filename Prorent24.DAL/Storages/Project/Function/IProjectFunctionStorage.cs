using Prorent24.Common.Models.Filters;
using Prorent24.Entity.Project;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Project
{
    public interface IProjectFunctionStorage : IBaseStorage<ProjectFunctionEntity>
    {
        Task<List<ProjectFunctionEntity>> GetAllByProject(int projectId, ProjectFunctionTypeEnum? type = null);
        Task<List<ProjectFunctionEntity>> GetAllFunction(List<Predicate<ProjectFunctionEntity>> conditions = null);

        //Task<List<ProjectFunctionEntity>> GetAllFunctionForGeneralTimeLine(List<SelectedFilter> filterList=null);
        Task<List<ProjectFunctionEntity>> GetAllFunctionForGeneralTimeLine(IQueryable<ProjectFunctionEntity> queryableEntity, string likeSearch);


    }
}
