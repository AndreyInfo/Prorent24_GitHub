using Prorent24.Entity.Configuration.Settings;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.Settings.ProjectType
{
    internal class ProjectTypeStorage : BaseStorage<ProjectTypeEntity>, IProjectTypeStorage
    {
        public ProjectTypeStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IPagedList<ProjectTypeEntity>> GetAll(List<Predicate<ProjectTypeEntity>> conditions = null)
        {
            return _repos.GetPagedListAsync<ProjectTypeEntity>(x => x);
        }
    }

}