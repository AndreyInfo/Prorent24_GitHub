using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Invoice
{
    public class InvoiceVATDto
    {
        public decimal? Rate { get; set; }
        public decimal? Price { get; set; }

        public int? VatClassId { get; set; }
        public decimal? VAT { get { return Price * Rate / 100; } }
        public string VATName { get; set; }
    }
}
