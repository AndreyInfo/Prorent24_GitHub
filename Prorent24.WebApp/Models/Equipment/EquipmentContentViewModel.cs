using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Equipment
{
    public class EquipmentContentViewModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string Equipment { get; set; }
        public int ContentId { get; set; }
        public string ContentName { get; set; }
        public int Quantity { get; set; }
        public bool UnfoldFinancialDocuments { get; set; }
        public bool UnfoldPackingSlip { get; set; }
    }
}
