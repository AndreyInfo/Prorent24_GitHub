using Prorent24.WebApp.Models.General.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Contact
{
    public class ContactPersonShortViewModel
    {
        public int Id { get; set; }
        public int ContactId { get; set; }

        public string Name { get; set; }
    }
}
