using Prorent24.Enum.CrewMember;
using Prorent24.WebApp.Models.General.Address;
using Prorent24.WebApp.Models.General.Note;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prorent24.WebApp.Models.CrewMember
{
    public class CrewMemberViewModel : AddressViewModel
    {
        public string Id { get; set; }
        public string ColorAppointments { get; set; }
        public string Description { get; set; }
        public bool? IsSystemUser { get; set; }
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        public int? FolderId { get; set; }
        public string FolderName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }

        // Details
        public AvailabilityCrewMemberTypeEnum Availability { get; set; }
        public bool DisplayInPlanner { get; set; }
        public string DrivingLicense { get; set; }
        public string EmergencyContact { get; set; }
        public string ReceiveEmails { get; set; }

        // Login information to AspNetUsers
        public bool Active { get; set; }
        public bool IsPowerUser { get; set; }
        public DateTime LastLogin { get; set; }
        public string UserRoleId { get; set; }
        public string Username { get; set; }
        //public int? AddressId { get; set; }
        public int? DefaultRateId { get; set; }

        // Administrative
        public string BankAccountNumber { get; set; }
        public DateTime? Birthdate { get; set; }
        public string CoCNumber { get; set; }
        public string CompanyName { get; set; }
        public DateTime? ContractDate { get; set; }
        public int? HoursInContract { get; set; }
        public string PassportNumber { get; set; }
        public string PassportCompany { get; set; }
        public DateTime? PassportDate { get; set; }


        // VAT identification number
        public string VAT { get; set; }

        public NoteViewModel Note { get; set; }

        public List<SocialNetworkViewModel> SocialNetworks { get; set; }

        public string ContractDateView { get; set; }

        public string BirthdateView { get; set; }



    }
}
