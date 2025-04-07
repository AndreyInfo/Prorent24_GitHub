using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.CustomerCommunication.Salutation
{
    internal class SalutationStorage : BaseStorage<SalutationEntity>, ISalutationStorage
    {
        public SalutationStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// Get list Salutations
        /// </summary>
        /// <returns></returns>
        public Task<IPagedList<SalutationEntity>> GetAll(List<Predicate<SalutationEntity>> conditions = null)
        {
            return _repos.GetPagedListAsync(x => new SalutationEntity()
            {
                Id = x.Id,
                Name = x.Name,
                View = x.View,
                CreationUserId = x.CreationUserId,
                CreationDate = x.CreationDate,
                LastModifiedDate = x.LastModifiedDate
            });
        }
    }
}
