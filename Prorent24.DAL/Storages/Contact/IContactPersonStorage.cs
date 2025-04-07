using Prorent24.Entity.Contact;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Contact
{
    public interface IContactPersonStorage:IBaseStorage<ContactPersonEntity>
    {
        Task<IPagedList<ContactPersonEntity>> GetPagedList(int contactId);

        Task<List<ContactPersonEntity>> SearchContactPersons(string likeString);
    }
}
