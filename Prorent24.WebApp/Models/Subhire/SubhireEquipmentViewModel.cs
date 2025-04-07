using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Subhire
{
    public class SubhireEquipmentViewModel
    {
        public int Id { get; set; }

        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }

        public int? ParentId { get; set; } // for equipment kit type

        public virtual ICollection<SubhireEquipmentViewModel> Children { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal Factor { get; set; }

        public string Remark { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
