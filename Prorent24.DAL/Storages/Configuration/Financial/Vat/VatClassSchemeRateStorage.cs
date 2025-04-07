using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.Financial.Vat
{
    internal class VatClassSchemeRateStorage:BaseStorage<VatClassSchemeRateEntity>, IVatClassSchemeRateStorage
    {
        public VatClassSchemeRateStorage(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }

        public Task<IPagedList<VatClassSchemeRateEntity>> GetAll(List<Predicate<VatClassSchemeRateEntity>> conditions = null)
        {
            return _repos.GetPagedListAsync();
        }

        public Task<List<VatClassSchemeRateEntity>> GetBySchemeId(int schemeId)
        {
            var vatClassSchemeRates = _repos.FromSql("SELECT * FROM sys_vat_class_schemes_rates WHERE VatSchemeId = {0}", schemeId);
            return vatClassSchemeRates.ToListAsync();
        }
    }
}
