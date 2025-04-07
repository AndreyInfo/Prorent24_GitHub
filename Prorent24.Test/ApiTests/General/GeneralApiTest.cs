using Prorent24.Test.ApiTests.General;
using System.Threading.Tasks;
using Xunit;
using Prorent24.Test.ApiTests.CrewMemberTest;


namespace Prorent24.Test.ApiTests
{
    public class GeneralApiTest
    {
        private TestServerFixture _fixture = new TestServerFixture();

        [Fact]
        public async Task Filter_Test()
        {
            CrewMemberApiTest crewMemberApiTest = new CrewMemberApiTest(_fixture);
            await crewMemberApiTest.Create();

            FilterApiTest apiTest = new FilterApiTest(_fixture, crewMemberApiTest.id);
            await apiTest.GetListFilters();
            await apiTest.SaveFilter();
            await apiTest.DeleteSavedFilter();
        }
    }
}
