using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Invoice;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Invoice
{
    internal class InvoiceExcludedStorage : BaseStorage<InvoiceExcludedEntity>, IInvoiceExcludedStorage
    {
        public InvoiceExcludedStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IPagedList<InvoiceExcludedEntity>> GetAll(List<Predicate<InvoiceExcludedEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public async override Task<InvoiceExcludedEntity> GetById(object id)
        {
            return await _repos.TableNoTracking.Include(x => x.InvoiceExluded).Where(x => x.Id == (int)id).FirstOrDefaultAsync();
        }
    }
}
