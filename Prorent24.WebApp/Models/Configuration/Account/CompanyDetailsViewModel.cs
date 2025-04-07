using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Configuration.Account
{
    public class CompanyDetailsViewModel
    {
        public string ContactPersonName { get; set; }
        public string ContactPersonEmail { get; set; }

        public string CompanyName { get; set; }
        public string InvoiceEmail { get; set; }

        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public int CountryId { get; set; }
        public ContactInfoViewModel AccountantInfo { get; set; }
        public ContactInfoViewModel DirectorInfo { get; set; }

        // public string LoginScreenImage { get; set; }

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

        public CompanyOfficeViewModel AdditionalOffice { get; set; }

    }

    public class CompanyOfficeViewModel
    {
        public int CountryId { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
       
    }
}
