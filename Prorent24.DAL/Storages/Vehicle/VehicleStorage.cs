using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prorent24.Common.Models.Filters;
using Prorent24.Entity.Vehicle;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.DAL.Storages.Vehicle
{
    internal class VehicleStorage : BaseStorage<VehicleEntity>, IVehicleStorage
    {
        public VehicleStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        public async override Task<VehicleEntity> GetById(object id)
        {
            return await _repos.Table
                .Include(x => x.Folder)
                .Include(x => x.CrewMembers).ThenInclude(x => x.CrewMember)
                .FirstOrDefaultAsync(x => x.Id == (int)id);
        }

        public async Task<VehicleEntity> GetDetails(int id)
        {
            return await _repos.Table
                                    .Include(x => x.Folder)
                                    .Include(x => x.Notes)
                                    .Include(x => x.Tags).ThenInclude(y => y.TagDirectory)
                                    .Include(x => x.Tasks)
                                    .Include(x => x.Files)
                                    .Include(x => x.CrewMembers).ThenInclude(x => x.CrewMember)
                                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Get list VehicleEntities
        /// </summary>
        /// <returns></returns>
        public Task<IPagedList<VehicleEntity>> GetAll(List<Predicate<VehicleEntity>> conditions = null)
        {
            return _repos.GetPagedListAsync<VehicleEntity>(x => x);
        }

        public async Task<IPagedList<VehicleEntity>> GetAllByFilter(List<SelectedFilter> filterList, string userId)
        {
            IQueryable<VehicleEntity> reposQuery = _repos.TableNoTracking.Include(x => x.CrewMembers).ThenInclude(x => x.CrewMember)
                .Include(x => x.Folder)
                .Where(x => (x.CrewMembers != null && x.CrewMembers.Any(c => c.CrewMemberId == userId))
                           || (x.IsPublic != null && x.IsPublic == true)
                           || x.CreationUserId == userId
                           );

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
                                    reposQuery = reposQuery.Where(x => values.Contains(x.Name.ToLower()) ||
                                                                        values.Contains(x.RegistrationNumber.ToLower()) ||
                                                                        values.Contains(x.Description.ToLower()));
                                }
                                break;
                            }
                        case FilterEnum.Folders:
                            {
                                reposQuery = reposQuery.Where(x => filter.Values.Any(i => Convert.ToInt32(i) == x.FolderId));
                                break;
                            }
                        case FilterEnum.Tags:
                            {
                                if (filter.Values?.Count > 0)
                                {
                                    reposQuery = reposQuery.Include(x => x.Tags).ThenInclude(y => y.TagDirectory);

                                    if (filter.SelectedAll)
                                    {
                                        // reposQuery = reposQuery.Where(x => filter.Values.Any(i => Convert.ToInt32(i) == x.Id));
                                    }
                                    else
                                    {
                                        reposQuery = reposQuery.Where(x => x.Tags.Any(t => filter.Values.Contains(t.TagDirectoryId)));
                                    }
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

            return await reposQuery.OrderByDescending(x => x.CreationDate).ToPagedListAsync(0, 500);
        }

        public async Task<List<VehicleEntity>> GetForPlanning(string userId)
        {
            return await _repos.TableNoTracking
                               .Include(x=>x.Folder)
                .Where(x => x.DisplayInPlanner && ((x.CrewMembers != null && x.CrewMembers.Any(c => c.CrewMemberId == userId))
                           || (x.IsPublic != null && x.IsPublic == true)
                           || x.CreationUserId == userId)
                           ).ToListAsync();
        }

        public async Task<List<VehicleEntity>> GetByIds(List<int> ids)
        {
            return await _repos.TableNoTracking.Include(x => x.CrewMembers).ThenInclude(x => x.CrewMember).Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<IPagedList<VehicleEntity>> GetAllByFilter(IQueryable<VehicleEntity> queryableEntity, string searchText, string userId)
        {
            var result = await queryableEntity.Include(x => x.Folder)
               .Include(x => x.CrewMembers).ThenInclude(x => x.CrewMember)
               .Where(x => (x.CrewMembers != null && x.CrewMembers.Any(c => c.CrewMemberId == userId))
                           || (x.IsPublic != null && x.IsPublic == true)
                           || x.CreationUserId == userId)
               .ToListAsync();

            if (searchText?.Length > 0)
            {
                result = result.Where(x =>
                    x.Name?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
             ).ToList();

            }
            return result.ToPagedList(0, 500);
        }
    }
}
