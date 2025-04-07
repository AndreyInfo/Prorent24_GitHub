using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Contact
{
    [Table("dbo_contact_payments")]
    public class ContactPaymentEntity : BaseEntity
    {
        public int ContactId { get; set; }

        [ForeignKey("ContactId")]
        public virtual ContactEntity Contact { get; set; }

        #region PaymentInformation
        public int InvoiceMomentId { get; set; }

        [ForeignKey("InvoiceMomentId")]
        public virtual InvoiceMomentEntity InvoiceMoment { get; set; }

        public int PaymentConditionId { get; set; }

        [ForeignKey("PaymentConditionId")]
        public virtual PaymentConditionEntity PaymentCondition { get; set; }

        public int VatSchemeId { get; set; }

        [ForeignKey("VatSchemeId")]
        public virtual VatSchemeEntity VatScheme { get; set; }

        public BooleanSelectPermissionEnum ContactInsurancePercentage { get; set; }

        public decimal InsurancePercentage { get; set; }
        #endregion

        #region DiscountGroup

        public decimal Rental { get; set; }

        public decimal Sales { get; set; }
        #endregion

        #region ProjectDiscount
        public decimal DiscountRentalEquipment { get; set; }

        public decimal DiscountSaleEquipment { get; set; }

        public decimal CrewDiscount { get; set; }

        public decimal TransportDiscount { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal SubhireDiscount { get; set; }
        #endregion
    }
}
