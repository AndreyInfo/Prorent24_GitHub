using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Invoice
{
    public class InvoiceExcludedDto: BaseDto<int>
    {
        public int InvoiceId { get; set; }
        public InvoiceDto Invoice { get; set; }
        public int InvoiceExcludedId { get; set; }
        public InvoiceDto InvoiceExcluded { get; set; }
    }
}
