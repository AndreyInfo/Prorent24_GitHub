using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Equipment;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Equipment
{
    internal class EquipmentAccessoryStorage : BaseStorage<EquipmentAccessoryEntity>, IEquipmentAccessoryStorage
    {
        public EquipmentAccessoryStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<IPagedList<EquipmentAccessoryEntity>> GetAllByForeignId(int id)
        {
            var result = await _repos.Table
                .Include(x => x.Equipment)
                .Include(x => x.Accessory)
                .Where(x => x.EquipmentId == id).Select(x => x).ToPagedListAsync(0, 100);
            return result;
        }
        public Task<IPagedList<EquipmentAccessoryEntity>> GetAll(List<Predicate<EquipmentAccessoryEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public async override Task<EquipmentAccessoryEntity> GetById(object id)
        {
            var result = await _repos.Table
                .Include(x => x.Equipment)
                .Include(x => x.Accessory)
                .Where(x => x.Id == (int)id).Select(x => x).FirstOrDefaultAsync();
            return result;
        }

        public async Task<EquipmentAccessoryEntity> GetByKeys(int equipmentId, int accessoryId)
        {
            var result = await _repos.Table
                .Where(x => x.EquipmentId == equipmentId && x.AccessoryId == accessoryId).FirstOrDefaultAsync();
            return result;
        }
    }
}
