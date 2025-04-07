using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration.Financial;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Project
{
    [Table("dbo_project_additional_costs")]
    public class ProjectAdditionalCostEntity: BaseEntity
    {
        public string Name { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual ProjectEntity Project { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SalePrice { get; set; }

        public int VatClassId { get; set; }

        [ForeignKey("VatClassId")]
        public virtual VatClassEntity VatClass { get; set; }

        public string Details { get; set; }
    }
}
