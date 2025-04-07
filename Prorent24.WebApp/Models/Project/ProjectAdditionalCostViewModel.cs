using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Project
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectAdditionalCostViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public int VatClassId { get; set; }
        public string Details { get; set; }
    }
}
