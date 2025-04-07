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
    public class ProjectTimeApiTest : BaseTest<ProjectTimeViewModel>
    {
        public ProjectTimeApiTest(TestServerFixture fixture, string controllerName) : base(fixture, controllerName)
        {
            model = new ProjectTimeViewModel()
            {
                Name = "Test",
                From = DateTime.Now,
                Until = DateTime.Now,
                DisplayQuotation = false,
                DisplayContract = true,
                DisplayPackSlip = false,
                DefaultPlanPeriod = true,
                DefaultUsagePeriod = true

            };
        }

    }
}
