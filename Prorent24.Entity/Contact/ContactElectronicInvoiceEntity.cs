using Prorent24.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Contact
{
    [Table("dbo_contact_electronic_invoices")]
    public class ContactElectronicInvoiceEntity : BaseEntity
    {
        public int ContactId { get; set; }

        [ForeignKey("ContactId")]
        public virtual ContactEntity Contact { get; set; }

        public string IdentificationNumber { get; set; }

        public string IdentificationScheme { get; set; }


    }
}
