using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Project;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.DAL.Storages.Project
{
    internal class ProjectFunctionGroupStorage : BaseStorage<ProjectFunctionGroupEntity>, IProjectFunctionGroupStorage
    {
        public ProjectFunctionGroupStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<ProjectFunctionGroupEntity>> GetListFunctionGroupsByProdjectId(int projectId)
        {
           return await _repos.Table.Where(x => x.SubprojectId == projectId).ToListAsync();
        }

        public Task<IPagedList<ProjectFunctionGroupEntity>> GetAll(List<Predicate<ProjectFunctionGroupEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }
    }
}