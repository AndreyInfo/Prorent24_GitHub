using Prorent24.Test.ApiTests.FinancialTest;
using Prorent24.Test.ApiTests.Project;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Prorent24.Test.ApiTests
{
    public class ProjectsApiTest
    {
        private TestServerFixture _fixture = new TestServerFixture();

        [Fact]
        public async Task Project_Test()
        {
            ProjectApiTest apiProjectTest = new ProjectApiTest(_fixture);
            await apiProjectTest.Create();
            await apiProjectTest.Update();
            await apiProjectTest.GetAll();
            await apiProjectTest.GetById();
            await apiProjectTest.GetDetails();

            ProjectTimeApiTest apiTimeTest = new ProjectTimeApiTest(_fixture, $"projects/{apiProjectTest.id}/times");
            await apiTimeTest.Create();
            await apiTimeTest.Update();
            await apiTimeTest.Delete();

            ProjectGroupApiTest apiGroupTest = new ProjectGroupApiTest(_fixture, $"projects/{apiProjectTest.id}/groups");
            await apiGroupTest.Create();
            await apiGroupTest.Update();
            await apiGroupTest.GetEquipmentGroupByProject();

            ProjectGroupApiTest apiEquipmentTest = new ProjectGroupApiTest(_fixture, $"projects/{apiProjectTest.id}/groups/{apiGroupTest.id}/equipment");
            await apiEquipmentTest.Create();
            await apiEquipmentTest.Update();
            await apiEquipmentTest.Delete();

            await apiGroupTest.Delete();

            VatClassApiTest apiVatClassTest = new VatClassApiTest(_fixture);
            await apiVatClassTest.Create();

            ProjectAdditionalCostApi projectAdditionalCostApi = new ProjectAdditionalCostApi(_fixture, $"projects/{apiProjectTest.id}/additional_costs", int.Parse(apiVatClassTest.id));
            await apiEquipmentTest.Create();
            await apiEquipmentTest.Update();
            await apiEquipmentTest.Delete();

            await apiVatClassTest.Delete();


            //проверка добавления групп функций, функций


            await apiProjectTest.Delete();
        }
    }
}

