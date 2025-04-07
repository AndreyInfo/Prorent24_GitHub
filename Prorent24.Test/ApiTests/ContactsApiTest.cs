using Prorent24.Test.ApiTests.ContactTest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Prorent24.Test.ApiTests
{
    public class ContactsApiTest
    {
        private TestServerFixture _fixture = new TestServerFixture();

        [Fact]
        public async Task Contact_Test()
        {
            ContactApiTest apiContactTest = new ContactApiTest(_fixture);
            await apiContactTest.Create();
            await apiContactTest.Update();
            await apiContactTest.GetAll();
            await apiContactTest.GetById();
            await apiContactTest.GetDetails();

            ContactPaymentApiTest apiPaymentTest = new ContactPaymentApiTest(_fixture, $"contacts/{apiContactTest.id}/contact_payment");
            await apiPaymentTest.Create();
            await apiPaymentTest.GetAll();

            ContactPersonApiTest apiContactPersonTest = new ContactPersonApiTest(_fixture, $"contacts/{apiContactTest.id}/contact_persons");
            await apiContactPersonTest.Create();
            await apiContactPersonTest.Update();
            await apiContactPersonTest.GetAll();
            await apiContactPersonTest.GetById();
            await apiContactPersonTest.Delete();
            //await apiContactPersonTest.GetDetails();

            ContactElectronicInvoiceApiTest apiElectronicInvoiceTest = new ContactElectronicInvoiceApiTest(_fixture, $"contacts/{apiContactTest.id}/contact_electronic_invoice");
            await apiElectronicInvoiceTest.Create();
            await apiElectronicInvoiceTest.GetAll();

            await apiContactTest.Delete();
        }
    }
}
