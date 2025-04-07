using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Project;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Project.Financial
{
    public interface IProjectFinancialService
    {
        Task<ProjectFinancialDto> GetByProjectId(int projectId);

        Task<ProjectFinancialDto> Update(ProjectFinancialDto model);

        Task CreateBaseCategoriesByProject(int projectId);

        Task<ProjectFinancialDto> CreateDefaultFinancial(int projectId);

        Task<ProjectFinancialCategoryUpdateDto> UpdateCategory(ProjectFinancialCategoryDto model);
        
        Task<List<ProjectFinancialCategoryDto>> GetCategoriesByProject(int projectId);


        /// <summary>
        /// створення чи оновлення вкладених категорій в Sale фбо Rental по фінансах  у випадку створення чи зміни групи обладнання, додання чи видаленння з неї елементів та перерахунок цих груп
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="equipmentGroupId"></param>
        /// <returns></returns>
        Task SaveSaleRentalSubCategoriesByProject(int projectId, int equipmentGroupId, bool isDeleteGroup = false);

        Task CalculateAdditionalCostFinancialCategory(int projectId);

        Task CalculateCrewTransporFinancialCategory(int projectId, ProjectFunctionTypeEnum functionType);

        Task<ProjectFinancialCategoryDto> CalculateTotalFinancialCategory(int projectId);

        Task<VatSchemeRateDto> GetVatScheme(int projectId, int? vatShemeId = null);

        Task<List<ProjectFinancialCategoryDto>> GetCategoriesIncludeChildrenByProject(int projectId);
    


    }
}
