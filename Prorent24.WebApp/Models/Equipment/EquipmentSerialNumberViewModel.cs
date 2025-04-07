using Prorent24.WebApp.Models.Equipment.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Equipment
{
    public class EquipmentSerialNumberViewModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string SerialNumber { get; set; }
        public string InternalReference { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public bool CalculateBookValueAutomatically { get; set; }
        // CalculateBookValueAutomatically == true
        public decimal DepreciationPerMonth { get; set; }
        // CalculateBookValueAutomatically == false
        public decimal BookValue { get; set; }
        public string Remark { get; set; }
        public bool Active { get; set; }

        public List<SupplierInfoViewModel> SuppliersInfo { get; set; }
    }
}
