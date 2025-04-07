using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Project
{
    public class ProjectEquipmentViewModel
    {
        public int Id { get; set; }

        public int GroupId { get; set; }
        public string GroupName { get; set; } // for output

        public int? EquipmentId { get; set; }
        public string EquipmentName { get; set; } // for output

        public int? ParentId { get; set; } // for equipment kit type

        public virtual ICollection<ProjectEquipmentViewModel> Children { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal Factor { get; set; }

        public string Remark { get; set; }

        public decimal TotalPrice { get; set; }

        public int? VatClassId { get; set; }

        public long? AvailableQuantity { get; set; }
    }
}
