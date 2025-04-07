using Prorent24.Common.Attributes;

namespace Prorent24.BLL.Models.Configuration.Financial
{
    public class FinancialSettingDto
    {
        #region Financial

        [IncludeToGrid(Order = 5)]
        public string CurrencySign { get; set; }

        [IncludeToGrid(Order = 6)]
        public int DefaultExpirationPeriodQuotationContract { get; set; }

        [IncludeToGrid(Order = 7)]
        public int DefaultDueTermInvoiceReminders { get; set; }

        [IncludeToGrid(Order = 8, IsEntity = true, EntityKey = "VatScheme")]
        public int DefaultVatSchemeId { get; set; }

        public VatSchemeDto VatScheme { get; set; }

        [IncludeToGrid(Order = 9 ,IsEntity = true, EntityKey = "VatClass")]
        public int DefaultVatClassId { get; set; }

        public VatClassDto VatClass { get; set; }

        [IncludeToGrid(Order = 10)]
        public int DefaultTaxClassCrewFunctionId { get; set; }

        [IncludeToGrid(Order = 11)]
        public int DefaultTaxClassTransportFunctionId { get; set; }

        [IncludeToGrid(Order = 12)]
        public int VatClassInsuranceId { get; set; }

        public int? CompanyTypeId { get; set; }

        #endregion

        #region BankDetail

        [IncludeToGrid(Order = 13)]
        public string BankName { get; set; }

        [IncludeToGrid(Order = 14)]
        public string BankAccountNumber { get; set; }

        [IncludeToGrid(Order = 15)]
        public string BankNumberBic { get; set; }

        #endregion

        #region Slips

        [IncludeToGrid(Order = 16)]
        public bool PrintOnLetterhead { get; set; }

        [IncludeToGrid(Order = 17)]
        public bool SendTermsConditionsWithQuotations { get; set; }

        [IncludeToGrid(Order = 18)]
        public bool SendTermsConditionsWithContracts { get; set; }

        [IncludeToGrid(Order = 19)]
        public bool SendTermsConditionsWithInvoices { get; set; }

        #endregion
    }
}