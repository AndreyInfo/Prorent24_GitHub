using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.Financial.Vat
{
    internal class VatClassStorage:BaseStorage<VatClassEntity>, IVatClassStorage
    {
        public VatClassStorage(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }

        public Task<IPagedList<VatClassEntity>> GetAll(List<Predicate<VatClassEntity>> conditions = null)
        {
            return _repos.GetPagedListAsync();
        }
    }
}
