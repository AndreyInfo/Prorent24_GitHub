using Prorent24.Entity.Configuration.Financial;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.Financial.FactorGroup
{
    internal class FactorGroupOptionStorage : BaseStorage<FactorGroupOptionEntity>, IFactorGroupOptionStorage
    {
        public FactorGroupOptionStorage(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }

        /// <summary>
        /// Get pagedList FactorGroup
        /// </summary>
        /// <returns></returns>
        public Task<IPagedList<FactorGroupOptionEntity>> GetAll(List<Predicate<FactorGroupOptionEntity>> conditions = null)
        {
            return _repos.GetPagedListAsync();
        }
        
    }
}
