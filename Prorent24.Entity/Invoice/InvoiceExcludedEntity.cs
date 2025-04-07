using Prorent24.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Invoice
{
    [Table("dbo_invoice_excluded")]
    public class InvoiceExcludedEntity: BaseEntity
    {
        public int InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public virtual InvoiceEntity Invoice { get; set; }
        public int InvoiceExcludedId { get; set; }
        [ForeignKey("InvoiceExludedId")]
        public virtual InvoiceEntity InvoiceExluded { get; set; }
    }
}
