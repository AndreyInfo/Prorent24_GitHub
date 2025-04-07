using Prorent24.Enum.Maintenance;
using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Maintenance.Repair;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.MaintenanceTest
{
    class RepairApiTest : BaseTest<RepairViewModel>
    {
        public RepairApiTest(TestServerFixture fixture, string equipmentId, string serialNumber, string controllerName) : base(fixture, controllerName)
        {
            model = new RepairViewModel()
            {
                EquipmentId = int.Parse(equipmentId),
                SerialNumberId = int.Parse(serialNumber),
                From = DateTime.Now,
                Until = DateTime.Now,
                Cost = 10,
                Quantity = 10,
                Remark = "",
                Usable = UsableTypeEnum.ItemCanBeUsed
            };
        }
    }
}
