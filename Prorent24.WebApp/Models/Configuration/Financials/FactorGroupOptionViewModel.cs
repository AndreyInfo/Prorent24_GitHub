using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Configuration.Financials
{
    public class FactorGroupOptionViewModel
    {
        public int Id { get; set; }

        public int FactorGroupId { get; set; }
        
        public int NumberOfDaysFrom { get; set; }
       
        public int NumberOfDaysTo { get; set; }

        public decimal Factor { get; set; }
    }
}
