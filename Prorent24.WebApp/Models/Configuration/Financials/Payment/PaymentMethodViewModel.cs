using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Configuration.Financials.Payment
{
    public class PaymentMethodViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AccountingCode { get; set; }
    }
}
