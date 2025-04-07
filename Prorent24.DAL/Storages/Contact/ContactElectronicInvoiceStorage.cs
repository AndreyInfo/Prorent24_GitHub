using Prorent24.Entity.Contact;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Contact
{
    internal class ContactElectronicInvoiceStorage : BaseStorage<ContactElectronicInvoiceEntity>, IContactElectronicInvoiceStorage
    {
        public ContactElectronicInvoiceStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<IPagedList<ContactElectronicInvoiceEntity>> GetAll(List<Predicate<ContactElectronicInvoiceEntity>> conditions = null)
        {
            return await _repos.GetPagedListAsync();
        }

        public async Task<ContactElectronicInvoiceEntity> GetByContactId(int contactId)
        {
            var result = _repos.GetFirstOrDefault(x => x, predicate: c => c.ContactId == contactId);
            return result;
        }
    }
}
