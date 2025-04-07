using Prorent24.Entity.Equipment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Equipment
{
    public interface IEquipmentAccessoryStorage : IBaseStorage<EquipmentAccessoryEntity>, IForeignDependencyStorage<EquipmentAccessoryEntity>
    {
        Task<EquipmentAccessoryEntity> GetByKeys(int equipmentId, int accessoryId);
    }
}
