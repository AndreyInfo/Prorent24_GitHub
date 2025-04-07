using Prorent24.Enum.Maintenance;
using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Maintenance.Inspection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.MaintenanceTest
{
    class InspectionApiTest : BaseTest<InspectionViewModel>
    {
        public InspectionApiTest(TestServerFixture fixture) : base(fixture, "inspections")
        {
            model = new InspectionViewModel()
            {
                Status = InspectionStatusEnum.Approved,
                PeriodicInspectionId = 1,
                Date = DateTime.Now,
                Description = "Test description",
            };
        }
    }
}
