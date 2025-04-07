using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.CrewMember;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Prorent24.Enum.General;

namespace Prorent24.Test.ApiTests.CrewMemberTest
{
    public class CrewMemberRateApiTest : BaseTest<CrewMemberRateViewModel>
    {
        public CrewMemberRateApiTest(TestServerFixture fixture,string crewMemberId, string controllerName) : base(fixture, controllerName)
        {
            model = new CrewMemberRateViewModel()
            {
                CrewMemberId = crewMemberId,
                DailyRate = 10,
                HourlyRate = 10,
                IsDefaultRate = true,
                Name = "Test rate",
                MaxNumberOfTimeUnit = 120,
                TimeUnit = TimeUnitTypeEnum.Hours
            };
        }
    }
}
