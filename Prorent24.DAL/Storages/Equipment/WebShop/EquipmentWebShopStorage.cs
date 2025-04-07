using Prorent24.Entity.Equipment;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Equipment
{
    internal class EquipmentWebShopStorage : BaseStorage<EquipmentWebShopEntity>, IEquipmentWebShopStorage
    {
        public EquipmentWebShopStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IPagedList<EquipmentWebShopEntity>> GetAll(List<Predicate<EquipmentWebShopEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public EquipmentWebShopEntity GetByEquipmentId(int equipmentId)
        {
            return _repos.GetFirstOrDefault(x => x, predicate: x => x.EquipmentId == equipmentId);
        }
    }
}
