using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.TimeRegistration
{
    public class TimeRegistrationActivityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public TimeUnitTypeEnum ActivityTimeUnit { get; set; } //Add to BD
    }
}
