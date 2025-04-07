using Prorent24.Test.ApiTest.ConfigurationsTest.FinancialTest;
using Prorent24.Test.ApiTests.ConfigurationsTest.FinancialTest;
using Prorent24.Test.ApiTests.FinancialTest;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Prorent24.Test.ApiTests.ConfigurationsTest
{
    public class FinancialApiTest
    {
        private TestServerFixture _fixture = new TestServerFixture();

        [Fact]
        public async Task AdditionalCondition_Test()
        {
            AdditionalConditionApiTest apiTest = new AdditionalConditionApiTest(_fixture);
            await apiTest.Create();
            await apiTest.Update();
            await apiTest.GetById();
            await apiTest.GetAll();
            await apiTest.Delete();
        }

        [Fact]
        public async Task DiscountGroup_Test()
        {
            DiscountGroupApiTest apiTest = new DiscountGroupApiTest(_fixture);
            await apiTest.Create();
            await apiTest.Update();
            await apiTest.GetById();
            await apiTest.GetAll();
            await apiTest.Delete();
        }

        [Fact]
        public async Task ElectronicInvoicing_Test()
        {
            ElectronicInvoicingApiTest apiTest = new ElectronicInvoicingApiTest(_fixture);
            await apiTest.Update();
            await apiTest.Get();
        }

        [Fact]
        public async Task FactorGroup_Test()
        {
            FactorGroupApiTest apiTest = new FactorGroupApiTest(_fixture);
            await apiTest.Create();
            await apiTest.Update();
            await apiTest.GetById();
            await apiTest.GetAll();
           

            FactorGroupOptionApiTest apiOptionTest = new FactorGroupOptionApiTest(_fixture, Convert.ToInt32(apiTest.id));
            await apiOptionTest.Create();
            await apiOptionTest.Update();
            await apiOptionTest.GetById();
            //await apiTest.GetAll();
            await apiOptionTest.Delete(); 
            await apiTest.Delete();

        }

        [Fact]
        public async Task FinancialSettings_Test()
        {
            FinancialSettingApiTest apiTest = new FinancialSettingApiTest(_fixture);
            await apiTest.Update();
            await apiTest.Get();
        }

        [Fact]
        public async Task InvoiceMoment_Test()
        {
            InvoiceMomentApiTest apiTest = new InvoiceMomentApiTest(_fixture);
            await apiTest.Create();
            await apiTest.Update();
            await apiTest.GetById();
            await apiTest.GetAll();
            await apiTest.Delete();
        }

        [Fact]
        public async Task Ledger_Test()
        {
            LedgerApiTest apiTest = new LedgerApiTest(_fixture);
            await apiTest.Create();
            await apiTest.Update();
            await apiTest.GetById();
            await apiTest.GetAll();
            await apiTest.Delete();
        }

        
        [Fact]
        public async Task Payment_Test()
        {
            PaymentMethodApiTest apiMethodTest = new PaymentMethodApiTest(_fixture);
            await apiMethodTest.Create();
            await apiMethodTest.Update();
            await apiMethodTest.GetAll();


            PaymentConditionApiTest apiConditionTest = new PaymentConditionApiTest(_fixture, Convert.ToInt32(apiMethodTest.id));
            await apiConditionTest.Create();
            await apiConditionTest.Update();
            await apiConditionTest.GetAll();
            //await apiConditionTest.Delete();

            //await apiMethodTest.Delete();
        }

        //TODO починити видалення
        [Fact]
        public async Task Vat_Test()
        {
            VatClassApiTest apiVatClassTest = new VatClassApiTest(_fixture);
            await apiVatClassTest.Create();
            await apiVatClassTest.Update();
            await apiVatClassTest.GetAll();


            VatSchemeApiTest apiVatSchemeTest = new VatSchemeApiTest(_fixture, Convert.ToInt32(apiVatClassTest.id));
            await apiVatSchemeTest.Create();
            await apiVatSchemeTest.Update();
            await apiVatSchemeTest.GetAll();
            //await apiVatSchemeTest.Delete();

            //await apiVatClassTest.Delete();




        }
    }
}
