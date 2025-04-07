using Prorent24.Enum.Configuration;
using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Configuration.Financials.Vat;
using System.Collections.Generic;

namespace Prorent24.Test.ApiTests.FinancialTest
{
    public class VatSchemeApiTest : BaseTest<VatSchemeViewModel>
    {
        public VatSchemeApiTest(TestServerFixture fixture, int vatClassId) : base(fixture, "configuration/financial/vat_schemes")
        {
            VatClassSchemeRateViewModel classSchemeRate = new VatClassSchemeRateViewModel()
            {
                VatClassId = vatClassId,
                VatClass= new VatClassViewModel
                {
                    Id = vatClassId,
                    Name = "name"
                },
                Rate = 10,
                AccountingCode = "AccountingCode",
                EdifactCode = "EdifactCode"
                
            };
            model = new VatSchemeViewModel()
            {
                Name = "VatScheme",
                Type = VatSchemeTypeEnum.FixedRate,
                VatClassSchemeRates = new List<VatClassSchemeRateViewModel>()
            };
            model.VatClassSchemeRates.Add(classSchemeRate);
        }
    }
}