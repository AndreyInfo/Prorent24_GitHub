using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Equipment
{
    public class EquipmentAlternativeViewModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string Equipment { get; set; }
        public int AlternativeId { get; set; }
        public string AlternativeName { get; set; }
        public string Code { get; set; }
    }
}
