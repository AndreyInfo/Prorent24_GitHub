using Microsoft.EntityFrameworkCore;
using Prorent24.Common.Models.Filters;
using Prorent24.Entity.CrewMember;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.CrewMember
{
    internal class CrewMemberStorage : BaseStorage<CrewMemberEntity>, ICrewMemberStorage
    {
        private readonly IRepository<CrewMemberEntity> repository;
        public CrewMemberStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async override Task<CrewMemberEntity> GetById(object id)
        {
            return await _repos.Table
                .Include(x => x.Folder)
                .Include(x => x.User)
                .ThenInclude(x=>x.Roles)
                .Include(x => x.Address)
                .Include(x => x.Notes)
                .Include(x => x.Tags).ThenInclude(x => x.TagDirectory)
                .Include(x => x.Tasks).ThenInclude(x => x.CompleatedByMember)
                .Include(x => x.Tasks).ThenInclude(x => x.CrewMembers)
                .Include(x => x.Tasks).ThenInclude(x => x.CreationUser)
                .Include(x => x.Files)
                .FirstOrDefaultAsync(x => x.UserId == id.ToString());
        }

        public async Task<IPagedList<CrewMemberEntity>> GetAllByFilter(IQueryable<CrewMemberEntity> queryableEntity, string searchText)
        {
            var result = await queryableEntity.Include(x => x.Folder).Include(x => x.User).Include(x => x.Address).ToListAsync();
            if (searchText.Length > 0)
            {
                result = result.Where(x =>
                  x.User?.FirstName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
               || x.User?.LastName?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
               || x.User?.MiddleName?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
               || x.User?.Email?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
               || x.User?.PhoneNumber?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
             ).ToList();
                
            }
            return result.ToPagedList(0,500);
        }

        public async Task<List<CrewMemberEntity>> GetList()
        {
            return await _repos.Table.Include(x => x.User).ToListAsync(); // ThenInclude(x=>x.Roles).ThenInclude(x=>x.Role).
        }

        public override Task<CrewMemberEntity> Update(CrewMemberEntity model)
        {
            return base.Update(model);
        }

        public Task<IPagedList<CrewMemberEntity>> GetAll(List<Predicate<CrewMemberEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CrewMemberEntity>> GetCrewMembers()
        {
            return await _repos.TableNoTracking.Include(x => x.User).Include(y => y.Folder).Where(x=>x.DisplayInPlanner).ToListAsync();

        }

        public bool ExistsByRoleId(string roleId)
        {
            var result = _repos.Table.Include(x => x.User).ThenInclude(x => x.Roles).Where(x => x.User.Roles.Any(r => r.RoleId == roleId));
            return result.Any();
        }

        public async Task<List<CrewMemberEntity>> GetList(List<string> ids)
        {
            return await _repos.Table.Include(x => x.User).Where(x => ids.Contains(x.UserId)).ToListAsync();
        }
    }
}
