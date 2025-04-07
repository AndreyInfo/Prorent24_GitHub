using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.EquipmentTest
{
    public class EquipmentAlternativeApiTest : BaseTest<EquipmentAlternativeViewModel>
    {
        public EquipmentAlternativeApiTest(TestServerFixture fixture, string equipmentId, string equipmentAlternativeId, string controllerName) : base(fixture, controllerName)
        {
            model = new EquipmentAlternativeViewModel()
            {
                EquipmentId = int.Parse(equipmentId),
                AlternativeId = int.Parse(equipmentAlternativeId)
            };
        }
    }
}
