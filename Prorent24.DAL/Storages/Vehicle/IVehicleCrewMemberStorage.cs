using Prorent24.Entity.Tasks;
using Prorent24.Entity.Vehicle;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Vehicle
{
    public interface IVehicleCrewMemberStorage: IBaseStorage<VehicleVisibilityCrewMemberEntity>
    {
        Task<bool> DeleteAllByVehicleId(int Id);
    }
}
