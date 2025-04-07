using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Configuration.Financials
{
    public class InvoiceMomentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Text { get; set; }

        public decimal AfterAgreement { get; set; }

        public decimal BeforeFirstDay { get; set; }

        public decimal Afterwards { get; set; }
    }
}
