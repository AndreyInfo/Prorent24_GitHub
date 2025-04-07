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
    internal class ProjectCrewMemberStorage : BaseStorage<ProjectVisibilityCrewMemberEntity>, IProjectCrewMemberStorage
    {
        public ProjectCrewMemberStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async override Task<ProjectVisibilityCrewMemberEntity> Create(ProjectVisibilityCrewMemberEntity model)
        {
            ProjectVisibilityCrewMemberEntity result = await base.Create(model);
            return await _repos.Table.Include(x => x.CrewMember).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAllByProjectId(int Id)
        {
            var entities = await _repos.Table.Where(x => x.ProjectId == Id).Select(x => x).ToListAsync();
            if (entities != null)
            {
                await Task.Run(() =>
                {
                    _repos.Delete(entities);
                    _unitOfWork.SaveChanges();
                });

            }
            else
            {
                return false;
            }

            entities = await _repos.Table.Where(x => x.ProjectId == Id).Select(x => x).ToListAsync();

            if (entities != null || entities.Count > 0)
            {
                return false;
            }
            else
            {
                return true;

            }
        }

        public Task<IPagedList<ProjectVisibilityCrewMemberEntity>> GetAll(List<Predicate<ProjectVisibilityCrewMemberEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

    }
}
