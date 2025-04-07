using Prorent24.BLL.Models.General.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Configuration.AccountConfiguration
{
    public class CompanyDetailsDto: LocationDto
    {
        public string ContactPersonName { get; set; }
        public string ContactPersonEmail { get; set; }

        public string CompanyName { get; set; }
        public string InvoiceEmail { get; set; }
        public ContacInfoDto DirectorInfo { get; set; }
        public ContacInfoDto AccountantInfo { get; set; }
       
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Ogrn { get; set; }
        public string Okpo { get; set; }
        public string CheckingAccount { get; set; }
        public string CorrespondentAccount { get; set; }
        public string Bank { get; set; }
        public string Bic { get; set; }
        public string Website { get; set; }
        public string ShortName { get; set; }
        public string BackgroundImage { get; set; }
        public string LogoImage { get; set; }
        public List<string> Phones { get; set; }
        public CompanyOfficeDto AdditionalOffice { get; set; }
    }
}
