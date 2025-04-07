using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Invoice
{
    public class InvoiceLineDto : BaseDto<int>
    {
        public int InvoiceId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int VatClassId { get; set; }
        public string VatClassName { get; set; }
        public int LedgerId { get; set; }
        public string LedgerName { get; set; }
        public decimal Amount { get; set; }
    }
}
