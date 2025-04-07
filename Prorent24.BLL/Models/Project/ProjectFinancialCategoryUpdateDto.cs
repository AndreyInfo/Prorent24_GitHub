namespace Prorent24.BLL.Models.Project
{
    public class ProjectFinancialCategoryUpdateDto 
    {
        public ProjectFinancialCategoryDto UpdateCategory { get; set; }

        public ProjectFinancialCategoryDto TotalExcludeVatCategory { get; set; }

        public decimal TotalIncludeVat { get; set; }
    }
}
