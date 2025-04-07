using Microsoft.EntityFrameworkCore;
using Prorent24.Common.Models.Filters;
using Prorent24.Entity.TimeRegistration;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.TimeRegistration
{
    internal class TimeRegistrationStorage : BaseStorage<TimeRegistrationEntity>, ITimeRegistrationStorage
    {
        public TimeRegistrationStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IPagedList<TimeRegistrationEntity>> GetAll(List<Predicate<TimeRegistrationEntity>> conditions = null)
        {
            return _repos.GetPagedListAsync<TimeRegistrationEntity>(x => x,null,null,i=>i.Include(u=>u.CreationUser));
        }

        public override async Task<TimeRegistrationEntity> GetById(object id)
        {
            return await _repos.Table.Include(x => x.Activities)
                                     .Include(x=>x.CreationUser)
                                     .Include(x => x.CrewMember).ThenInclude(x=>x.User)
                                     .FirstOrDefaultAsync(x => x.Id == (int)id);

        }

        public IQueryable<TimeRegistrationEntity> GetAllByFilter(List<SelectedFilter> filterList)
        {
            var reposQuery = _repos.TableNoTracking;

            foreach (SelectedFilter filter in filterList)
            {
                if (filter.Values != null && filter.Values.Any())
                {
                    FilterEnum filterEnum = (FilterEnum)System.Enum.Parse(typeof(FilterEnum), filter.FilterType);
                    switch (filterEnum)
                    {
                        case FilterEnum.SearchText:
                            {
                                List<string> values = filter.Values.Select(x => x != null ? x.ToString().ToLower() : "").Where(x => !String.IsNullOrWhiteSpace(x.ToString())).ToList();

                                if (values != null && values.Count > 0)
                                {
                                }
                                break;
                            }
                        case FilterEnum.AddedFilters:
                            {

                                break;
                            }
                    }
                }
            }

            return reposQuery.Include(x=>x.CrewMember).ThenInclude(u=>u.User).OrderByDescending(x => x.CreationDate);
        }

        public async Task<IPagedList<TimeRegistrationEntity>> GetAllByFilter(IQueryable<TimeRegistrationEntity> queryableEntity, string searchText)
        {
            var result = await queryableEntity.Include(x => x.CrewMember)
                .ThenInclude(u => u.User)
                .ToListAsync();

            if (searchText?.Length > 0)
            {
                result = result.Where(x =>
                    x.CrewMember?.CompanyName?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                  || x.CrewMember?.User?.FirstName?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                  || x.CrewMember?.User?.LastName?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
             ).ToList();

            }
            return result.ToPagedList(0, 500);
        }
    }
}
