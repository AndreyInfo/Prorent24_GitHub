using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Configuration.CustomerCommunication
{
    public class CustomerCommmunicationViewModel
    {
        public string CompanyName { get; set; } = "";
        public string Email { get; set; } = "";
        public string EmailFrom { get; set; } = "";
        public string EmailBodyTemplate { get; set; } = "";
    }
}
