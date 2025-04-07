
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Invoice
{
    public class InvoiceLineViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int VatClassId { get; set; }
        public int LedgerId { get; set; }
        public decimal Amount { get; set; }
    }
}
