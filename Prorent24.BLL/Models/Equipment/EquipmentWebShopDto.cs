using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Equipment
{
    public class EquipmentWebShopDto: BaseDto<int>
    {
        public int EquipmentId { get; set; }
        public bool InWebshop { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string SEOTitle { get; set; }
        public string SEOKeyword { get; set; }
        public string SEODescription { get; set; }
        public bool FeaturedProduct { get; set; }
    }
}
