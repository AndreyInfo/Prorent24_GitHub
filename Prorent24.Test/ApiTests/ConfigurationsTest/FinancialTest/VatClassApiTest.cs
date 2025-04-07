using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Configuration.Financials.Vat;

namespace Prorent24.Test.ApiTests.FinancialTest
{
    public class VatClassApiTest : BaseTest<VatClassViewModel>
    {
        public VatClassApiTest(TestServerFixture fixture) : base(fixture, "configuration/financial/vat_classes")
        {
            model = new VatClassViewModel()
            {
                Name = "VatClass"
            };
        }
    }
}