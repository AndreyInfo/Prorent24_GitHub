using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.CustomerCommunication.Letterhead
{
    internal class LetterheadStorage : BaseStorage<LetterheadEntity>, ILetterheadStorage
    {
        public LetterheadStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// Get list Letterheads
        /// </summary>
        /// <returns></returns>
        public Task<IPagedList<LetterheadEntity>> GetAll(List<Predicate<LetterheadEntity>> conditions = null)
        {
            return _repos.GetPagedListAsync(x => new LetterheadEntity()
            {
                Id = x.Id,
                Name = x.Name,
                CreationUserId = x.CreationUserId,
                CreationDate = x.CreationDate,
                LastModifiedDate = x.LastModifiedDate
            });
        }
    }
}
