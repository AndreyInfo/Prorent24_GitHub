using Prorent24.Common.Models.Filters;
using Prorent24.Entity.Tasks;
using Prorent24.UnitOfWork.PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Tasks
{
    public interface ITaskStorage : IBaseStorage<TaskEntity>
    {
        Task<TaskEntity> GetDetails(int id);
        //IQueryable<TaskEntity> GetAllByFilter(List<SelectedFilter> filterList);
        Task<IPagedList<TaskEntity>> GetAllByFilter(IQueryable<TaskEntity> queryableEntity, string userId, string searchText = "");

        Task<List<TaskEntity>> GetAllByCrewMember(string crewMemberId);
    }
}
