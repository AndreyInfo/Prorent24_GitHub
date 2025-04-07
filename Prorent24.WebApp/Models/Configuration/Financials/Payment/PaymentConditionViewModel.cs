using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Configuration.Financials.Payment
{
    public class PaymentConditionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TextOnInvoice { get; set; }

        public string AccountingCode { get; set; }

        public int TermInDays { get; set; }

        public int? PaymentMethodId { get; set; }

        public PaymentMethodViewModel PaymentMethod { get; set; }
    }
}
