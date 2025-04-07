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

namespace Prorent24.DAL.Storages.Maintenance.Repair
{
    internal class RepairStorage : BaseStorage<RepairEntity>, IRepairStorage
    {
        public RepairStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async override Task<RepairEntity> GetById(object id)
        {
            return await _repos.Table
                .Include(x => x.Equipment)
                .Include(x => x.Notes)
                .Include(x => x.Tags).ThenInclude(y => y.TagDirectory)
                .Include(x => x.Tasks)
                .Include(x => x.Files)
                .Include(x=>x.ExternalRepair)
                .Include(x=>x.AssignTo)
                .Include(x=>x.ReportedBy)
                .FirstOrDefaultAsync(x => x.Id == (int)id);
        }

        public async Task<IPagedList<RepairEntity>> GetAll(List<Predicate<RepairEntity>> conditions = null)
        {
            return await _repos.TableNoTracking
                    .Include(x => x.Equipment)
                    .Include(x => x.AssignTo)
                    .Include(x => x.ExternalRepair)
                    .Include(x => x.SerialNumber)
                    .ToPagedListAsync(0, 500);
        }

        public async Task<IPagedList<RepairEntity>> GetAllByFilter(List<SelectedFilter> filterList)
        {
            var reposQuery = _repos.TableNoTracking
            .Include(x => x.Equipment)
            .Include(x => x.AssignTo)
            .Include(x => x.ExternalRepair)
            .Include(x => x.SerialNumber).AsQueryable();

            string searchText= string.Empty;
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
            List<RepairEntity> result = await reposQuery.ToListAsync();
            if (searchText.Length > 0)
            {
                result = (from item in result
                          where EF.Functions.Like(item.Equipment.Name, $"%{searchText}%") ||
                                EF.Functions.Like(item.Remark, $"%{searchText}%")
                          select item).ToList();
            }
            return result.OrderByDescending(x => x.CreationDate).ToPagedList(0, 500);
        }
    }
}
