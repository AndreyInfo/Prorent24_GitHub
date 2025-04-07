using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Contact;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.ContactTest
{
    public class ContactElectronicInvoiceApiTest : BaseTest<ContactElectronicInvoiceViewModel>
    {
        public ContactElectronicInvoiceApiTest(TestServerFixture fixture, string controllerName) : base(fixture, controllerName)
        {
            model = new ContactElectronicInvoiceViewModel()
            {
                IdentificationNumber = "123456",
                IdentificationScheme = "0006"

            };
        }
    }
}
