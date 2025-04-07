using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.EquipmentTest
{
    public class EquipmentAccessoryApiTest : BaseTest<EquipmentAccessoryViewModel>
    {
        public EquipmentAccessoryApiTest(TestServerFixture fixture, string equipmentId, string equipmentAccessoryId, string controllerName) : base(fixture, controllerName)
        {
            model = new EquipmentAccessoryViewModel()
            {
                EquipmentId = int.Parse(equipmentId),
                AccessoryId = int.Parse(equipmentAccessoryId),
                Quantity = 10,
                Free = false,
                SkipIfAlreadyPresent = true,
                Automatic = true
            };
        }
    }
}
