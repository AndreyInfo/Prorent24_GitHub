using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Configuration.Financials;

namespace Prorent24.Test.ApiTests.FinancialTest
{
    public class LedgerApiTest : BaseTest<LedgerViewModel>
    {
        public LedgerApiTest(TestServerFixture fixture) : base(fixture, "configuration/financial/ledgers")
        {
            model = new LedgerViewModel()
            {
                AccountingCode = "00001",
                IsSystem = true,
                Name = "LedgerView"
            };
        }
    }
}
