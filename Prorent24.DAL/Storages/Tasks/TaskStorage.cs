using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Prorent24.Common.Models.Filters;
using Prorent24.Entity.Tasks;
using Prorent24.Enum.Directory;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.DAL.Storages.Tasks
{
    internal class TaskStorage : BaseStorage<TaskEntity>, ITaskStorage
    {
        public TaskStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IPagedList<TaskEntity>> GetAll(List<Predicate<TaskEntity>> conditions = null)
        {
            return _repos.GetPagedListAsync<TaskEntity>(x => x);
        }

        public async Task<List<TaskEntity>> GetAllByCrewMember(string crewMemberId)
        {
            return _repos.TableNoTracking
                .Include(x => x.CrewMembers)
                .Where(x => (x.CrewMembers != null && x.CrewMembers.Any(c => c.CrewMemberId == crewMemberId))
                || x.Executors != null && x.Executors.Any(c => c.CrewMemberId == crewMemberId)
                || x.IsPublic || x.CreationUserId == crewMemberId).ToList();
        }

        //public IQueryable<TaskEntity> GetAllByFilter(List<SelectedFilter> filterList)
        //{
        //    IQueryable<TaskEntity> reposQuery = _repos.TableNoTracking
        //        .Include(x => x.CrewMembers).ThenInclude(x => x.CrewMember)
        //        .Include(x => x.Executors).ThenInclude(x => x.CrewMember);

        //    foreach (SelectedFilter filter in filterList)
        //    {
        //        if (filter.Values != null && filter.Values.Any())
        //        {
        //            FilterEnum filterEnum = (FilterEnum)System.Enum.Parse(typeof(FilterEnum), filter.FilterType);

        //            switch (filterEnum)
        //            {
        //                case FilterEnum.SearchText:
        //                    {
        //                        List<string> values = filter.Values.Select(x => x != null ? x.ToString().ToLower() : "").Where(x => !String.IsNullOrWhiteSpace(x.ToString())).ToList();

        //                        if (values != null && values.Count > 0)
        //                        {
        //                            reposQuery = reposQuery.Where(x => values.Contains(x.Task.ToLower()) ||
        //                                                                values.Contains(x.Description.ToLower()));
        //                        }

        //                        break;
        //                    }

        //                case FilterEnum.Tags:
        //                    {
        //                        if (filter.Values?.Count > 0)
        //                        {
        //                            reposQuery = reposQuery.Include(x => x.Tags).ThenInclude(y => y.TagDirectory);

        //                            if (filter.SelectedAll)
        //                            {
        //                                // reposQuery = reposQuery.Where(x => filter.Values.Any(i => i.ToString() == x.UserId));
        //                            }
        //                            else
        //                            {
        //                                reposQuery = reposQuery.Where(x => x.Tags.Any(t => filter.Values.Contains(t.Id)));
        //                            }
        //                        }

        //                        break;
        //                    }
        //                case FilterEnum.AddedFilters:
        //                    {

        //                        break;
        //                    }
        //            }
        //        }
        //    }

        //    return reposQuery;
        //}

        public async Task<IPagedList<TaskEntity>> GetAllByFilter(IQueryable<TaskEntity> queryableEntity, string userId, string searchText)
        {
            List<TaskEntity> listEntity = await queryableEntity
               .Include(x => x.CrewMembers).ThenInclude(x => x.CrewMember)
               .Include(x => x.Executors).ThenInclude(x => x.CrewMember).Where(x =>
             (x.CrewMembers != null && x.CrewMembers.Any(c => c.CrewMemberId == userId))
             || (x.Executors != null && x.Executors.Any(c => c.CrewMemberId == userId))
             || x.IsPublic || x.CreationUserId == userId).ToListAsync();
            if (searchText.Length > 0)
            {
                listEntity = listEntity.Where(x =>
                 x.Task?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                || x.Description?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
               ).ToList();
            }
            return listEntity.ToPagedList(0, 500);
        }
        public async override Task<TaskEntity> GetById(object id)
        {
            //var table = _repos.Table.Include(x => x.CrewMembers);

            return await _repos.Table
                .Include(x => x.CrewMembers).ThenInclude(x => x.CrewMember)
                .Include(x => x.Executors).ThenInclude(x => x.CrewMember)
                .Include(x => x.CreationUser)
                .Where(x => x.Id == (int)id).Select(x => x).FirstOrDefaultAsync();
        }

        public async Task<TaskEntity> GetDetails(int id)
        {
            return await _repos.Table
                                    .Include(x => x.Tags).ThenInclude(y => y.TagDirectory)
                                    .Include(x => x.Notes)
                                    .Include(x => x.Files)
                                    .Include(x => x.CreationUser)
                                    .Include(x => x.CrewMember)
                                    .ThenInclude(x => x.User)
                                    .Include(x => x.Vehicle)
                                    .Include(x => x.Equipment)
                                    .Include(x => x.Contact)
                                    .Include(x => x.CrewMembers).ThenInclude(x => x.CrewMember)
                                    .Include(x => x.Executors).ThenInclude(x => x.CrewMember)
                                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async override Task<TaskEntity> Update(TaskEntity model)
        {
            TaskEntity task = await GetDetails(model.Id);
            task.DeadLine = model.DeadLine;
            task.Task = model.Task;
            task.IsPublic = model.IsPublic;
            task.CompleatedByMember = model.CompleatedByMember;
            task.CompleatedBy = model.CompleatedBy;
            task.CompletedDate = model.CompletedDate;
            task.Description = model.Description;

            return await base.Update(task);
        }
    }
}
