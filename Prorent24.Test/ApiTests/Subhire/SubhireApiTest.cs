using Prorent24.Enum.Contact;
using Prorent24.Enum.Subhire;
using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Contact;
using Prorent24.WebApp.Models.CrewMember;
using Prorent24.WebApp.Models.General.Address;
using Prorent24.WebApp.Models.Subhire;
using System;

namespace Prorent24.Test.ApiTests.Subhire
{
    public class SubhireApiTest : BaseTest<SubhireViewModel>
    {
        public SubhireApiTest(TestServerFixture fixture) : base(fixture, "subhires")
        {
            model = new SubhireViewModel()
            {
                Name = "Name Subhire",
                Number = "00001",
                Status = SubhireStatusEnum.Application,
                Reference = "Reference",
                EquipmentCost = 10,
                AdditionalCost = 10,
                TotalCost = 20,
                Type = SubhireTypeEnum.CollectAtSupplier,
                Remark = "Remark",
                UsagePeriodStart = DateTime.Now,
                UsagePeriodEnd = DateTime.Now.AddDays(1),
                PlanningPeriodStart = DateTime.Now,
                PlanningPeriodEnd = DateTime.Now.AddDays(1)

            };
        }
    }
}
