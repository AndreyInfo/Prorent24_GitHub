using Prorent24.Entity.Contact;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Contact
{
    internal class ContactPaymentStorage : BaseStorage<ContactPaymentEntity>, IContactPaymentStorage
    {
        public ContactPaymentStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<IPagedList<ContactPaymentEntity>> GetAll(List<Predicate<ContactPaymentEntity>> conditions = null)
        {
            return await _repos.GetPagedListAsync();
        }

        public async Task<ContactPaymentEntity> GetByContactId(int contactId)
        {
            var result = _repos.GetFirstOrDefault(x => x, predicate: c => c.ContactId == contactId);
            return result;
        }
    }
}
