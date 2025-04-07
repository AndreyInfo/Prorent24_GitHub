using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Configuration.Financials;

namespace Prorent24.Test.ApiTests.FinancialTest
{
   public class FactorGroupApiTest : BaseTest<FactorGroupViewModel>
    {
        public FactorGroupApiTest(TestServerFixture fixture) : base(fixture, "configuration/financial/factor_groups")
        {
            model = new FactorGroupViewModel()
            {
                IsSystem = true,
                Name = "FactorGroup_Name"
            };
        }
    }
}

