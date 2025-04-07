using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.Financial.FactorGroup
{
    internal class FactorGroupStorage : BaseStorage<FactorGroupEntity>, IFactorGroupStorage
    {
        public FactorGroupStorage(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }

        public async override Task<FactorGroupEntity> GetById(object id)
        {
            return await _repos.Table.Include(x => x.FactorGroupOptions).FirstOrDefaultAsync(x => x.Id == (int)id);
        }

        /// <summary>
        /// Get pagedList FactorGroup
        /// </summary>
        /// <returns></returns>
        public Task<IPagedList<FactorGroupEntity>> GetAll(List<Predicate<FactorGroupEntity>> conditions = null)
        {
            return _repos.GetPagedListAsync();
        }
        
    }
}
