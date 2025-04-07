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
    internal class ContactElectronicInvoiceService : IContactElectronicInvoiceService
    {
        private readonly IContactElectronicInvoiceStorage _contactElectronicInvoiceStorage;
        public ContactElectronicInvoiceService(IContactElectronicInvoiceStorage contactElectronicInvoiceStorage)
        {
            _contactElectronicInvoiceStorage = contactElectronicInvoiceStorage;
        }
        public async Task<ContactElectronicInvoiceDto> GetByContactId(int id)
        {
            ContactElectronicInvoiceEntity entity = await _contactElectronicInvoiceStorage.GetByContactId(id);
            ContactElectronicInvoiceDto dto = entity?.TransferToDto();
            return dto;
        }

        public async Task<ContactElectronicInvoiceDto> Save(ContactElectronicInvoiceDto model)
        {
            ContactElectronicInvoiceEntity dbEntity = await _contactElectronicInvoiceStorage.GetByContactId(model.ContactId);
            ContactElectronicInvoiceEntity transferEntity = model.TransferToEntity();
            ContactElectronicInvoiceDto result;
            if (dbEntity != null)
            {
                ContactElectronicInvoiceEntity entity = await _contactElectronicInvoiceStorage.Update(transferEntity);
                result = entity.TransferToDto();
            }
            else
            {
                ContactElectronicInvoiceEntity entity = await _contactElectronicInvoiceStorage.Create(transferEntity);
                result = entity.TransferToDto();
            }
            return result;
        }
        
    }
}
