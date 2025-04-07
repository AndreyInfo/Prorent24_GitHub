using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Equipment
{
    public class EquipmentSupplierViewModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string Equipment { get; set; }
        public decimal Price { get; set; }
        public string Details { get; set; }
        public int ContactId { get; set; }
        public string ContactName { get; set; }
    }
}
