using Prorent24.Entity.Configuration.Financial;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.Financial.Ledger
{
    internal class LedgerStorage : BaseStorage<LedgerEntity>, ILedgerStorage
    {
        public LedgerStorage(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }
        /// <summary>
        /// Get list Ledger
        /// </summary>
        /// <returns></returns>
        public Task<IPagedList<LedgerEntity>> GetAll(List<Predicate<LedgerEntity>> conditions = null)
        {
            return _repos.GetPagedListAsync(x=>x);
        }

    }
}
