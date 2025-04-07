using Prorent24.BLL.Models.General.Modules;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.Module
{
    public interface IModuleService
    {
        Task<List<ModuleDto>> GetModuleItems();
        Task ImportModules();

        Task<List<ModuleDto>> GetModuleItemsBaseOnPermission();

    }
}
