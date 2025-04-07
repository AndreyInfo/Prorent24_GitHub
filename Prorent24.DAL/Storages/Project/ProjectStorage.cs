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
    internal class ProjectStorage : BaseStorage<ProjectEntity>, IProjectStorage
    {
        public ProjectStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public Task<IPagedList<ProjectEntity>> GetAll(List<Predicate<ProjectEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IPagedList<ProjectEntity>> GetAllByFilter(IQueryable<ProjectEntity> queryableEntity, string searchText)
        {
            var projects = await queryableEntity
                .Include(c => c.ClientContact)
                .Include(c => c.ClientContactPerson)
                .Include(l => l.LocationContact)
                .Include(t => t.Times)
                .Include(t => t.CrewMember)
                .ThenInclude(t => t.User)
                .Include(c => c.Type)
                .Include(x => x.CrewMembers).ThenInclude(x => x.CrewMember)
                .ToListAsync();
            if (searchText.Length > 0)
            {
                projects = projects.Where(x =>
                 x.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                || x.ClientContact?.FirstName?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                || x.ClientContact?.LastName?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                || x.ClientContactPerson?.FirstName?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                || x.ClientContactPerson?.LastName?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                || x.CrewMember?.CompanyName?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                || x.CrewMember?.User?.FirstName?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                || x.CrewMember?.User?.LastName?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                || x.CrewMember?.User?.Email?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                || x.CrewMember?.User?.PhoneNumber?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
               ).ToList();
            }
            return projects.ToPagedList(0, 500);
        }

        public override async Task<ProjectEntity> GetById(object id)
        {
            return await _repos.Table
                .Include(x => x.Notes)
                .Include(x => x.Tags).ThenInclude(y => y.TagDirectory)
                .Include(x => x.Tasks)
                .Include(x => x.Files)
                //.Include(x => x.Type)
                .Include(x => x.ClientContact)
                .Include(x => x.ClientContactPerson)
                .Include(x => x.LocationContact)
                .Include(x => x.LocationContactPerson)
                .Include(x => x.CrewMember)
                .Include(x => x.Times)
                .Include(x => x.CrewMembers).ThenInclude(x => x.CrewMember)
                .FirstOrDefaultAsync(x => x.Id == (int)id);
        }

        public async Task<List<ProjectEntity>> GetByIds(List<int> ids)
        {
            return await _repos.TableNoTracking.Include(x => x.Times)
             .Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}
