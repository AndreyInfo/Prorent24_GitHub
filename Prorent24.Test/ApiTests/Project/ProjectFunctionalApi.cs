using Prorent24.Enum.Project;
using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Project;
    
namespace Prorent24.Test.ApiTests.Project
{
    public class ProjectFunctionalApi : BaseTest<ProjectFunctionViewModel>
    {
        public ProjectFunctionalApi(TestServerFixture fixture, string controllerName) : base(fixture, controllerName)
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
