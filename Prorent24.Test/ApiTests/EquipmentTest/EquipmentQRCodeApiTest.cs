using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.EquipmentTest
{
    public class EquipmentQRCodeApiTest : BaseTest<EquipmentQRCodeViewModel>
    {
        public EquipmentQRCodeApiTest(TestServerFixture fixture, string controllerName) : base(fixture, controllerName)
        {
            model = new EquipmentQRCodeViewModel()
            {
                Code = "Code1",
                Image = "Image"
            };
        }
    }
}
