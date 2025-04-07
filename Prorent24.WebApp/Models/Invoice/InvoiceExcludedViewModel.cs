using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Invoice
{
    public class InvoiceExcludedViewModel
    {
        public int Id { get; set; }
        // public int InvoiceId { get; set; }
        public int ExcludedInvoiceId { get; set; }
        public InvoiceShortInfoViewModel ExcludedInvoice { get; set;  }
    }
}
