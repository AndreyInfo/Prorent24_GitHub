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
    internal class EquipmentQRCodeStorage : BaseStorage<EquipmentQRCodeEntity>, IEquipmentQRCodeStorage
    {
        public EquipmentQRCodeStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [Obsolete]
        public Task<IPagedList<EquipmentQRCodeEntity>> GetAll(List<Predicate<EquipmentQRCodeEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedList<EquipmentQRCodeEntity>> GetAllByForeignId(int equipmentId, int? serialNumberId)
        {
            var result = _repos.Table
                  .Include(x => x.Equipment)
                  .Include(x => x.SerialNumber)
                  .Where(x => x.EquipmentId == equipmentId && x.SerialNumberId == serialNumberId)
                  .Select(x => x)
                  .ToPagedListAsync(0, 100);
            return result;
        }
    }
}
