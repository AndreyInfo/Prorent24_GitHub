using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Contact
{
    public class ContactElectronicInvoiceDto : BaseDto<int>
    {
        public int ContactId { get; set; }

        public string IdentificationNumber { get; set; }

        public string IdentificationScheme { get; set; }


    }
}
