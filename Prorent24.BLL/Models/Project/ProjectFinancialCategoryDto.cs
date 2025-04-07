using Prorent24.Common.Attributes;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Project
{
    public class ProjectFinancialCategoryDto : BaseDto<int>
    {
        public int ProjectId { get; set; }
        
        [IncludeToGrid(Order = 5)]
        public ProjectFinancialCategoryEnum Category { get; set; }

        [IncludeToGrid(Order = 6)]
        public int? ParentId { get; set; } // for rental sale equipment

        [IncludeToGrid(Order = 7)]
        public string Name { get; set; } // for rental sale equipment

        [IncludeToGrid(Order = 8)]
        public decimal EstimatedCosts { get; set; } = 0;
         
        [IncludeToGrid(Order = 9)]
        public decimal PlannedCosts { get; set; } = 0;

        [IncludeToGrid(Order = 10)]
        public decimal ActualCosts { get; set; } = 0;

        [IncludeToGrid(Order = 11)]
        public decimal Revenue { get; set; } = 0;

        [IncludeToGrid(Order = 12)]
        public decimal Discount { get; set; } = 0;

        [IncludeToGrid(Order = 13)]
        public decimal Profit { get; set; } = 0;

        [IncludeToGrid(Order = 14)]
        public decimal Total { get; set; } = 0;

        public decimal TotalIncVat { get; set; } = 0;

        public ICollection<ProjectFinancialCategoryDto> Children { get; set; }

    }
}
