using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Configuration.Settings
{
    public class TimeAndLocationViewModel
    {
        [Required]
        public int TimeZone { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public int CountryId { get; set; }

        public long? Lat { get; set; }
        public long? Long { get; set; }
        public long? Alt { get; set; }
    }
}
