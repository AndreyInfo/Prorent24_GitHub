using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.EquipmentTest
{
    public class EquipmentSupplierApiTest : BaseTest<EquipmentSupplierViewModel>
    {
        public EquipmentSupplierApiTest(TestServerFixture fixture, string contactId, string controllerName) : base(fixture, controllerName)
        {
            model = new EquipmentSupplierViewModel()
            {
               ContactId = int.Parse(contactId),
               Details = "Description",
               Price = 10
            };
        }
    }
}
