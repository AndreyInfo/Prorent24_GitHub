using Prorent24.Entity.Invoice;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Invoice
{
    internal class InvoiceLineStorage : BaseStorage<InvoiceLineEntity>, IInvoiceLineStorage
    {
        public InvoiceLineStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IPagedList<InvoiceLineEntity>> GetAll(List<Predicate<InvoiceLineEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }
    }
}
