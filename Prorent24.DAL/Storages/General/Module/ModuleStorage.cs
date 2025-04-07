using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prorent24.Entity;
using Prorent24.UnitOfWork;

namespace Prorent24.DAL.Storages.General.Module
{
    internal class ModuleStorage : BaseStorage<ModuleEntity>, IModuleStorage
    {
        public ModuleStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<List<ModuleEntity>> GetAll()
        {
            return await _repos.TableNoTracking.ToListAsync();
        }

        public async Task ImportModules(List<ModuleEntity> modules)
        {
            IEnumerable<ModuleEntity> moduleEntities = modules.Where(x => !_repos.TableNoTracking.Any(m => m.ModuleType == x.ModuleType));

            if (moduleEntities.Any())
            {
                await _repos.InsertAsync(moduleEntities);

                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
