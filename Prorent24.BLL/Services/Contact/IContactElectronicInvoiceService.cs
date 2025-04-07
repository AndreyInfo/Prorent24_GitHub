using Prorent24.BLL.Models.Contact;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Contact
{
    public interface IContactElectronicInvoiceService
    {
        /// <summary>
        /// Create ContactElectronicInvoice
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ContactElectronicInvoiceDto> Save(ContactElectronicInvoiceDto model);

        /// <summary>
        /// Get by ContactId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ContactElectronicInvoiceDto> GetByContactId(int id);
    }
}
