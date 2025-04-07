using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Subhire
{
    public class SubhireEquipmentGroupViewModel
    {
        public int Id { get; set; }

        public int SubhireId { get; set; }

        public string Name { get; set; }

        //public decimal TotalPrice { get; set; }

        public virtual ICollection<SubhireEquipmentViewModel> Equipments { get; set; }

        public int GroupId { get; set; }
    }
}
