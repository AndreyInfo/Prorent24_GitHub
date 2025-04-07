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
    public class SubhireEquipmentApiTest : BaseTest<SubhireEquipmentViewModel>
    {
        public SubhireEquipmentApiTest(TestServerFixture fixture, string groupId, string controllerName) : base(fixture, controllerName)
        {
            model = new SubhireEquipmentViewModel()
            {
                Name = "Test",
                GroupId = int.Parse(groupId),
                Quantity = 10,
                Price = 10,
                Discount = 10,
                Factor = 1,
                Remark = "Remark",
                TotalPrice = 11

            };
        }

    }
}
