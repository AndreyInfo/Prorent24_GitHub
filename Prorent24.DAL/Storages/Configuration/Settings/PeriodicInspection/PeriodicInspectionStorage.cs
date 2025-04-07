using Prorent24.Entity.Configuration.Settings;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.Settings.PeriodicInspection
{
    internal class PeriodicInspectionStorage : BaseStorage<PeriodicInspectionEntity>, IPeriodicInspectionStorage
    {
        public PeriodicInspectionStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Get list Periodic Inspection
        /// </summary>
        /// <returns></returns>
        public Task<IPagedList<PeriodicInspectionEntity>> GetAll(List<Predicate<PeriodicInspectionEntity>> conditions = null)
        {
            return _repos.GetPagedListAsync<PeriodicInspectionEntity>(x => x);
        }
    }
}
