using Prorent24.Entity.Maintenance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.DAL.Storages.Maintenance.Inspection
{
    public interface IInspectionSerialNumberStorage : IBaseStorage<InspectionSerialNumberEntity>, IForeignDependencyStorage<InspectionSerialNumberEntity>
    {
    }
}
