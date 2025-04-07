using Prorent24.Enum.Contact;
using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Contact;
using Prorent24.WebApp.Models.General.Address;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.ContactTest
{
    public class ContactApiTest : BaseTest<ContactViewModel>
    {
        public ContactApiTest(TestServerFixture fixture) : base(fixture, "contacts")
        {
            model = new ContactViewModel()
            {
                Bic = "bic",
                BankAccountNumber = "1234",
                CocNumber = "1235",
                Email = "email",
                CompanyName = "Company",
                FiscalCode = "1234",
                //NameLine = "name line test",
                Phone = "099999222",
                ProjNote = "note",
                Type = ContactType.Company,
                SubjectProjNote = "qwer",
                //Email2 = "",
                Warning = "warning",
                Website = "",
                VatIdentificationNumber = "1234",
                FolderId = 1,
                //BillingAddress = new AddressViewModel() {
                //},
                PostalAddress = new AddressViewModel() {
                },
                VisitingAddress = new AddressViewModel() {
                }
            };
        }
    }
}
