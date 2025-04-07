using Prorent24.Entity.Project;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Project
{
    internal class ProjectFinancialStorage : BaseStorage<ProjectFinancialEntity>, IProjectFinancialStorage
    {
        public ProjectFinancialStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<IPagedList<ProjectFinancialEntity>> GetAll(List<Predicate<ProjectFinancialEntity>> conditions = null)
        {
            return await _repos.GetPagedListAsync();
        }

        public async Task<ProjectFinancialEntity> GetByProjectId(int projectId)
        {
            var result = _repos.GetFirstOrDefault(x => x, predicate: c => c.ProjectId == projectId);
            return result;
        }
    }
}
