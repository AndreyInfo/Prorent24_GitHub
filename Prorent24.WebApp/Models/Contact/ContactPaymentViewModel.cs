using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Configuration.ConfigurationRoles;

namespace Prorent24.WebApp.Models.Contact
{
    public class ContactPaymentViewModel
    {
        public int Id { get; set; }
        public int ContactId { get; set; }

        #region PaymentInformation
        public int InvoiceMomentId { get; set; }

        public int PaymentConditionId { get; set; }

        public int VatSchemeId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
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
