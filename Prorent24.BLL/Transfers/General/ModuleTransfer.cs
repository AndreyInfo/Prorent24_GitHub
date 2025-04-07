using Prorent24.BLL.Models.General.Modules;
using Prorent24.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.General
{
    public static class ModuleTransfer
    {
        public static List<ModuleEntity> TransferToModuleEntity(this List<ModuleDto> moduleEntities)
        {
            List<ModuleEntity> modules = moduleEntities.Select(x => new ModuleEntity()
            {
                Id = x.Id,
                ModuleType = x.ModuleType,
                Name = x.Name,
                Order = x.Order
            }).ToList();

            return modules;
        }

        public static List<ModuleDto> TransferToModuleDto(this List<ModuleEntity> moduleEntities)
        {
            List<ModuleDto> modules = moduleEntities.Select(x => new ModuleDto()
            {
                Id = x.Id,
                ModuleType = x.ModuleType,
                Name = x.Name,
                Order = x.Order
            }).ToList();

            return modules;
        }
    }
}
