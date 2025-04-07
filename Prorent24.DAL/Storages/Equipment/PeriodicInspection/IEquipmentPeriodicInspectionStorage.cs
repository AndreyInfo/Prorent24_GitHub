using Prorent24.Entity.Equipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.DAL.Storages.Equipment
{
    public interface IEquipmentPeriodicInspectionStorage : IBaseStorage<EquipmentPeriodicInspectionEntity>, IForeignDependencyStorage<EquipmentPeriodicInspectionEntity>
    {
        EquipmentPeriodicInspectionEntity GetByPeriodicInspection(int equipmentId, int periodicInspectionId);
    }
}
