using Prorent24.Common.Models.Filters;
using Prorent24.Entity.Equipment;
using Prorent24.UnitOfWork.PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Equipment
{
    public interface IEquipmentStorage : IBaseStorage<EquipmentEntity>
    {
        //Task<IPagedList<EquipmentEntity>> GetAllByFilter(List<SelectedFilter> filterList);

        Task<List<EquipmentEntity>> Search(string text);

        Task SetFolderId(int equipmentId, int folderId);

        Task<List<EquipmentEntity>> GetByIds(List<int> ids);

        Task<IPagedList<EquipmentEntity>> GetAllByFilter(IQueryable<EquipmentEntity> queryableEntity, string searchText = "");

    }
}
