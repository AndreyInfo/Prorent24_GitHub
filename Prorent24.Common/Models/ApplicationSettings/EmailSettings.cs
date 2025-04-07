using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Common.Models.ApplicationSettings
{
    public class EmailSettings
    {
        public string MailAddress { get; set; }
        public string MailPassword { get; set; }
        public string SmtpAddress { get; set; }
        public int SmtpPort { get; set; }
    }
}
