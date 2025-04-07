using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Vehicle;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Prorent24.Test.ApiTests
{
    public class VehicleApiTest : BaseTest<VehicleViewModel>
    {
        // vehicles
        public VehicleApiTest(TestServerFixture fixture) : base(fixture, "vehicles")
        {
            model = new VehicleViewModel()
            {
                Name = "Test",
                RegistrationNumber = "QW21WE",
                LoadCapacity = 12.52M,
                MOTDate = DateTime.Now,
                FolderId = 1,
                DeployedMultipleTimesSimultaneously = true,


                DayilCosts = 120,
                VariableCosts = 25.23M,

                DisplayInPlanner = true,
                LoadingSurface = "2x2x2",
                Length = 15.5M,
                Width = 10,
                Height = 2,
                Seats = 4,

                Description = "ты слишком много ищешь; из за чрезмерного искания ты не успеваешь находить",
            };
        }

        [Fact]
        public async Task Vehicle_Test()
        {
            await Create();
            await Update();
            await GetAll();
            await Delete();
        }
    }
}
