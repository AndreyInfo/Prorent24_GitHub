using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Maintenance;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Maintenance.Inspection
{
    internal class InspectionSerialNumberStorage : BaseStorage<InspectionSerialNumberEntity>, IInspectionSerialNumberStorage
    {
        public InspectionSerialNumberStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IPagedList<InspectionSerialNumberEntity>> GetAll(List<Predicate<InspectionSerialNumberEntity>> conditions = null)
        {
            return await _repos.TableNoTracking.Include(x => x.Equipment).Include(x=>x.SerialNumber).ToPagedListAsync(0, 500);
        }

        public async Task<IPagedList<InspectionSerialNumberEntity>> GetAllByForeignId(int id)
        {
            return await _repos.TableNoTracking
                .Include(x => x.Equipment)
                .Include(x => x.SerialNumber)
                .Where(x=>x.InspectionId == id).ToPagedListAsync(0, 500);
        }
    }
}
