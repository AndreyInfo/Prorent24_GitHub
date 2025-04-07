using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Contact;
using Prorent24.WebApp.Models.General.Address;
using Prorent24.WebApp.Models.Project;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.Test.ApiTests.Project
{
    public class ProjectGroupApiTest : BaseTest<ProjectEquipmentGroupViewModel>
    {
        public ProjectGroupApiTest(TestServerFixture fixture, string controllerName) : base(fixture, controllerName)
        {
            model = new ProjectEquipmentGroupViewModel()
            {
                Name = "Test",
                StartPlanPeriod = DateTime.Now,
                EndPlanPeriod = DateTime.Now,
                StartUsePeriod = DateTime.Now,
                EndUsePeriod = DateTime.Now
            };
        }

        public async Task GetEquipmentGroupByProject()
        {
            HttpResponseMessage response = await _fixture.Client.GetAsync(_endPoint);
            response.EnsureSuccessStatusCode();
        }
    }
}
