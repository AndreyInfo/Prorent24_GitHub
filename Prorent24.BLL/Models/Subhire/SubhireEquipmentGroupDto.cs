using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Subhire
{
    public class SubhireEquipmentGroupDto:BaseDto<int>
    {
        public int SubhireId { get; set; }
        
        public string Name { get; set; }

        //public decimal TotalPrice { get; set; }

        public ICollection<SubhireEquipmentDto> Equipments { get; set; }
    }
}
