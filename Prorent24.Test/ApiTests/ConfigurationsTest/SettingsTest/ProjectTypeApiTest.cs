using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Configuration.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.ConfigurationsTest.SettingsTest
{
    public class ProjectTypeApiTest : BaseTest<ProjectTypeViewModel>
    {
        public ProjectTypeApiTest(TestServerFixture fixture) : base(fixture, "configuration/settings/project_types")
        {
            model = new ProjectTypeViewModel()
            {
                Name = "ProjectType",
                Color = "Color",
                PackingSlipTemplateId = 1,
                QuotationTemplateId = 2,
                ContractTemplateId = 3,
                InvoiceTemplateId = 4,
                LetterheadTemplateId = 5,
                InvoiceMommentId = 6,
                DefaultAdditionalConditionId = 7
            };
        }
    }
}
