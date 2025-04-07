using Prorent24.Entity.Equipment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Equipment.EquipmentReserved
{
    public interface IEquipmentReservedStorage : IBaseStorage<EquipmentReservedEntity>
    {
        Task PutInReserve(EquipmentReservedEntity entity);
        Task<List<EquipmentReservedEntity>> GetInReserve(List<int> ids);
    }
}
