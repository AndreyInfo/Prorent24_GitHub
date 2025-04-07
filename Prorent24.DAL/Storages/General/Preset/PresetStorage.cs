using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.General;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.General.Preset
{
    internal class PresetStorage : BaseStorage<PresetEntity>, IPresetStorage
    {
        public PresetStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<List<PresetEntity>> GetPresets(ModuleTypeEnum moduleType)
        {
            return await _repos.Table.Where(x => x.ModuleType == moduleType).ToListAsync();
        }

        #region [Obsolete("Don't use")]

        public Task<IPagedList<PresetEntity>> GetAll(List<Predicate<PresetEntity>> conditions = null)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
