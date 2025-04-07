using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Prorent24.Common.Models.Filters;
using Prorent24.DAL.Models.Project;
using Prorent24.Entity.Project;
using Prorent24.Enum.Directory;
using Prorent24.Enum.General;
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
    internal class ProjectFunctionStorage : BaseStorage<ProjectFunctionEntity>, IProjectFunctionStorage
    {
        public ProjectFunctionStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<List<ProjectFunctionEntity>> GetAllFunction(List<Predicate<ProjectFunctionEntity>> conditions = null)
        {
            return await _repos.TableNoTracking.Where(z => z.ProjectId == null).ToListAsync();
        }

        public async Task<List<ProjectFunctionEntity>> GetAllFunctionForGeneralTimeLine(IQueryable<ProjectFunctionEntity> queryableEntity, string search)
        {
            var functions = await queryableEntity.Include(x => x.Project).Include(x => x.ProjectFunctionGroup).Where(z => z.ProjectId != null).ToListAsync();
            if (search.Length > 0)
            {
                return functions.Where(x => x.Project?.Name.ToLower().IndexOf(search.ToLower(), StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            return functions;
        }



        public async Task<List<ProjectFunctionEntity>> GetAllByProject(int projectId, ProjectFunctionTypeEnum? type = null)
        {
            if (type != null)
            {
                return await _repos.TableNoTracking.Include(x => x.VatClass).ThenInclude(y => y.VatClassSchemeRates).Where(z => z.ProjectId == projectId && z.Type == type).ToListAsync();

            }
            else
            {
                return await _repos.TableNoTracking.Where(z => z.ProjectId == projectId).ToListAsync();
            }

        }



        public Task<IPagedList<ProjectFunctionEntity>> GetAll(List<Predicate<ProjectFunctionEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public override async Task<ProjectFunctionEntity> GetById(object id)
        {
            return await _repos.TableNoTracking
                 .Include(x => x.ProjectFunctionGroup)
                 .FirstOrDefaultAsync(x => x.Id == (int)id);
        }


    }

}
