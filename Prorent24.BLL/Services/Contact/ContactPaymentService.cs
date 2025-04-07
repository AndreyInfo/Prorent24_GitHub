using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Prorent24.BLL.Models.Contact;
using Prorent24.BLL.Transfers.Contact;
using Prorent24.DAL.Storages.Contact;
using Prorent24.Entity.Contact;

namespace Prorent24.BLL.Services.Contact
{
    internal class ContactPaymentService : IContactPaymentService
    {
        private readonly IContactPaymentStorage _contactPaymentStorage;
        public ContactPaymentService(IContactPaymentStorage contactPaymentStorage)
        {
            _contactPaymentStorage = contactPaymentStorage;
        }
        
        public async Task<ContactPaymentDto> GetByContactId(int contactId)
        {
            ContactPaymentEntity entity = await _contactPaymentStorage.GetByContactId(contactId);
            ContactPaymentDto dto = entity?.TransferToDto();
            return dto;
        }

        public  async Task<ContactPaymentDto> Save(ContactPaymentDto dto)
        {
            ContactPaymentEntity dbEntity = await _contactPaymentStorage.GetByContactId(dto.ContactId);
            ContactPaymentEntity transferEntity = dto.TransferToEntity();
            ContactPaymentDto result;
            if (dbEntity != null)
            {
                ContactPaymentEntity entity = await _contactPaymentStorage.Update(transferEntity);
                result = entity.TransferToDto();
            }
            else {
                ContactPaymentEntity entity = await _contactPaymentStorage.Create(transferEntity);
                result = entity.TransferToDto();
            }
            return result;
        }

    }
}
