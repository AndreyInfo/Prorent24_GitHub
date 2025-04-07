using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Equipment;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.DAL.Storages.Equipment
{
    internal class EquipmentSupplierStorage : BaseStorage<EquipmentSupplierEntity>, IEquipmentSupplierStorage
    {
        public EquipmentSupplierStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IPagedList<EquipmentSupplierEntity>> GetAll(List<Predicate<EquipmentSupplierEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedList<EquipmentSupplierEntity>> GetAllByForeignId(int id)
        {
            var result = _repos.Table
                .Include(x => x.Contact)
                .ThenInclude(x => x.DefaultContact)
                .Include(x => x.Equipment)
                .Where(x => x.EquipmentId == id).Select(x => x).ToPagedListAsync(0, 100);
            return result;
        }

        public async override Task<EquipmentSupplierEntity> GetById(object id)
        {
            return await _repos.Table.Include(x => x.Contact).Where(x => x.Id == (int)id).Select(x => x).FirstOrDefaultAsync();
        }
    }
}
