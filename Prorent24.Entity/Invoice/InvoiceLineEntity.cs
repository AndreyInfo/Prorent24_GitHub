using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration.Financial;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Invoice
{
    [Table("dbo_invoice_lines")]
    public class InvoiceLineEntity: BaseEntity
    {
        public int InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public InvoiceEntity Invoice { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int VatClassId { get; set; }
        [ForeignKey("VatClassId")]
        public virtual VatClassEntity VatClass { get; set; }
        public int LedgerId { get; set; }
        [ForeignKey("LedgerId")]
        public virtual LedgerEntity Ledger { get; set; }
        // Calculated on VatClass and Ledger
        public decimal Amount { get; set; }
    }
}
