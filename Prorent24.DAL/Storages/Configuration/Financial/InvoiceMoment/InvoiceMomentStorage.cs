using Prorent24.Entity.Configuration.Financial;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.Financial.InvoiceMoment
{
    internal class InvoiceMomentStorage :BaseStorage<InvoiceMomentEntity>, IInvoiceMomentStorage
    {
        public InvoiceMomentStorage(IUnitOfWork unitOfWork):base(unitOfWork)
        {
        }

        /// <summary>
        /// get pagedList InvoiceMoment
        /// </summary>
        /// <returns></returns>
        public Task<IPagedList<InvoiceMomentEntity>> GetAll(List<Predicate<InvoiceMomentEntity>> conditions = null)
        {
            return _repos.GetPagedListAsync();
        }
    }
}
