using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.CrewPlanner;
using Prorent24.Enum.Project;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.CrewPlanner
{
    internal class CrewPlannerStorage : BaseStorage<CrewPlannerEntity>, ICrewPlannerStorage
    {
        public CrewPlannerStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public Task<IPagedList<CrewPlannerEntity>> GetAll(List<Predicate<CrewPlannerEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CrewPlannerEntity>> GetAll(ProjectFunctionTypeEnum type, List<string> ids)
        {
            if(type == ProjectFunctionTypeEnum.Crew)
            {
                return await _repos.TableNoTracking
                    .Include(x=>x.CrewMember).ThenInclude(y=>y.User).
                    Where(x=>x.VehicleId == null && ids.Contains(x.CrewMemberId)).ToListAsync();
            }
            else
            {
                return await _repos.TableNoTracking
                    .Include(x => x.Vehicle).
                    Where(x => x.CrewMemberId == null && ids.Contains(x.VehicleId.ToString())).ToListAsync();
            }
        }

        public async Task<List<CrewPlannerEntity>> GetAllForModuleTimeLine(DateTime from, DateTime until, ProjectFunctionTypeEnum type, List<string> ids)
        {
            if (type == ProjectFunctionTypeEnum.Crew)
            {
                return await _repos.TableNoTracking
                    .Include(x => x.CrewMember).ThenInclude(y => y.User).
                    Where(x => x.VehicleId == null && ids.Contains(x.CrewMemberId) 
                    && ((from >= x.From && from <= x.Until) || (until >= x.From && until <= x.Until))
                    ).ToListAsync();
            }
            else
            {
                return await _repos.TableNoTracking
                    .Include(x => x.Vehicle).
                    Where(x => x.CrewMemberId == null && ids.Contains(x.VehicleId.ToString())
                    && ((from >= x.From && from <= x.Until) || (until >= x.From && until <= x.Until))
                    ).ToListAsync();
            }
        }

        public async Task<List<CrewPlannerEntity>> GetAllForTimeLine(IQueryable<CrewPlannerEntity> queryableEntity, ProjectFunctionTypeEnum type, List<string> ids)
        {
            if (type == ProjectFunctionTypeEnum.Crew)
            {
                return await queryableEntity
                    .Include(x => x.CrewMember).ThenInclude(y => y.User).
                    Where(x => x.VehicleId == null && ids.Contains(x.CrewMemberId)).ToListAsync();
            }
            else
            {
                return await queryableEntity
                    .Include(x => x.Vehicle).
                    Where(x => x.CrewMemberId == null && ids.Contains(x.VehicleId.ToString())).ToListAsync();
            }
        }

        public async Task<List<CrewPlannerEntity>> GetByEntityId(CrewPlannerEntity entity)
        {
            if (entity.VehicleId != null)
            {
                return await _repos.TableNoTracking
                 .Where(x => x.VehicleId == entity.VehicleId).ToListAsync();
            }
            else
            {
                return await _repos.TableNoTracking
                   .Where(x => x.CrewMemberId == entity.CrewMemberId).ToListAsync();
            }
        }

    }
}
