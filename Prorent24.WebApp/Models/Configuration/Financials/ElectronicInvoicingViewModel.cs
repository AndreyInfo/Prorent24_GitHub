using Prorent24.WebApp.Models.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Configuration.Financials
{
    public class ElectronicInvoicingViewModel
    {
        public string IdentificationNumber { get; set; }

        public string IdentificationScheme { get; set; }

        public int Currency { get; set; } //DirectoryId

        public virtual DirectoryViewModel Directory { get; set; }
    }
}
