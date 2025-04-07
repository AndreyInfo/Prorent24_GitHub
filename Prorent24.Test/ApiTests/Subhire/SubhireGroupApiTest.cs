using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Contact;
using Prorent24.WebApp.Models.General.Address;
using Prorent24.WebApp.Models.Subhire;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.Test.ApiTests.Subhire
{
    public class SubhireGroupApiTest : BaseTest<SubhireEquipmentGroupViewModel>
    {
        public SubhireGroupApiTest(TestServerFixture fixture, string controllerName) : base(fixture, controllerName)
        {
            model = new SubhireEquipmentGroupViewModel()
            {
                Name = "Test"
                
            };
        }

        public async Task GetEquipmentGroupBySubhire()
        {
            HttpResponseMessage response = await _fixture.Client.GetAsync(_endPoint);
            response.EnsureSuccessStatusCode();
        }
    }
}
