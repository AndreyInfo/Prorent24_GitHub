using Prorent24.Enum.Equipment;
using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.EquipmentTest
{
    public class EquipmentApiTest : BaseTest<EquipmentViewModel>
    {
        public EquipmentApiTest(TestServerFixture fixture) : base(fixture, "equipments")
        {
            model = new EquipmentViewModel()
            {
                Name = "Test Equipment",
                SetType = SetTypeEnum.Case,
                SupplyType = SupplyTypeEnum.Rental,

                // SupplyType.Rental
                StockManagement = StockManagementEnum.ExcludeFromStockTracking,
                CriticalStock = 0,

                // SupplyType.Sale
                QuantityMode = QuantityModeEnum.EnterQuantityManually,
                Quantity = 10,

                LocationInWarehouse = "here",
                Code = "code#1",

                // DETAILS
                DisplayInPlanner = true,
                MeasuringUnit = "unit",
                InternalRemark = "text",
                ExternalRemark = "text",


                // FINACIAL
                // SupplyType == SupplyType.Rental
                RentalPrice = 10,
                NewPrice = 11,
                SubhirePrice = 10,
                MarginPrice = 10,

                PurchasePrice = 0,
                SalePrice = 0,

                // SPECIFICATION
                Length = 10,
                Height = 10,
                Width = 10,
                Weight = 1,
                Volume = 0,
                Power = 5,
                SurfaceItem = true,
                Current = 7
            };
        }
    }
}
