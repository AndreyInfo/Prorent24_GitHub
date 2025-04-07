using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Project
{
    public class ProjectFinancialCategoryViewModel
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectFinancialCategoryEnum Category { get; set; }

        public int? ParentId { get; set; } // for rental sale equipment

        public string Name { get; set; } // for rental sale equipment group

        public decimal EstimatedCosts { get; set; }

        public decimal PlannedCosts { get; set; }

        public decimal ActualCosts { get; set; }

        public decimal Revenue { get; set; }

        public decimal Discount { get; set; }

        public decimal Profit { get; set; }

        public decimal Total { get; set; }

        public decimal TotalIncVat { get; set; }

        public ICollection<ProjectFinancialCategoryViewModel> Children { get; set; }

    }
}
