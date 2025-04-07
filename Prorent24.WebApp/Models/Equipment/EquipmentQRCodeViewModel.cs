using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Equipment
{
    public class EquipmentQRCodeViewModel
    {
        public int Id { get; set; }
        //public int EquipmentId { get; set; }
        public string Code { get; set; }
        public string Image { get; set; }
    }
}
