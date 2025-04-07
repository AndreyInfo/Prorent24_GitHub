using Prorent24.Test.ApiTests.CrewMemberTest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Prorent24.Test.ApiTests
{
    public class CrewMembersApiTest
    {
        private TestServerFixture _fixture = new TestServerFixture();

        [Fact]
        public async Task CrewMember_Test()
        {
            CrewMemberApiTest apiCrewMemberTest = new CrewMemberApiTest(_fixture);
            await apiCrewMemberTest.Create();
            await apiCrewMemberTest.Update();
            await apiCrewMemberTest.GetAll();


            CrewMemberRateApiTest apiCrewMemberRateTest = new CrewMemberRateApiTest(_fixture, apiCrewMemberTest.id, $"crew_members/{apiCrewMemberTest.id}/rates");
            await apiCrewMemberRateTest.Create();
            await apiCrewMemberRateTest.Update();
            await apiCrewMemberRateTest.GetAll();

            // Delete with CrewMember - CrewMemberRate Is Default Rate
            //await apiCrewMemberRateTest.Delete();
            await apiCrewMemberTest.Delete();

        }
    }
}
