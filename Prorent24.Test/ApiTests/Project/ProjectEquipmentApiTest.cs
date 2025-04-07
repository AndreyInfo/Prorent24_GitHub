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
    public class ProjectEquipmentApiTest : BaseTest<ProjectEquipmentViewModel>
    {
        public ProjectEquipmentApiTest(TestServerFixture fixture, string controllerName) : base(fixture, controllerName)
        {
            model = new ProjectEquipmentViewModel()
            {
                Name = "Test",
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
