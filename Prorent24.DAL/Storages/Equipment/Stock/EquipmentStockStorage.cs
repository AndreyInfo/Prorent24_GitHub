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
    internal class EquipmentStockStorage : BaseStorage<EquipmentStockEntity>, IEquipmentStockStorage
    {
        public EquipmentStockStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IPagedList<EquipmentStockEntity>> GetAll(List<Predicate<EquipmentStockEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IPagedList<EquipmentStockEntity>> GetAllByForeignId(int id)
        {
            var result = await _repos.Table
                .Include(x => x.Equipment)
                .Where(x => x.EquipmentId == id).Select(x => x).ToPagedListAsync(0, 100);
            return result;
        }

        public async override Task<EquipmentStockEntity> GetById(object id)
        {
            // x => x, predicate: c => c.ContactId == contactId
            return await _repos.Table
                .Include(x => x.Equipment)
                .SingleOrDefaultAsync(predicate: c => c.Id == (int)id);
        }
    }
}
