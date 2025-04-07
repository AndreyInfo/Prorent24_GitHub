using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.EquipmentTest
{
    public class EquipmentSerialNumberApiTest : BaseTest<EquipmentSerialNumberViewModel>
    {
        public EquipmentSerialNumberApiTest(TestServerFixture fixture, string equipmentId, string controllerName) : base(fixture, controllerName)
        {
            model = new EquipmentSerialNumberViewModel()
            {
                EquipmentId = int.Parse(equipmentId),
                Active = true,
                BookValue = 10,
                InternalReference = "ref",
                Remark = "remark",
                CalculateBookValueAutomatically = false, 
                DepreciationPerMonth = 10,
                PurchaseDate = DateTime.Now,
                PurchasePrice = 10,
                SerialNumber = "125"
            };
        }
    }
}
