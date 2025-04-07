using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Prorent24.Enum.Entity;

namespace Prorent24.Test.ApiTests
{
    public class TaskApiTest : BaseTest<TaskViewModel>
    {
        // vehicles
        public TaskApiTest(TestServerFixture fixture) : base(fixture, "tasks")
        {
            model = new TaskViewModel()
            {
                //BelongsTo = EntityEnum.VehicleEntity,
                IsPublic = true,
                Name  = "test task",
                DeadLine = DateTime.Now,
                //VehicleId = 1,
                CrewMembers = new List<WebApp.Models.CrewMember.CrewMemberShortViewModel>() {
                    new WebApp.Models.CrewMember.CrewMemberShortViewModel(){ Id = "542df0b7-3dee-42bc-b627-8e34e30934ab"}
                },
                Description = "ты слишком много ищешь; из за чрезмерного искания ты не успеваешь находить",
            };
        }

        [Fact]
        public async Task Task_Test()
        {
            await Create();
            await GetById();
            await Update();
            await GetById();
            await GetAll();
            await Delete();
        }
    }
}
