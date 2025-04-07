using Prorent24.Entity.Base;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Project
{
    [Table("dbo_project_financial_categories")]
    public class ProjectFinancialCategoryEntity : BaseEntity
    {
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual ProjectEntity Project { get; set; }

        public ProjectFinancialCategoryEnum Category { get; set; }

        public int? ParentId { get; set; } // for rental sale equipment

        [ForeignKey("ParentId")]
        public virtual ProjectFinancialCategoryEntity ParentFinancialCategory { get; set; }

        [InverseProperty("ParentFinancialCategory")]
        public ICollection<ProjectFinancialCategoryEntity> Children { get; set; }

        public int? EquipmentGroupId { get; set; } // for rental sale equipment

        [ForeignKey("EquipmentGroupId")]
        public virtual ProjectEquipmentGroupEntity EquipmentGroup { get; set; } // for rental sale equipment

        public string Name { get; set; } // for rental sale equipment

        public decimal EstimatedCosts { get; set; }

        public decimal PlannedCosts { get; set; }

        public decimal ActualCosts { get; set; }

        public decimal Revenue { get; set; }

        public decimal Discount { get; set; }

        public decimal Profit { get; set; }

        public decimal Total { get; set; }

        public decimal? TotalIncVat { get; set; } 
    }
}
