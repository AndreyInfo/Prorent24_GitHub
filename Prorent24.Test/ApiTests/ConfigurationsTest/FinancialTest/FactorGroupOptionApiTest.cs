using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Configuration.Financials;

namespace Prorent24.Test.ApiTests.FinancialTest
{
   public class FactorGroupOptionApiTest : BaseTest<FactorGroupOptionViewModel>
    {
        public FactorGroupOptionApiTest(TestServerFixture fixture, int factorGroupId) : base(fixture, "configuration/financial/factor_group_options")
        {
            model = new FactorGroupOptionViewModel()
            {
                FactorGroupId = factorGroupId,
                Factor = 0.1M,
                NumberOfDaysTo = 10,
                NumberOfDaysFrom = 5
            };
        }
    }
}

