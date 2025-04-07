using Prorent24.Entity.Configuration.Financial;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.Financial.Vat
{
    public interface IVatSchemeStorage : IBaseStorage<VatSchemeEntity>
    {
        Task<List<VatSchemeEntity>> GetList();
    }

}
