using Prorent24.Test.ApiTests.ContactTest;
using Prorent24.Test.ApiTests.EquipmentTest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Prorent24.Test.ApiTests
{
    public class EquipmentsApiTest
    {
        private TestServerFixture _fixture = new TestServerFixture();

        [Fact]
        public async Task Equipment_Test()
        {
            EquipmentApiTest apiEquipmentTest = new EquipmentApiTest(_fixture);
            Console.Write("EquipmentCreate");
            await apiEquipmentTest.Create();
            await apiEquipmentTest.Update();
            await apiEquipmentTest.GetAll();
            EquipmentApiTest apiEquipmentTest2 = new EquipmentApiTest(_fixture);
            await apiEquipmentTest2.Create();

            EquipmentContentApiTest apiContentTest = new EquipmentContentApiTest(_fixture, apiEquipmentTest.id, apiEquipmentTest2.id, $"equipments/{apiEquipmentTest.id}/contents");
            await apiContentTest.Create();
            await apiContentTest.Update();
            await apiContentTest.GetById();
            await apiContentTest.GetAll();
            await apiContentTest.Delete();

            EquipmentAccessoryApiTest apiAccessoryTest = new EquipmentAccessoryApiTest(_fixture, apiEquipmentTest.id, apiEquipmentTest2.id, $"equipments/{apiEquipmentTest.id}/accessories");
            await apiAccessoryTest.Create();
            await apiAccessoryTest.Update();
            await apiAccessoryTest.GetById();
            await apiAccessoryTest.GetAll();
            await apiAccessoryTest.Delete();

            EquipmentAlternativeApiTest apiAlternativeTest = new EquipmentAlternativeApiTest(_fixture, apiEquipmentTest.id, apiEquipmentTest2.id, $"equipments/{apiEquipmentTest.id}/alternatives");
            await apiAlternativeTest.Create();
            await apiAlternativeTest.Update();
            await apiAlternativeTest.GetById();
            await apiAlternativeTest.GetAll();
            await apiAlternativeTest.Delete();

            EquipmentQRCodeApiTest equipmentQRCodeApiTest = new EquipmentQRCodeApiTest(_fixture, $"equipments/{apiEquipmentTest.id}/qr_codes");
            await equipmentQRCodeApiTest.Create();
            await equipmentQRCodeApiTest.Update();
            await equipmentQRCodeApiTest.GetById();
            await equipmentQRCodeApiTest.GetAll();
            await equipmentQRCodeApiTest.Delete();

            Console.ForegroundColor = ConsoleColor.Green;
            Trace.WriteLine("Equipment Serial Number Api Test START");
            EquipmentSerialNumberApiTest equipmentSerialNumberApiTest = new EquipmentSerialNumberApiTest(_fixture, apiEquipmentTest.id, $"equipments/{apiEquipmentTest.id}/serial_numbers");
            await equipmentSerialNumberApiTest.Create();
            await equipmentSerialNumberApiTest.Update();
            await equipmentSerialNumberApiTest.GetById();
            await equipmentSerialNumberApiTest.GetAll();

            EquipmentQRCodeApiTest equipmentSerialNumberQRCodeApiTest = new EquipmentQRCodeApiTest(_fixture, $"equipments/{apiEquipmentTest.id}/serial_numbers/{equipmentSerialNumberApiTest.id}/qr_codes");
            await equipmentSerialNumberQRCodeApiTest.Create();
            await equipmentSerialNumberQRCodeApiTest.Update();
            await equipmentSerialNumberQRCodeApiTest.GetById();
            await equipmentSerialNumberQRCodeApiTest.GetAll();
            await equipmentSerialNumberQRCodeApiTest.Delete();

            await equipmentSerialNumberApiTest.Delete();

            ContactApiTest apiContactTest = new ContactApiTest(_fixture);
            await apiContactTest.Create();

            EquipmentSupplierApiTest equipmentSupplierApiTest = new EquipmentSupplierApiTest(_fixture, apiContactTest.id, $"equipments/{apiEquipmentTest.id}/suppliers");
            await equipmentSupplierApiTest.Create();
            await equipmentSupplierApiTest.Update();
            await equipmentSupplierApiTest.GetById();
            await equipmentSupplierApiTest.GetAll();
            await equipmentSupplierApiTest.Delete();

            EquipmentPeriodicInspectionApiTest equipmentPeriodicInspectionApiTest = new EquipmentPeriodicInspectionApiTest(_fixture, "1", $"equipments/{apiEquipmentTest.id}/periodic_inspections");
            await equipmentPeriodicInspectionApiTest.GetAll();

            await apiContactTest.Delete();

            await apiEquipmentTest2.Delete();
            await apiEquipmentTest.Delete();
        }
    }
}
