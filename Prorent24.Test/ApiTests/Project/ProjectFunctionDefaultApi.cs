using Prorent24.Enum.Project;
using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.Project
{
    public class ProjectFunctionDefaultApi : BaseTest<ProjectFunctionViewModel>
    {
        public ProjectFunctionDefaultApi(TestServerFixture fixture, string controllerName) : base(fixture, controllerName)
        {
            model = new ProjectFunctionViewModel()
            {
                ExternalName = "ExternalName",
                InternalName = "InternalName",
                Type = ProjectFunctionTypeEnum.Crew
            };
        }
    }
}
