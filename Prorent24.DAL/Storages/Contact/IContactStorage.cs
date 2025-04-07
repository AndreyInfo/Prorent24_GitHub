using Prorent24.Entity.Contact;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Contact
{
    public interface IContactStorage : IBaseStorage<ContactEntity>
    {
        Task<List<ContactEntity>> SearchContacts(string likeString);
        Task<IPagedList<ContactEntity>> GetAllByFilter(IQueryable<ContactEntity> queryableEntity, string searchText);

    }
}
