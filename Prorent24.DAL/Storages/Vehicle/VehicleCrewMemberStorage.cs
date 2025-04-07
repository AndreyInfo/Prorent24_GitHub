using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Vehicle;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Vehicle
{
    internal class VehicleCrewMemberStorage : BaseStorage<VehicleVisibilityCrewMemberEntity>, IVehicleCrewMemberStorage
    {
        public VehicleCrewMemberStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async override Task<VehicleVisibilityCrewMemberEntity> Create(VehicleVisibilityCrewMemberEntity model)
        {
            VehicleVisibilityCrewMemberEntity result = await base.Create(model);
            return await _repos.Table.Include(x => x.CrewMember).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAllByVehicleId(int Id)
        {
            var entities = await _repos.Table.Where(x => x.VehicleId == Id).Select(x => x).ToListAsync();
            if (entities != null)
            {
                await Task.Run(() =>
                {
                    _repos.Delete(entities);
                    _unitOfWork.SaveChanges();
                });

            }
            else
            {
                return false;
            }

            entities = await _repos.Table.Where(x => x.VehicleId == Id).Select(x => x).ToListAsync();

            if (entities != null || entities.Count > 0)
            {
                return false;
            }
            else
            {
                return true;

            }
        }

        public Task<IPagedList<VehicleVisibilityCrewMemberEntity>> GetAll(List<Predicate<VehicleVisibilityCrewMemberEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

    }
}
