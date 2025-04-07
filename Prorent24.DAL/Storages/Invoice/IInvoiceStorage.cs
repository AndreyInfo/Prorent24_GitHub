using Prorent24.Common.Models.Filters;
using Prorent24.Entity.Invoice;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Invoice
{
    public interface IInvoiceStorage : IBaseStorage<InvoiceEntity>
    {
        Task<InvoiceEntity> GetByDocumentId(int id);

        Task<IPagedList<InvoiceEntity>> GetAllByFilter(IQueryable<InvoiceEntity> queryableEntity, string searchText = "");
    }
}
