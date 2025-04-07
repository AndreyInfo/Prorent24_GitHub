using Prorent24.Entity.Configuration.Financial;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.Financial.DiscountGroup
{
    internal class DiscountGroupStorage : BaseStorage<DiscountGroupEntity>, IDiscountGroupStorage
    {
        public DiscountGroupStorage(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }

        /// <summary>
        /// Get pagedList DiscountGroup
        /// </summary>
        /// <returns></returns>
        public Task<IPagedList<DiscountGroupEntity>> GetAll(List<Predicate<DiscountGroupEntity>> conditions = null)
        {
            return _repos.GetPagedListAsync(x=>x);
        }
        
    }
}
