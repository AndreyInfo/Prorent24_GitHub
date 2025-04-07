using Prorent24.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Contact
{
    public class ContactPersonDto : BaseDto<int>
    {
        public int ContactId { get; set; }

        #region Data
        public int? SalutationId { get; set; }

        [IncludeToGrid(Order = 5)]
        public string FirstName { get; set; }

        [IncludeToGrid(Order = 6)]
        public string LastName { get; set; }

        [IncludeToGrid(Order = 7)]
        public string MiddleName { get; set; }

        public string Function { get; set; }

        public string Salutation { get; set; }

        #endregion

        #region Detail

        public string Address { get; set; }

        public string HouseNumber { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Province { get; set; }

        public int? CountryId { get; set; }
        
        [IncludeToGrid(Order = 8)]
        public string Mobile { get; set; }

        [IncludeToGrid(Order = 9)]
        public string Phone { get; set; }

        [IncludeToGrid(Order = 10)]
        public string Email { get; set; }

        #endregion
    }
}
