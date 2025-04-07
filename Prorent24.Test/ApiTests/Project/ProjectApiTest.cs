using Prorent24.Enum.Project;
using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.Project
{
    class ProjectApiTest : BaseTest<ProjectViewModel>
    {
        public ProjectApiTest(TestServerFixture fixture) : base(fixture, "projects")
        {
            model = new ProjectViewModel()
            {
                Name = "Project",
                Number = "00001",
                Status =  ProjectStatusEnum.Application,
                Reference = "Reference",
                //TypeId = 1,
                Color = "#523424"

            };
        }
    }
}
