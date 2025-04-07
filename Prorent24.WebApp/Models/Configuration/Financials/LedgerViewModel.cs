using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Configuration.Financials
{
    public class LedgerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string AccountingCode { get; set; }
        
        public bool IsSystem { get; set; }
    }
}
