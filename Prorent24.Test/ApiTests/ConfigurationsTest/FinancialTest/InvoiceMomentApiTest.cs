using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Configuration.Financials;

namespace Prorent24.Test.ApiTests.FinancialTest
{
    public class InvoiceMomentApiTest : BaseTest<InvoiceMomentViewModel>
    {
        public InvoiceMomentApiTest(TestServerFixture fixture) : base(fixture, "configuration/financial/invoice_moments")
        {
            model = new InvoiceMomentViewModel()
            {
                AfterAgreement = 200,
                Afterwards = 50,
                BeforeFirstDay = 25,
                Name = "InvoiceMoment",
                Text = "Text"
            };
        }
    }
}