using Prorent24.Entity.Project;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Project
{
    public interface IProjectFinancialCategoryStorage
    {

        /// <summary>
        /// Get by ProjectId
        /// </summary>
        /// <returns></returns>
        Task<List<ProjectFinancialCategoryEntity>> GetAllByProject(int projectId);

        Task CreateBaseCategoriesByProject(List<ProjectFinancialCategoryEntity> categories);

        Task<ProjectFinancialCategoryEntity> Update(ProjectFinancialCategoryEntity model);

        Task<ProjectFinancialCategoryEntity> Create(ProjectFinancialCategoryEntity model);

        Task<ProjectFinancialCategoryEntity> GetByCategoryAndEquipmentGroup(int projectId, int equipmentGroupId, ProjectFinancialCategoryEnum category);

        Task<ProjectFinancialCategoryEntity> GetByCategory(int projectId, ProjectFinancialCategoryEnum categoryType);

        Task<List<ProjectFinancialCategoryEntity>> GetSubCategories(int projectId, ProjectFinancialCategoryEnum categoryType);

        Task<bool> Delete(int id);
        /// <summary>
        /// Get by ProjectId Include Children
        /// </summary>
        /// <returns></returns>
        Task<List<ProjectFinancialCategoryEntity>> GetAllIncludeChildrenByProject(int projectId);

    }
}
