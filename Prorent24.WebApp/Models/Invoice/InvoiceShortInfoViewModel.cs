using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Invoice
{
    public class InvoiceShortInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
