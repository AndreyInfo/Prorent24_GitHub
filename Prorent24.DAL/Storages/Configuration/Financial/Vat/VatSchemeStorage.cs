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
    internal class VatSchemeStorage:BaseStorage<VatSchemeEntity>, IVatSchemeStorage
    {
        public VatSchemeStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Task<IPagedList<VatSchemeEntity>> GetAll(List<Predicate<VatSchemeEntity>> conditions = null)
        {
            return _repos.GetPagedListAsync(x=>x);
        }

        public async override Task<VatSchemeEntity> GetById(object id)
        {
            return await _repos.Table
                .Include(x => x.VatClassSchemeRates)
                .ThenInclude(x=>x.VatClass)
                .Where(x => x.Id == (int)id)
                .Select(x => x).FirstOrDefaultAsync();
        }

        public Task<List<VatSchemeEntity>> GetList()
        {
            return _repos.TableNoTracking.Select(x => x).ToListAsync();
        }
    }
}
