using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prorent24.Entity.Equipment;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.DAL.Storages.Equipment
{
    internal class EquipmentSerialNumberStorage : BaseStorage<EquipmentSerialNumberEntity>, IEquipmentSerialNumberStorage
    {
        public EquipmentSerialNumberStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IPagedList<EquipmentSerialNumberEntity>> GetAll(List<Predicate<EquipmentSerialNumberEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedList<EquipmentSerialNumberEntity>> GetAllByForeignId(int id)
        {
            return _repos.Table.Where(x => x.EquipmentId == id).ToPagedListAsync(0,100);
        }
    }
}
