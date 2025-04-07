using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Equipment
{
    public class EquipmentWebShopViewModel
    {
        public bool InWebshop { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string SEOTitle { get; set; }
        public string SEOKeyword { get; set; }
        public string SEODescription { get; set; }
        public bool FeaturedProduct { get; set; }
    }
}
