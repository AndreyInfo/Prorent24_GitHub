using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.EquipmentTest
{
    public class EquipmentPeriodicInspectionApiTest : BaseTest<EquipmentPeriodicInspectionViewModel>
    {
        public EquipmentPeriodicInspectionApiTest(TestServerFixture fixture, string periodicInspectionId, string controllerName) : base(fixture, controllerName)
        {
            model = new EquipmentPeriodicInspectionViewModel()
            {
                // EquipmentId = int.Parse(equipmentId),
                PeriodicInspectionId = int.Parse(periodicInspectionId),
                Active = true
            };
        }
    }
}
