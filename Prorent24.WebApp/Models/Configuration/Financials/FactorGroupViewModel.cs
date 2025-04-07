using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Configuration.Financials
{
    public class FactorGroupViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsSystem { get; set; }

        public List<FactorGroupOptionViewModel> FactorGroupOptions { get; set; }
    }
}
