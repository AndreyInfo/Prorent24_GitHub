using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Maintenance.Inspection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.MaintenanceTest
{
    public class InspectionSerialNumberApiTest : BaseTest<InspectionSerialNumberViewModel>
    {
        public InspectionSerialNumberApiTest(TestServerFixture fixture, string equipmentId, string serialNumber,  string controllerName) : base(fixture, controllerName)
        {
            model = new InspectionSerialNumberViewModel()
            {
                EquipmentId =  int.Parse(equipmentId),
                SerialNumberId = int.Parse(serialNumber)
            };
        }
    }
}

