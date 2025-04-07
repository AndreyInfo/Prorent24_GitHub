using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Configuration.CustomerCommunication
{
    public class CustomerCommmunicationDto
    {
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string EmailFrom { get; set; }
        public string EmailBodyTemplate { get; set; }
    }
}
