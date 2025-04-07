using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.Project
{
    class ProjectAdditionalCostApi : BaseTest<ProjectAdditionalCostViewModel>
    {
        public ProjectAdditionalCostApi(TestServerFixture fixture, string controllerName, int vatClassId) : base(fixture, controllerName)
        {
            model = new ProjectAdditionalCostViewModel()
            {
                Name = "усілякі супровідні витрати",
                SalePrice = 10,
                PurchasePrice = 15,
                VatClassId = vatClassId,
                Details = ""
            };
        }
    }
}
