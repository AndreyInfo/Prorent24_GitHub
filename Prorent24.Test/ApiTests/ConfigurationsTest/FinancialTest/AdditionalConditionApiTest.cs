using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Configuration.Financials.AdditionalCondition;

namespace Prorent24.Test.ApiTest.ConfigurationsTest.FinancialTest
{
    public class AdditionalConditionApiTest : BaseTest<AdditionalConditionViewModel>
    {
        public AdditionalConditionApiTest(TestServerFixture fixture) : base(fixture, "configuration/financial/additional_conditions")
        {
            model = new AdditionalConditionViewModel()
            {
                Name = "Test_Name",
                Text = "Test_Text"
            };
        }
    }
}
