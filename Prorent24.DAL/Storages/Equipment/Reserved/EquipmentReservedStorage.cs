using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Equipment;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Equipment.EquipmentReserved
{
   internal class EquipmentReservedStorage : BaseStorage<EquipmentReservedEntity>, IEquipmentReservedStorage
    {
        public EquipmentReservedStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public Task<IPagedList<EquipmentReservedEntity>> GetAll(List<Predicate<EquipmentReservedEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EquipmentReservedEntity>> GetInReserve(List<int> ids)
        {
            return await _repos.Table.Include(x => x.ProjectEquipmentEntity).Where(x => ids.Any(i => i == x.ProjectEquipmentEntity.EquipmentId)).ToListAsync();
        }

        public async Task PutInReserve(EquipmentReservedEntity entity)
        {
            bool created = _repos.TableNoTracking.Any(x => x.ProjectEquipmentId == entity.ProjectEquipmentId);
            if (created)
            {
               await this.Update(entity);
            }
            else
            {
                await this.Create(entity);
            }
        }
    }
}
