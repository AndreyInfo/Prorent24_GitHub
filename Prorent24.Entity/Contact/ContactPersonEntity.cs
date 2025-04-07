using Prorent24.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Contact
{
    [Table("dbo_contact_persons")]
    public class ContactPersonEntity : BaseEntity
    {
        public int ContactId { get; set; }

        [ForeignKey("ContactId")]
        public virtual ContactEntity Contact { get; set; }

        #region Data
        public int? SalutationId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

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

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        #endregion

    }
}
