using Prorent24.BLL.Models.Contact;
using Prorent24.WebApp.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Contact
{
    public static class ContactElectronicInvoiceTransfer
    {
        /// <summary>
        /// Transfer from ContactElectronicInvoiceDto to ContactElectronicInvoiceViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ContactElectronicInvoiceDto TransferToDto(this ContactElectronicInvoiceViewModel model)
        {
            ContactElectronicInvoiceDto ContactElectronicInvoiceDto = new ContactElectronicInvoiceDto()
            {
                Id = model.Id,
                ContactId = model.ContactId,
                IdentificationNumber = model.IdentificationNumber,
                IdentificationScheme = model.IdentificationScheme,
            };

            return ContactElectronicInvoiceDto;
        }

        /// <summary>
        /// Transfer from ContactElectronicInvoiceViewModel to ContactElectronicInvoiceDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ContactElectronicInvoiceViewModel TransferToViewModel(this ContactElectronicInvoiceDto dto)
        {
            ContactElectronicInvoiceViewModel ContactElectronicInvoiceViewModel = new ContactElectronicInvoiceViewModel();

            if (dto != null)
            {
                ContactElectronicInvoiceViewModel = new ContactElectronicInvoiceViewModel()
                {
                    Id = dto.Id,
                    ContactId = dto.ContactId,
                    IdentificationNumber = dto.IdentificationNumber,
                    IdentificationScheme = dto.IdentificationScheme,

                };
            }
        
            return ContactElectronicInvoiceViewModel;
        }
    }
}
