using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.General;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.General.Filter
{
    internal class SavedFilterStorage : BaseStorage<SavedFilterEntity>, ISavedFilterStorage
    {
        public SavedFilterStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<List<SavedFilterEntity>> GetSavedFilters(ModuleTypeEnum moduleType)
        {
            return await _repos.Table.Where(x => x.ModuleType == moduleType).ToListAsync();
        }

        #region [Obsolete("Don't use")]

        public Task<IPagedList<SavedFilterEntity>> GetAll(List<Predicate<SavedFilterEntity>> conditions = null)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
