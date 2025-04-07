using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Contact;
using Prorent24.WebApp.Models.General.Address;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.ContactTest
{
    public class ContactPersonApiTest : BaseTest<ContactPersonViewModel>
    {
        public ContactPersonApiTest(TestServerFixture fixture, string controllerName) : base(fixture, controllerName)
        {
            model = new ContactPersonViewModel()
            {
                Email = "",
                FirstName = "Test",
                Function = "",
                LastName = "Test",
                MiddleName = "",
                Mobile = "233",
                Address = new AddressViewModel(),
                Phone = "+3809999"
            };
        }
    }
}
