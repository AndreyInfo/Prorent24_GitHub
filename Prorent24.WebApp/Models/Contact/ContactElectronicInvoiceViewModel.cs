using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Contact
{
    public class ContactElectronicInvoiceViewModel
    {
        public int Id { get; set; }

        public int ContactId { get; set; }

        public string IdentificationNumber { get; set; }

        public string IdentificationScheme { get; set; }

    }
}
