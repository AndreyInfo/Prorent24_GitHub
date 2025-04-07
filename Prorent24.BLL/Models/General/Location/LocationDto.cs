using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.General.Location
{
    public class LocationDto
    {
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
