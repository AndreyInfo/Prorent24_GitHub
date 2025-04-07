using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Project;
using Prorent24.Enum.Project;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Project
{
    internal class ProjectFinancialCategoryStorage : BaseStorage<ProjectFinancialCategoryEntity>, IProjectFinancialCategoryStorage
    {
        public ProjectFinancialCategoryStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<IPagedList<ProjectFinancialCategoryEntity>> GetAll(List<Predicate<ProjectFinancialCategoryEntity>> conditions = null)
        {
            return await _repos.GetPagedListAsync();
        }

        public async Task<List<ProjectFinancialCategoryEntity>> GetAllByProject(int projectId)
        {
            return await _repos.TableNoTracking
                 .Where(x => x.ProjectId == projectId)
                 .Select(x => x)
                 .ToListAsync();
        }

        public async Task<List<ProjectFinancialCategoryEntity>> GetAllIncludeChildrenByProject(int projectId)
        {
            return await _repos.TableNoTracking.Include(x => x.Children)
                 .Where(x => x.ProjectId == projectId && x.ParentId == null)
                 .Select(x => x)
                 .ToListAsync();
        }


        public async Task CreateBaseCategoriesByProject(List<ProjectFinancialCategoryEntity> categories)
        {
            await _repos.InsertAsync(categories);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ProjectFinancialCategoryEntity> GetByCategoryAndEquipmentGroup(int projectId, int equipmentGroupId, ProjectFinancialCategoryEnum category)
        {
            return await _repos.TableNoTracking
              .Where(x => x.ProjectId == projectId && x.Category == category && x.EquipmentGroupId == equipmentGroupId).FirstOrDefaultAsync();
        }

        public async Task<ProjectFinancialCategoryEntity> GetByCategory(int projectId, ProjectFinancialCategoryEnum category)
        {
            return await _repos.TableNoTracking
                .Where(x => x.ProjectId == projectId && x.Category == category && x.ParentId == null).FirstOrDefaultAsync();
        }

        public async Task<List<ProjectFinancialCategoryEntity>> GetSubCategories(int projectId, ProjectFinancialCategoryEnum categoryType)
        {
            return await _repos.TableNoTracking
                .Where(x => x.ProjectId == projectId && x.Category == categoryType && x.ParentId != null)
                .Select(x => x)
                .ToListAsync();
        }
    }
}
