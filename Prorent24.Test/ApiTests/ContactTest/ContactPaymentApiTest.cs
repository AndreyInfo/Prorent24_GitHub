using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.Contact;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Test.ApiTests.ContactTest
{
    public class ContactPaymentApiTest : BaseTest<ContactPaymentViewModel>
    {
        public ContactPaymentApiTest(TestServerFixture fixture, string controllerName) : base(fixture, controllerName)
        {
            model = new ContactPaymentViewModel()
            {
                ContactInsurancePercentage = 0,
                DiscountRentalEquipment = 0,
                DiscountSaleEquipment = 0,
                InsurancePercentage = 1,
                Rental = 10,
                Sales = 10,
                SubhireDiscount = 10,
                TotalDiscount = 10,
                TransportDiscount = 10,
                CrewDiscount = 10,
                InvoiceMomentId = 1,
                PaymentConditionId = 1,
                VatSchemeId = 1
            };
        }
    }
}
