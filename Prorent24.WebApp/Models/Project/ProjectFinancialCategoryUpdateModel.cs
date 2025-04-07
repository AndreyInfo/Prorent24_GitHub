using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Project
{
    public class ProjectFinancialCategoryUpdateModel
    {
        public ProjectFinancialCategoryViewModel UpdateCategory { get; set; }

        public ProjectFinancialCategoryViewModel TotalExcludeVatCategory { get; set; }

        public decimal TotalIncludeVat { get; set; }

    }
}
