using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.General;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.General.Filter
{
    public interface ISavedFilterStorage : IBaseStorage<SavedFilterEntity>
    {
        Task<List<SavedFilterEntity>> GetSavedFilters(ModuleTypeEnum moduleType);
    }
}
