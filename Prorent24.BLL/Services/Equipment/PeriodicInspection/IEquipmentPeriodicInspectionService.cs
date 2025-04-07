using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Equipment.PeriodicInspection
{
    public interface IEquipmentPeriodicInspectionService
    {
        Task<EquipmentPeriodicInspectionDto> GetByPeriodicInspectionId(int equipmentId, int periodicInspectionId);
        Task<BaseListDto> GetPagedList(int equipmentId);
        Task<EquipmentPeriodicInspectionDto> Save(EquipmentPeriodicInspectionDto dto);
    }
}
