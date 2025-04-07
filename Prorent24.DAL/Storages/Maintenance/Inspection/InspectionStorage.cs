using Microsoft.EntityFrameworkCore;
using Prorent24.Common.Models.Filters;
using Prorent24.Entity.Maintenance;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Maintenance.Inspection
{
    internal class InspectionStorage : BaseStorage<InspectionEntity>, IInspectionStorage
    {
        public InspectionStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IPagedList<InspectionEntity>> GetAll(List<Predicate<InspectionEntity>> conditions = null)
        {
            return await _repos.TableNoTracking
               .Include(x => x.PeriodicInspection)
                .ToPagedListAsync(0, 500);
        }

        public async Task<IPagedList<InspectionEntity>> GetAllByFilter(List<SelectedFilter> filterList)
        {
            // var result = _repos.GetPagedListAsync(x => x, include: i => i.Include(x => x.PeriodicInspection));

            var reposQuery = _repos.TableNoTracking.Include(x => x.PeriodicInspection).AsQueryable();
            string searchText = string.Empty;
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
                                    searchText = values.FirstOrDefault();
                                }
                                break;
                            }
                        case FilterEnum.Tags:
                            {
                                if (filter.Values?.Count > 0)
                                {
                                    reposQuery = reposQuery.Include(x => x.Tags).ThenInclude(y => y.TagDirectory);

                                    if (filter.SelectedAll)
                                    {
                                        reposQuery = reposQuery.Where(x => filter.Values.Any(i => (int)i == x.Id));
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
            List<InspectionEntity> result = await reposQuery.ToListAsync();
            if (searchText.Length > 0)
            {
                result = result.Where(x =>
                 x.Description?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                ).ToList();
            }
            return result.OrderByDescending(x => x.CreationDate).ToPagedList(0, 500);
        }

        public async override Task<InspectionEntity> GetById(object id)
        {
            var result = _repos.GetFirstOrDefault(x => x,
                include: i => i.Include(x => x.PeriodicInspection)
                    .Include(x => x.Notes)
                    .Include(x => x.Tags).ThenInclude(y => y.TagDirectory)
                    .Include(x => x.Tasks)
                    .Include(x => x.Files),
                predicate: y => y.Id == (int)id);


            return result;
        }
    }
}
