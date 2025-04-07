using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Configuration.Financials.Payment;

namespace Prorent24.Test.ApiTests.FinancialTest
{
    public class PaymentConditionApiTest : BaseTest<PaymentConditionViewModel>
    {
        public PaymentConditionApiTest(TestServerFixture fixture,int paymentMethoId) : base(fixture, "configuration/financial/payment_conditions")
        {
            model = new PaymentConditionViewModel()
            {
                PaymentMethodId = paymentMethoId,
                PaymentMethod = new PaymentMethodViewModel()
                {
                    AccountingCode = "0008",
                    Name = "0008"
                },
                AccountingCode = "0008",
                Name = "PaymentMethod",
                TermInDays = 5,
                TextOnInvoice = "TextOnInvoice"
            };
        }
    }
}