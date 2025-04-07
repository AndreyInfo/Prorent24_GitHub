using Prorent24.Entity.Base;
using Prorent24.Entity.General;
using Prorent24.Entity.Tasks;
using Prorent24.Enum.Contact;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Contact
{
    [Table("dbo_contacts")]
    public class ContactEntity : BaseEntity
    {
        #region Data
        public ContactType Type { get; set; }

        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string NameLine { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string BankAccountNumber { get; set; }

        public string Bic { get; set; }

        public string DatabaseNumber { get; set; }

        public int? DefaultContactPersonId { get; set; }

        [ForeignKey("DefaultContactPersonId")]
        public virtual ContactPersonEntity DefaultContact { get; set; }

        public int FolderId { get; set; }

        [ForeignKey("FolderId")]
        public virtual FolderEntity Folder { get; set; }

        #endregion

        public int? VisitingAddressId { get; set; }

        [ForeignKey("VisitingAddressId")]
        public virtual AddressEntity VisitingAddress { get; set; }

        public int? PostalAddressId { get; set; }

        [ForeignKey("PostalAddressId")]
        public virtual AddressEntity PostalAddress { get; set; }

        public int? BillingAddressId { get; set; }

        [ForeignKey("BillingAddressId")]
        public virtual AddressEntity BillingAddress { get; set; }


        #region Detail
        public string Phone2 { get; set; }

        public string Email2 { get; set; }

        public string Website { get; set; }

        public string CocNumber { get; set; }

        public string VatIdentificationNumber { get; set; }

        public string FiscalCode { get; set; }

        public string PurchaseNumber { get; set; }

        public string Warning { get; set; }

        public string SubjectProjNote { get; set; }

        public string ProjNote { get; set; }

        #endregion

        public virtual ICollection<NoteEntity> Notes { get; set; }
        public virtual ICollection<TagBondEntity> Tags { get; set; }
        public virtual ICollection<TaskEntity> Tasks { get; set; }
        public virtual ICollection<FileEntity> Files { get; set; }

        public int? CompanyTypeId { get; set; }
        public string SocialNetworkJson { get; set; }
        public string PhonesJson { get; set; }
        public string CompanyPhonesJson { get; set; }
        public string CompanyShortName { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Ogrn { get; set; }
        public string CheckingAccount { get; set; }
        public string CorrespondentAccount { get; set; }
        public string Bank { get; set; }
        public string EmailsJson { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Specialization { get; set; }
        public bool? IsCompany { get; set; }

        public bool? IsPublic { get; set; }
        // vehicle visible for
        public virtual ICollection<ContactVisibilityCrewMemberEntity> CrewMembers { get; set; }


    }
}
