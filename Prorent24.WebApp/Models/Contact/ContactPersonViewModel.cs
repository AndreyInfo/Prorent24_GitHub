using Prorent24.WebApp.Models.General.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Contact
{
    public class ContactPersonViewModel
    {
        public int Id { get; set; }
        public int ContactId { get; set; }

        #region Data
        public int? SalutationId { get; set; }
        public string Salutation { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Function { get; set; }

        #endregion

        #region Detail
        public AddressViewModel Address { get; set; }

        //public string Address { get; set; }

        //public string HouseNumber { get; set; }

        //public string PostalCode { get; set; }

        //public string City { get; set; }

        //public string State { get; set; }

        //public string Province { get; set; }

        //public int? CountryId { get; set; }

        public string Mobile { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
        public string CompanyName { get; set; }

        #endregion
    }
}
