using Prorent24.Entity.Configuration.Financial;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.Financial.Vat
{
    public interface IVatClassSchemeRateStorage : IBaseStorage<VatClassSchemeRateEntity>
    {
        Task<List<VatClassSchemeRateEntity>> GetBySchemeId(int schemeId);

    }

}
