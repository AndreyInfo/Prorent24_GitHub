using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Configuration.Financials;

namespace Prorent24.Test.ApiTests.FinancialTest
{
    public class DiscountGroupApiTest : BaseTest<DiscountGroupViewModel>
    {
        public DiscountGroupApiTest(TestServerFixture fixture) : base(fixture, "configuration/financial/discount_groups")
        {
            model = new DiscountGroupViewModel()
            {
                Name = "Test_Name"
            };
        }
    }
}