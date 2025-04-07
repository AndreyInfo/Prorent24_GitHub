using Prorent24.Test.ApiTests.Subhire;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Prorent24.Test.ApiTests
{
    public class SubhiresApiTest
    {
        private TestServerFixture _fixture = new TestServerFixture();

        [Fact]
        public async Task Subhire_Test()
        {
            SubhireApiTest apiSubhireTest = new SubhireApiTest(_fixture);
            await apiSubhireTest.Create();
            await apiSubhireTest.Update();
            await apiSubhireTest.GetAll();
            await apiSubhireTest.GetById();
            
            SubhireGroupApiTest apigroupTest = new SubhireGroupApiTest(_fixture, $"subhires/{apiSubhireTest.id}/groups");
            await apigroupTest.Create();
            await apigroupTest.Update();
            await apigroupTest.GetEquipmentGroupBySubhire();

            SubhireEquipmentApiTest apiEquipmentTest = new SubhireEquipmentApiTest(_fixture, apigroupTest.id, $"subhires/{apiSubhireTest.id}/equipments");
            await apiEquipmentTest.Create();
            await apiEquipmentTest.Update();
            await apiEquipmentTest.Delete();

            await apigroupTest.Delete();

            await apiSubhireTest.Delete();
        }
    }
}
