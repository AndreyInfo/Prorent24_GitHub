using Prorent24.WebApp.Models.Configuration.Financials.Vat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Configuration.Financials
{
    public class FinancialSettingViewModel
    {
        #region Financial
        public string CurrencySign { get; set; }
        
        public int DefaultExpirationPeriodQuotationContract { get; set; }
        
        public int DefaultDueTermInvoiceReminders { get; set; }
        
        public int DefaultVatSchemeId { get; set; }

        public VatSchemeViewModel VatScheme { get; set; }
        
        public int DefaultVatClassId { get; set; }

        public VatClassViewModel VatClass { get; set; }
        
        public int DefaultTaxClassCrewFunctionId { get; set; }
        
        public int DefaultTaxClassTransportFunctionId { get; set; }
        
        public int VatClassInsuranceId { get; set; }

        public int? CompanyTypeId { get; set; }

        #endregion

        #region BankDetail

        public string BankName { get; set; }
        
        public string BankAccountNumber { get; set; }
        
        public string BankNumberBic { get; set; }

        #endregion

        #region Slips
        
        public bool PrintOnLetterhead { get; set; }
        
        public bool SendTermsConditionsWithQuotations { get; set; }
        
        public bool SendTermsConditionsWithContracts { get; set; }
        
        public bool SendTermsConditionsWithInvoices { get; set; }

        #endregion
    }
}
