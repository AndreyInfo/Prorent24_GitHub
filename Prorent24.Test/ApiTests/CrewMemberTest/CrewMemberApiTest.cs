using Prorent24.Enum.CrewMember;
using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.CrewMember;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Prorent24.Test.ApiTests.CrewMemberTest
{
    public class CrewMemberApiTest : BaseTest<CrewMemberViewModel>
    {
        public CrewMemberApiTest(TestServerFixture fixture) : base(fixture, "crew_members")
        {
            model = new CrewMemberViewModel()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                MiddleName = "MiddleName",
                Phone = "Phone",
                FolderId = 1,

                UserRoleId = "Default user role",
                Active = true,
                Email = String.Concat(Guid.NewGuid(),"@gmail.com"),
                Username = String.Concat("Senya_",Guid.NewGuid()),
                //Password

                HoursInContract = 200,
                ContractDate = DateTime.Now,
                //DefaultRateId
                BankAccountNumber = "20000000",
                VAT = "VAT identification number",
                CompanyName = "senya2205198",
                CoCNumber = "CoCNumber",

                //Addres

                Birthdate = DateTime.Now,
                DrivingLicense = "DrivingLicense",
                PassportNumber = "Passport number",
                EmergencyContact = "EmergencyContact",
                DisplayInPlanner = true,
                ReceiveEmails = "ReceiveEmails", //bool
                Availability = AvailabilityCrewMemberTypeEnum.CanBePlannedMultipleTimes,
                Description = "Description"
            };
        }
    }
}
