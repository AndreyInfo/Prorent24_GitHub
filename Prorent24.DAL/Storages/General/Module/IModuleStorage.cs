using Prorent24.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.General.Module
{
    public interface IModuleStorage
    {
        Task<List<ModuleEntity>> GetAll();
        Task ImportModules(List<ModuleEntity> modules);
    }
}
