using Prorent24.Test.ApiTest.BaseJsonTest;
using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Configuration.Financials;

namespace Prorent24.Test.ApiTests.FinancialTest
{
    class ElectronicInvoicingApiTest : BaseJsonTest<ElectronicInvoicingViewModel>
    {
        public ElectronicInvoicingApiTest(TestServerFixture fixture) : base(fixture, "configuration/financial/electronic_invoicings")
        {
            model = new ElectronicInvoicingViewModel()
            {
                Currency = 0,
                IdentificationNumber = "IdentificationNumber",
                IdentificationScheme = "IdentificationScheme"
            };
        }
    }
}