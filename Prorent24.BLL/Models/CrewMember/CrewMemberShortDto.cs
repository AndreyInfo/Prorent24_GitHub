using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.CrewMember
{
    public class CrewMemberShortDto
    {
        public string Id { get; set; } // UserId
        public string ColorAppointments { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
    }
}
