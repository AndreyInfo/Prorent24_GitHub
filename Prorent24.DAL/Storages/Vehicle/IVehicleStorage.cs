using Prorent24.Common.Models.Filters;
using Prorent24.Entity.Vehicle;
using Prorent24.UnitOfWork.PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Vehicle
{
    public interface IVehicleStorage : IBaseStorage<VehicleEntity>
    {
        Task<VehicleEntity> GetDetails(int id);
        Task<IPagedList<VehicleEntity>> GetAllByFilter(List<SelectedFilter> filterList, string userId);

        Task Import(List<VehicleEntity> vehicleEntities);

        Task<List<VehicleEntity>> GetForPlanning(string userId);

        Task<List<VehicleEntity>> GetByIds(List<int> ids);


        Task<IPagedList<VehicleEntity>> GetAllByFilter(IQueryable<VehicleEntity> queryableEntity, string searchText, string userId);
    }
}
