using Prorent24.Test.ApiTests.ContactTest;
using Prorent24.Test.ApiTests.EquipmentTest;
using Prorent24.Test.ApiTests.MaintenanceTest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Prorent24.Test.ApiTests
{
    public class MainTenanceApiTest
    {
        private TestServerFixture _fixture = new TestServerFixture();

        [Fact]
        public async Task Inspection_Test()
        {
            EquipmentApiTest apiEquipmentTest = new EquipmentApiTest(_fixture);
            await apiEquipmentTest.Create();  
            
            EquipmentSerialNumberApiTest equipmentSerialNumberApiTest = new EquipmentSerialNumberApiTest(_fixture, apiEquipmentTest.id, $"equipments/{apiEquipmentTest.id}/serial_numbers");
            await equipmentSerialNumberApiTest.Create();

            InspectionApiTest apiInspectionTest = new InspectionApiTest(_fixture);
            await apiInspectionTest.Create();
            await apiInspectionTest.Update();
            await apiInspectionTest.GetById();
            await apiInspectionTest.GetAll();

            InspectionSerialNumberApiTest apiInspectionSerialNumberTest = new InspectionSerialNumberApiTest(_fixture, apiEquipmentTest.id, equipmentSerialNumberApiTest.id, $"inspections/{apiInspectionTest.id}/serial_numbers");
            await apiInspectionSerialNumberTest.Create();
            await apiInspectionSerialNumberTest.Update();
            await apiInspectionSerialNumberTest.GetById();
            await apiInspectionSerialNumberTest.GetAll();

            await apiInspectionSerialNumberTest.Delete();

            await apiInspectionTest.Delete();


            await equipmentSerialNumberApiTest.Delete();
            await apiEquipmentTest.Delete();
        }

        [Fact]
        public async Task Repair_Test()
        {
            EquipmentApiTest apiEquipmentTest = new EquipmentApiTest(_fixture);
            await apiEquipmentTest.Create();

            EquipmentSerialNumberApiTest equipmentSerialNumberApiTest = new EquipmentSerialNumberApiTest(_fixture, apiEquipmentTest.id, $"equipments/{apiEquipmentTest.id}/serial_numbers");
            await equipmentSerialNumberApiTest.Create();

            RepairApiTest apiReparTest = new RepairApiTest(_fixture, apiEquipmentTest.id, equipmentSerialNumberApiTest.id, "repairs");
            await apiReparTest.Create();
            await apiReparTest.Update();
            await apiReparTest.GetById();

            await apiReparTest.Delete();

            await equipmentSerialNumberApiTest.Delete();

            await apiEquipmentTest.Delete();

        }
    }
}
