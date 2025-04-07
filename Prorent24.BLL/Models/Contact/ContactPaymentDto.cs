using Prorent24.BLL.Models.Configuration;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Contact
{
    public class ContactPaymentDto:BaseDto<int>
    {
        public int ContactId { get; set; }
        
        #region PaymentInformation
        public int InvoiceMomentId { get; set; }
        
        public virtual InvoiceMomentDto InvoiceMoment { get; set; }

        public int PaymentConditionId { get; set; }
        
        public virtual PaymentConditionDto PaymentCondition { get; set; }

        public int VatSchemeId { get; set; }

        
        public virtual VatSchemeDto VatScheme { get; set; }

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
