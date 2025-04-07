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
    internal class EquipmentPeriodicInspectionStorage : BaseStorage<EquipmentPeriodicInspectionEntity>, IEquipmentPeriodicInspectionStorage
    {
        public EquipmentPeriodicInspectionStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IPagedList<EquipmentPeriodicInspectionEntity>> GetAll(List<Predicate<EquipmentPeriodicInspectionEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedList<EquipmentPeriodicInspectionEntity>> GetAllByForeignId(int id)
        {
            var result = _repos.Table
                .Include(x => x.Equipment)
                .Include(x => x.PeriodicInspection)
                .Where(x => x.EquipmentId == id).Select(x => x).ToPagedListAsync(0, 100);
            return result;
        }

        public EquipmentPeriodicInspectionEntity GetByPeriodicInspection(int equipmentId, int periodicInspectionId)
        {
            var result = _repos.Table
                .Include(x => x.Equipment)
                .Include(x => x.PeriodicInspection)
                .Where(x => x.EquipmentId == equipmentId && x.PeriodicInspectionId == periodicInspectionId).Select(x => x).FirstOrDefault();
            return result;
        }
    }
}
