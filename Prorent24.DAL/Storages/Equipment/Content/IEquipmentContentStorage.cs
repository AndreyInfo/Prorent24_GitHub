using Prorent24.Entity.Equipment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Equipment
{
    public interface IEquipmentContentStorage : IBaseStorage<EquipmentContentEntity>, IForeignDependencyStorage<EquipmentContentEntity>
    {
        Task<EquipmentContentEntity> GetByKeys(int equipmentId, int contentId);
    }
}
