using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.EquipmentTest
{
    public class EquipmentContentApiTest : BaseTest<EquipmentContentViewModel>
    {
        public EquipmentContentApiTest(TestServerFixture fixture, string equipmentId, string equipmentContentId, string controllerName) : base(fixture, controllerName)
        {
            model = new EquipmentContentViewModel()
            {
                EquipmentId = int.Parse(equipmentId),
                ContentId = int.Parse(equipmentContentId),
                Quantity = 10,
                UnfoldFinancialDocuments = true,
                UnfoldPackingSlip = true
            };
        }
    }
}
