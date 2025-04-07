using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Project;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Project
{
    internal class ProjectAdditionalCostStorage : BaseStorage<ProjectAdditionalCostEntity>, IProjectAdditionalCostStorage
    {
        public ProjectAdditionalCostStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IPagedList<ProjectAdditionalCostEntity>> GetAll(List<Predicate<ProjectAdditionalCostEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IPagedList<ProjectAdditionalCostEntity>> GetAllByForeignId(int id)
        {
            var result = await _repos.Table
               .Include(x=>x.VatClass)
               .Where(x => x.ProjectId == id).Select(x => x).ToPagedListAsync(0, 100);
            return result;
        }

        public async Task<List<ProjectAdditionalCostEntity>> GetAllByProject(int projectId)
        {
            return await _repos.TableNoTracking
                .Include(x => x.VatClass)
                .ThenInclude(y=>y.VatClassSchemeRates)
                .Where(x =>  x.ProjectId == projectId)
                .Select(x => x)
                .ToListAsync();
        }
    }
}
