using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Configuration.Financials.Payment;

namespace Prorent24.Test.ApiTests.FinancialTest
{
    public class PaymentMethodApiTest : BaseTest<PaymentMethodViewModel>
    {
        public PaymentMethodApiTest(TestServerFixture fixture) : base(fixture, "configuration/financial/payment_methods")
        {
            model = new PaymentMethodViewModel()
            {
                AccountingCode = "0008",
                Name = "PaymentMethod"
            };
        }
    }
}
