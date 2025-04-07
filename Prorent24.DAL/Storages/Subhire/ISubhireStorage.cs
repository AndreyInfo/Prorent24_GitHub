using Prorent24.Entity.Subhire;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Subhire
{
    public interface ISubhireStorage : IBaseStorage<SubhireEntity>
    {
        Task<List<SubhireEntity>> GetByIds(int[] ids);

        Task<List<SubhireEntity>> GetByProjects(List<int> projectIds);

        Task<List<int>> GetSubhireProjects(int subhireId);

        Task<List<SubhireProjectEntity>> CreateSubhireProjects(List<SubhireProjectEntity> models);

        Task<IPagedList<SubhireEntity>> GetAllByFilter(IQueryable<SubhireEntity> queryableEntity, string searchText = "");

    }
}
