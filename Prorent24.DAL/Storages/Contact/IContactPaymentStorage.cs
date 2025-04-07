using Prorent24.Entity.Contact;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Contact
{
    public interface IContactPaymentStorage : IBaseStorage<ContactPaymentEntity>
    {
        /// <summary>
        /// Get by ContactId
        /// </summary>
        /// <returns></returns>
        Task<ContactPaymentEntity> GetByContactId(int contactId);
        
    }
}
