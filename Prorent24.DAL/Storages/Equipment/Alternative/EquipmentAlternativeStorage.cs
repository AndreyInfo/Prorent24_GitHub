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
    internal class EquipmentAlternativeStorage : BaseStorage<EquipmentAlternativeEntity>, IEquipmentAlternativeStorage
    {
        public EquipmentAlternativeStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<IPagedList<EquipmentAlternativeEntity>> GetAllByForeignId(int equipmentId)
        {
            var result = await _repos.Table
                .Include(x => x.Equipment)
                .Include(x => x.Alternative)
                .Where(x => x.EquipmentId == equipmentId).Select(x => x).ToPagedListAsync(0, 100);
            return result;
        }

        public async override Task<EquipmentAlternativeEntity> GetById(object id)
        {
            var result = await _repos.Table
                .Include(x => x.Equipment)
                .Include(x => x.Alternative)
                .Where(x => x.Id == (int)id).Select(x => x).FirstOrDefaultAsync();
            return result;
        }

        public Task<IPagedList<EquipmentAlternativeEntity>> GetAll(List<Predicate<EquipmentAlternativeEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public async Task<EquipmentAlternativeEntity> GetByKeys(int equipmentId, int alternativeId)
        {
            var result = await _repos.Table
                .Include(x => x.Equipment)
                .Include(x => x.Alternative)
                .Where(x => x.EquipmentId == equipmentId && x.AlternativeId == alternativeId).FirstOrDefaultAsync();
            return result;
        }
    }
}
