using Prorent24.Test.ApiTest.BaseJsonTest;
using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Configuration.Financials;
using Prorent24.WebApp.Models.Configuration.Financials.Vat;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.ConfigurationsTest.FinancialTest
{
    class FinancialSettingApiTest : BaseJsonTest<FinancialSettingViewModel>
    {
        public FinancialSettingApiTest(TestServerFixture fixture) :base(fixture, "configuration/financial/financial_settings")
        {
            model = new FinancialSettingViewModel()
            {
                CurrencySign = "$",
                DefaultExpirationPeriodQuotationContract = 1,
                DefaultDueTermInvoiceReminders = 2,
                DefaultVatSchemeId = 1,
                VatScheme = new VatSchemeViewModel(),
                DefaultVatClassId = 2,
                VatClass = new VatClassViewModel(),
                DefaultTaxClassCrewFunctionId = 3,
                DefaultTaxClassTransportFunctionId = 4,
                VatClassInsuranceId = 5,
                BankName = "BankName",
                BankAccountNumber = "BankAccountNumber",
                BankNumberBic = "BankNumberBic",
                PrintOnLetterhead = true,
                SendTermsConditionsWithQuotations = true,
                SendTermsConditionsWithContracts = true,
                SendTermsConditionsWithInvoices = true
            };
        }
    }
}
