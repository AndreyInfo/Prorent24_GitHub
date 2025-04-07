using Prorent24.Entity.Project;
using Prorent24.UnitOfWork.PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Project
{
    public interface IProjectStorage : IBaseStorage<ProjectEntity>
    {
        Task<IPagedList<ProjectEntity>> GetAllByFilter(IQueryable<ProjectEntity> queryableEntity, string searchText="");

        Task<List<ProjectEntity>> GetByIds(List<int> ids);
    }
}
