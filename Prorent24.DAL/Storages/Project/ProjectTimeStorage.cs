using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Project;
using Prorent24.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Project
{
    internal class ProjectTimeStorage : BaseStorage<ProjectTimeEntity>, IProjectTimeStorage
    {
        public ProjectTimeStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<bool> DeleteAllOtherByProjectId(int Id)
        {
            var entities = await _repos.Table.Where(x => x.ProjectId == Id && x.DefaultUsagePeriod != true && x.DefaultPlanPeriod!=true).Select(x => x).ToListAsync();
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
    }
}
