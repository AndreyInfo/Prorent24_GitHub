using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Contact;
using Prorent24.WebApp.Models.CrewMember;
using Prorent24.WebApp.Models.General.Address;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prorent24.WebApp.Models.Contact
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        #region Data
        [JsonConverter(typeof(StringEnumConverter))]
        public ContactType Type { get; set; }

        public bool? IsCompany { get; set; }

        public int? CompanyTypeId { get; set; }

        public List<SocialNetworkViewModel> SocialNetworks { get; set; }
        public List<string> Phones { get; set; }
        public List<string> Emails { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Specialization { get; set; }

        public List<string> CompanyPhones { get; set; }
        public string CompanyShortName { get; set; }
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Длина строки должна быть 10 символов")]
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Ogrn { get; set; }
        public string CheckingAccount { get; set; }
        public string CorrespondentAccount { get; set; }
        public string Bank { get; set; }

        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string MiddleName { get; set; }

        //public string NameLine { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public string Bic { get; set; }
        public string DatabaseNumber { get; set; }
        public int? DefaultContactPersonId { get; set; }
        public int FolderId { get; set; }
        public string FolderName { get; set; }

        #endregion
        public int? VisitingAddressId { get; set; }
        public AddressViewModel VisitingAddress { get; set; } //фактический
        public int? PostalAddressId { get; set; }
        public AddressViewModel PostalAddress { get; set; } //юридический
                                                            //public int? BillingAddressId { get; set; }
                                                            //public AddressViewModel BillingAddress { get; set; }
        public bool IsPublic { get; set; }
        public List<CrewMemberShortViewModel> CrewMembers { get; set; }
        #region Details
        //public string Phone2 { get; set; }
        //public string Email2 { get; set; }
        public string Website { get; set; }
        public string CocNumber { get; set; }
        public string VatIdentificationNumber { get; set; }
        public string FiscalCode { get; set; }
        public string PurchaseNumber { get; set; }
        public string Warning { get; set; }
        public string SubjectProjNote { get; set; }
        public string ProjNote { get; set; }

        public string Name { get; set; }

        #endregion
    }
}
