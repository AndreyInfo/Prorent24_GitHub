using Prorent24.Common.Models.Filters;
using Prorent24.Entity.Maintenance;
using Prorent24.UnitOfWork.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Maintenance.Repair
{
    public interface IRepairStorage : IBaseStorage<RepairEntity>
    {
        Task<IPagedList<RepairEntity>> GetAllByFilter(List<SelectedFilter> filterList);
    }
}
