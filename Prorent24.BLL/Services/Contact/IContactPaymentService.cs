using Prorent24.BLL.Models.Contact;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Contact
{
    public interface IContactPaymentService
    {
        /// <summary>
        /// Create ContactPayment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ContactPaymentDto> Save(ContactPaymentDto model);

        /// <summary>
        /// Get by ContactId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ContactPaymentDto> GetByContactId(int id);
    }
}
