using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.Project
{
    public class ProjectFunctionGroupApi : BaseTest<ProjectFunctionGroupViewModel>
    {
        public ProjectFunctionGroupApi(TestServerFixture fixture, string controllerName) : base(fixture, controllerName)
        {
            model = new ProjectFunctionGroupViewModel()
            {
                Name = "Test"
            };
        }
    }
}
