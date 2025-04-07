using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models.General.Period;
using Prorent24.Common.Models.Filters;
using Prorent24.Entity.Contact;
using Prorent24.Entity.CrewMember;
using Prorent24.Entity.CrewPlanner;
using Prorent24.Entity.Equipment;
using Prorent24.Entity.Invoice;
using Prorent24.Entity.Project;
using Prorent24.Entity.Subhire;
using Prorent24.Entity.Tasks;
using Prorent24.Entity.TimeRegistration;
using Prorent24.Entity.Vehicle;
using Prorent24.Enum.Directory;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Builders
{
    public static class FilterBuilder
    {

        public static IQueryable<VehicleEntity> CreateFilterForVehicleEntity(this IQueryable<VehicleEntity> queryableEntity, List<SelectedFilter> filterList, out string searchText)
        {
            searchText = "";
            if (filterList != null)
            {
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
                                        searchText = values.FirstOrDefault().ToLower();
                                    }

                                    break;
                                }
                            case FilterEnum.Folders:
                                {
                                    queryableEntity = queryableEntity.Where(x => filter.Values.Any(i => Convert.ToInt32(i) == x.FolderId));
                                    break;
                                }
                            case FilterEnum.Tags:
                                {
                                    if (filter.Values?.Count > 0)
                                    {
                                        queryableEntity = queryableEntity.Include(x => x.Tags).ThenInclude(y => y.TagDirectory);

                                        if (filter.SelectedAll)
                                        {
                                            queryableEntity = queryableEntity.Where(x => filter.Values.Any(i => i.ToString() == x.CreationUserId));
                                        }
                                        else
                                        {
                                            queryableEntity = queryableEntity.Where(x => x.Tags.Any(t => filter.Values.Contains(t.TagDirectoryId)));
                                        }
                                    }

                                    break;
                                }
                        }
                    }
                }
            }

            return queryableEntity.OrderByDescending(x => x.CreationDate);
        }

        public static IQueryable<TimeRegistrationEntity> CreateFilterForTimeRegisterEntity(this IQueryable<TimeRegistrationEntity> queryableEntity, List<SelectedFilter> filterList, out string searchText)
        {
            searchText = "";
            if (filterList != null)
            {
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
                                        searchText = values.FirstOrDefault().ToLower();
                                    }

                                    break;
                                }
                            case FilterEnum.Period:
                                {
                                    if (filter.Values != null && filter.Values.Any())
                                    {
                                        PeriodDto period = JsonConvert.DeserializeObject<PeriodDto>(filter.Values[0].ToString());

                                        queryableEntity = queryableEntity.FilterByPeriod(period);
                                    }
                                    break;
                                }
                        }
                    }
                }
            }

            return queryableEntity.OrderByDescending(x => x.CreationDate);
        }

        public static IQueryable<EquipmentEntity> CreateFilterForEquipmentEntity(this IQueryable<EquipmentEntity> queryableEntity, List<SelectedFilter> filterList, out string searchText)
        {
            searchText = "";
            if (filterList != null)
            {
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
                                        searchText = values.FirstOrDefault().ToLower();
                                    }

                                    break;
                                }
                            case FilterEnum.Folders:
                                {
                                    queryableEntity = queryableEntity.Where(x => x.FolderId.HasValue && filter.Values.Any(i => Convert.ToInt32(i) == x.FolderId.Value));
                                    break;
                                }
                            case FilterEnum.Tags:
                                {
                                    if (filter.Values?.Count > 0)
                                    {
                                        queryableEntity = queryableEntity.Include(x => x.Tags).ThenInclude(y => y.TagDirectory);

                                        if (filter.SelectedAll)
                                        {
                                            queryableEntity = queryableEntity.Where(x => filter.Values.Any(i => i.ToString() == x.CreationUserId));
                                        }
                                        else
                                        {
                                            queryableEntity = queryableEntity.Where(x => x.Tags.Any(t => filter.Values.Contains(t.TagDirectoryId)));
                                        }
                                    }

                                    break;
                                }
                        }
                    }
                }
            }

            return queryableEntity.OrderByDescending(x => x.CreationDate);
        }


        /// <summary>
        /// Filters by ProjectEntity
        /// </summary>
        /// <param name="filterList"></param>
        /// <param name="queryableEntity"></param>
        /// <returns></returns>
        public static IQueryable<InvoiceEntity> CreateFilterForInvoiceEntity(this IQueryable<InvoiceEntity> queryableEntity, List<SelectedFilter> filterList, out string searchText)
        {
            searchText = "";
            if (filterList != null)
            {
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
                                        searchText = values.FirstOrDefault().ToLower();
                                    }

                                    break;
                                }
                            case FilterEnum.Period:
                                {
                                    if (filter.Values != null && filter.Values.Count > 0)
                                    {
                                        PeriodDto period = JsonConvert.DeserializeObject<PeriodDto>(filter.Values[0].ToString());

                                        if (period != null)
                                        {
                                            if (period.PeriodType == PeriodTypeEnum.Period)
                                            {
                                                DateTime date = DateTime.UtcNow;
                                                DateTime dateFrom = DateTime.UtcNow;
                                                DateTime dateTo = DateTime.UtcNow;
                                                if (period.DurationTime == DurationTimeEnum.Next)
                                                {
                                                    dateFrom = date;
                                                    dateTo = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, period.Duration);
                                                }
                                                else if (period.DurationTime == DurationTimeEnum.Past)
                                                {
                                                    dateFrom = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, -period.Duration);
                                                    dateTo = date;
                                                }

                                                queryableEntity = queryableEntity.Where(t => t.Date >= dateFrom.Date && t.Date <= dateTo);


                                            }
                                            else if (period.PeriodType == PeriodTypeEnum.FromToUntil)
                                            {
                                                if (period.FromDate.HasValue && period.FromDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Where(t => t.Date >= period.FromDate.Value.Date);
                                                }

                                                if (period.ToDate.HasValue && period.ToDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Where(t => t.Date <= period.ToDate.Value.Date);
                                                }
                                            }
                                        }
                                    }

                                    break;
                                }
                            case FilterEnum.Tags:
                                {
                                    if (filter.Values?.Count > 0)
                                    {
                                        queryableEntity = queryableEntity.Include(x => x.Tags).ThenInclude(y => y.TagDirectory);

                                        if (filter.SelectedAll)
                                        {
                                            queryableEntity = queryableEntity.Where(x => filter.Values.Any(i => i.ToString() == x.CreationUserId));
                                        }
                                        else
                                        {
                                            queryableEntity = queryableEntity.Where(x => x.Tags.Any(t => filter.Values.Contains(t.TagDirectoryId)));
                                        }
                                    }

                                    break;
                                }
                        }
                    }
                }
            }

            return queryableEntity.OrderByDescending(x => x.CreationDate);
        }
        /// <summary>
        /// Filters by ProjectEntity
        /// </summary>
        /// <param name="filterList"></param>
        /// <param name="queryableEntity"></param>
        /// <returns></returns>
        public static IQueryable<ProjectEntity> CreateFilterForProjectEntity(this IQueryable<ProjectEntity> queryableEntity, List<SelectedFilter> filterList, out string searchText)
        {
            searchText = "";
            if (filterList != null)
            {
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
                                        searchText = values.FirstOrDefault().ToLower();
                                    }

                                    break;
                                }
                            case FilterEnum.Period:
                                {
                                    if (filter.Values != null && filter.Values.Count > 0)
                                    {
                                        PeriodDto period = JsonConvert.DeserializeObject<PeriodDto>(filter.Values[0].ToString());

                                        if (period != null)
                                        {
                                            if (period.PeriodType == PeriodTypeEnum.Period)
                                            {
                                                DateTime date = DateTime.UtcNow;
                                                DateTime dateFrom = DateTime.UtcNow;
                                                DateTime dateTo = DateTime.UtcNow;
                                                if (period.DurationTime == DurationTimeEnum.Next)
                                                {
                                                    dateFrom = date;
                                                    dateTo = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, period.Duration);
                                                }
                                                else if (period.DurationTime == DurationTimeEnum.Past)
                                                {
                                                    dateFrom = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, -period.Duration);
                                                    dateTo = date;
                                                }

                                                queryableEntity = queryableEntity.Include(x => x.Times).Where(x => x.Times.Any(t => t.From >= dateFrom.Date && t.Until <= dateTo));


                                            }
                                            else if (period.PeriodType == PeriodTypeEnum.FromToUntil)
                                            {
                                                if (period.FromDate.HasValue && period.FromDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Where(x => x.Times.Any(t => t.From.Date >= period.FromDate.Value.Date));
                                                }

                                                if (period.ToDate.HasValue && period.ToDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Where(x => x.Times.Any(t => t.Until.Date <= period.ToDate.Value.Date));
                                                }
                                            }
                                        }
                                    }

                                    break;
                                }
                            case FilterEnum.Tags:
                                {
                                    if (filter.Values?.Count > 0)
                                    {
                                        queryableEntity = queryableEntity.Include(x => x.Tags).ThenInclude(y => y.TagDirectory);

                                        if (filter.SelectedAll)
                                        {
                                            queryableEntity = queryableEntity.Where(x => filter.Values.Any(i => i.ToString() == x.CreationUserId));
                                        }
                                        else
                                        {
                                            queryableEntity = queryableEntity.Where(x => x.Tags.Any(t => filter.Values.Contains(t.TagDirectoryId)));
                                        }
                                    }

                                    break;
                                }
                        }
                    }
                }
            }

            return queryableEntity.OrderByDescending(x => x.CreationDate);
        }

        /// <summary>
        /// Filters by CrewMemberEntity
        /// </summary>
        /// <param name="filterList"></param>
        /// <param name="queryableEntity"></param>
        /// <returns></returns>
        public static IQueryable<CrewMemberEntity> CreateFilterForCrewMemberEntity(this IQueryable<CrewMemberEntity> queryableEntity, List<SelectedFilter> filterList, out string searchText)
        {
            searchText = String.Empty;
            if (filterList != null)
            {
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
                            case FilterEnum.Folders:
                                {
                                    queryableEntity = queryableEntity.Where(x => x.FolderId.HasValue && filter.Values.Any(i => Convert.ToInt32(i) == x.FolderId.Value));
                                    break;
                                }
                            case FilterEnum.Tags:
                                {
                                    if (filter.Values?.Count > 0)
                                    {
                                        queryableEntity = queryableEntity.Include(x => x.Tags).ThenInclude(y => y.TagDirectory);

                                        if (filter.SelectedAll)
                                        {
                                            queryableEntity = queryableEntity.Where(x => filter.Values.Any(i => i.ToString() == x.UserId));
                                        }
                                        else
                                        {
                                            queryableEntity = queryableEntity.Where(x => x.Tags.Any(t => filter.Values.Contains(t.TagDirectoryId)));
                                        }
                                    }

                                    break;
                                }
                        }
                    }
                }
            }

            return queryableEntity.Where(x => !x.User.Removed).OrderByDescending(x => x.CreationDate);
        }


        /// <summary>
        /// Filters by ProjectEquipmentEntity
        /// </summary>
        /// <param name="filterList"></param>
        /// <param name="queryableEntity"></param>
        /// <returns></returns>
        public static IQueryable<ProjectEquipmentEntity> CreateFilterForProjectEquipmentEntity(this IQueryable<ProjectEquipmentEntity> queryableEntity, List<SelectedFilter> filterList, out string searchText)
        {
            searchText = string.Empty;
            if (filterList != null)
            {
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
                            case FilterEnum.Period:
                                {
                                    if (filter.Values != null && filter.Values.Count > 0)
                                    {
                                        PeriodDto period = JsonConvert.DeserializeObject<PeriodDto>(filter.Values[0].ToString());

                                        if (period != null)
                                        {
                                            if (period.PeriodType == PeriodTypeEnum.Period)
                                            {
                                                DateTime date = DateTime.UtcNow;

                                                if (period.DurationTime == DurationTimeEnum.Next)
                                                {
                                                    date = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, period.Duration);
                                                }
                                                else if (period.DurationTime == DurationTimeEnum.Past)
                                                {
                                                    date = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, -period.Duration);
                                                }

                                                queryableEntity = queryableEntity.Where(x => x.ProjectEquipmentGroup.StartUsePeriod >= date.Date && x.ProjectEquipmentGroup.StartUsePeriod <= date.Date);
                                            }
                                            else if (period.PeriodType == PeriodTypeEnum.FromToUntil)
                                            {
                                                if (period.FromDate.HasValue && period.FromDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Where(x => x.ProjectEquipmentGroup.StartUsePeriod >= period.FromDate.Value.Date);
                                                }

                                                if (period.ToDate.HasValue && period.ToDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Where(x => x.ProjectEquipmentGroup.StartUsePeriod <= period.ToDate.Value.Date);
                                                }
                                            }
                                        }
                                    }

                                    break;
                                }
                        }
                    }
                }
            }

            return queryableEntity;

        }

        /// <summary>
        /// Filters by SubhireEquipmentEntity
        /// </summary>
        /// <param name="filterList"></param>
        /// <param name="queryableEntity"></param>
        /// <returns></returns>
        public static IQueryable<SubhireEquipmentEntity> CreateFilterForSubhireEquipmentEntity(this IQueryable<SubhireEquipmentEntity> queryableEntity, List<SelectedFilter> filterList)
        {
            if (filterList != null)
            {
                foreach (SelectedFilter filter in filterList)
                {
                    if (filter.Values != null && filter.Values.Any())
                    {
                        FilterEnum filterEnum = (FilterEnum)System.Enum.Parse(typeof(FilterEnum), filter.FilterType);
                        switch (filterEnum)
                        {

                            case FilterEnum.Period:
                                {
                                    if (filter.Values != null && filter.Values.Count > 0)
                                    {
                                        PeriodDto period = JsonConvert.DeserializeObject<PeriodDto>(filter.Values[0].ToString());

                                        if (period != null)
                                        {
                                            if (period.PeriodType == PeriodTypeEnum.Period)
                                            {
                                                DateTime date = DateTime.UtcNow;

                                                if (period.DurationTime == DurationTimeEnum.Next)
                                                {
                                                    date = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, period.Duration);
                                                }
                                                else if (period.DurationTime == DurationTimeEnum.Past)
                                                {
                                                    date = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, -period.Duration);
                                                }

                                                queryableEntity = queryableEntity.Where(x => x.Subhire.UsagePeriodStart >= date.Date && x.Subhire.UsagePeriodStart <= date.Date);
                                            }
                                            else if (period.PeriodType == PeriodTypeEnum.FromToUntil)
                                            {
                                                if (period.FromDate.HasValue && period.FromDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Where(x => x.Subhire.UsagePeriodStart >= period.FromDate.Value.Date);
                                                }

                                                if (period.ToDate.HasValue && period.ToDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Where(x => x.Subhire.UsagePeriodStart <= period.ToDate.Value.Date && x.Subhire.UsagePeriodEnd <= period.ToDate.Value.Date);
                                                }
                                            }
                                        }
                                    }

                                    break;
                                }
                        }
                    }
                }
            }

            return queryableEntity;


        }


        /// <summary>
        /// Filters by GeneralTimeLineProjectFunction
        /// </summary>
        /// <param name="filterList"></param>
        /// <param name="queryableEntity"></param>
        /// <returns></returns>
        public static IQueryable<ProjectFunctionEntity> CreateFilterForGeneralTimeLineProjectFunction(this IQueryable<ProjectFunctionEntity> queryableEntity, List<SelectedFilter> filterList, out string searchText)
        {
            searchText = "";
            if (filterList != null)
            {
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
                                        searchText = values.FirstOrDefault().ToLower();

                                    }

                                    break;
                                }

                            case FilterEnum.Period:
                                {
                                    if (filter.Values != null && filter.Values.Count > 0)
                                    {
                                        PeriodDto period = JsonConvert.DeserializeObject<PeriodDto>(filter.Values[0].ToString());

                                        if (period != null)
                                        {
                                            if (period.PeriodType == PeriodTypeEnum.Period)
                                            {
                                                DateTime date = DateTime.UtcNow;
                                                DateTime dateFrom = DateTime.UtcNow;
                                                DateTime dateTo = DateTime.UtcNow;
                                                if (period.DurationTime == DurationTimeEnum.Next)
                                                {
                                                    dateFrom = date;
                                                    dateTo = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, period.Duration);
                                                }
                                                else if (period.DurationTime == DurationTimeEnum.Past)
                                                {
                                                    dateFrom = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, -period.Duration);
                                                    dateTo = date;
                                                }

                                                queryableEntity = queryableEntity.Include(x => x.ProjectFunctionGroup)
                                                    .Where(x => (x.ProjectFunctionGroup.UseFrom >= dateFrom && x.ProjectFunctionGroup.UseFrom <= dateTo) || (x.ProjectFunctionGroup.UseUntil >= dateFrom && x.ProjectFunctionGroup.UseUntil <= dateTo) || (x.ProjectFunctionGroup.UseFrom <= dateFrom && x.ProjectFunctionGroup.UseUntil >= dateTo));
                                            }
                                            else if (period.PeriodType == PeriodTypeEnum.FromToUntil)
                                            {
                                                if (period.FromDate.HasValue && period.FromDate.Value != DateTime.MinValue && period.ToDate.HasValue && period.ToDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Include(x => x.ProjectFunctionGroup)
                                                        .Where(x => (x.ProjectFunctionGroup.UseFrom >= period.FromDate.Value && x.ProjectFunctionGroup.UseFrom <= period.ToDate.Value) || (x.ProjectFunctionGroup.UseUntil >= period.FromDate.Value && x.ProjectFunctionGroup.UseUntil <= period.ToDate.Value) || (x.ProjectFunctionGroup.UseFrom <= period.FromDate.Value && x.ProjectFunctionGroup.UseUntil > period.ToDate.Value));

                                                }
                                                else if (period.FromDate.HasValue && period.FromDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Include(x => x.ProjectFunctionGroup)
                                                        .Where(x => x.ProjectFunctionGroup.UseFrom >= period.FromDate.Value.Date || (x.ProjectFunctionGroup.UseFrom <= period.FromDate.Value.Date && x.ProjectFunctionGroup.UseUntil > period.FromDate.Value.Date));
                                                }

                                                else if (period.ToDate.HasValue && period.ToDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Include(x => x.ProjectFunctionGroup).Where(x => x.ProjectFunctionGroup.UseUntil <= period.ToDate.Value.Date || (x.ProjectFunctionGroup.UseUntil > period.ToDate.Value.Date && x.ProjectFunctionGroup.UseFrom < period.ToDate.Value.Date));
                                                }
                                            }
                                        }
                                    }

                                    break;
                                }
                        }
                    }
                }
            }

            return queryableEntity;

        }


        /// <summary>
        /// Filters by GeneralTimeLineProjectFunction
        /// </summary>
        /// <param name="filterList"></param>
        /// <param name="queryableEntity"></param>
        /// <returns></returns>
        public static IQueryable<CrewPlannerEntity> CreateFilterForGeneralTimeLineCrewPlanner(this IQueryable<CrewPlannerEntity> queryableEntity, List<SelectedFilter> filterList)
        {
            if (filterList != null)
            {
                foreach (SelectedFilter filter in filterList)
                {
                    if (filter.Values != null && filter.Values.Any())
                    {
                        FilterEnum filterEnum = (FilterEnum)System.Enum.Parse(typeof(FilterEnum), filter.FilterType);
                        switch (filterEnum)
                        {
                            case FilterEnum.Period:
                                {
                                    if (filter.Values != null && filter.Values.Count > 0)
                                    {
                                        PeriodDto period = JsonConvert.DeserializeObject<PeriodDto>(filter.Values[0].ToString());

                                        if (period != null)
                                        {
                                            if (period.PeriodType == PeriodTypeEnum.Period)
                                            {
                                                DateTime date = DateTime.UtcNow;
                                                DateTime dateFrom = DateTime.UtcNow;
                                                DateTime dateTo = DateTime.UtcNow;
                                                if (period.DurationTime == DurationTimeEnum.Next)
                                                {
                                                    dateFrom = date;
                                                    dateTo = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, period.Duration);
                                                }
                                                else if (period.DurationTime == DurationTimeEnum.Past)
                                                {
                                                    dateFrom = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, -period.Duration);
                                                    dateTo = date;
                                                }

                                                queryableEntity = queryableEntity.Where(x => (x.From >= dateFrom && x.From <= dateTo) || (x.Until >= dateFrom && x.Until <= dateTo) || (x.From <= dateFrom && x.Until >= dateTo));
                                            }
                                            else if (period.PeriodType == PeriodTypeEnum.FromToUntil)
                                            {
                                                if (period.FromDate.HasValue && period.FromDate.Value != DateTime.MinValue && period.ToDate.HasValue && period.ToDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Where(x => (x.From >= period.FromDate.Value && x.From <= period.ToDate.Value) || (x.Until >= period.FromDate.Value && x.Until <= period.ToDate.Value) || (x.From <= period.FromDate.Value && x.Until > period.ToDate.Value));

                                                }
                                                else if (period.FromDate.HasValue && period.FromDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Where(x => x.From >= period.FromDate.Value.Date || (x.From <= period.FromDate.Value.Date && x.Until > period.FromDate.Value.Date));
                                                }

                                                else if (period.ToDate.HasValue && period.ToDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Where(x => x.Until <= period.ToDate.Value.Date || (x.Until > period.ToDate.Value.Date && x.From < period.ToDate.Value.Date));
                                                }
                                            }
                                        }
                                    }

                                    break;
                                }
                        }
                    }
                }
            }

            return queryableEntity;

        }


        /// <summary>
        /// Filters by GeneralTimeLineProjectFunction
        /// </summary>
        /// <param name="filterList"></param>
        /// <param name="queryableEntity"></param>
        /// <returns></returns>
        public static IQueryable<ProjectPlanningEntity> CreateFilterForProjectPlanning(this IQueryable<ProjectPlanningEntity> queryableEntity, List<SelectedFilter> filterList)
        {
            if (filterList != null)
            {
                foreach (SelectedFilter filter in filterList)
                {
                    if (filter.Values != null && filter.Values.Any())
                    {
                        FilterEnum filterEnum = (FilterEnum)System.Enum.Parse(typeof(FilterEnum), filter.FilterType);
                        switch (filterEnum)
                        {
                            case FilterEnum.Period:
                                {
                                    if (filter.Values != null && filter.Values.Count > 0)
                                    {
                                        PeriodDto period = JsonConvert.DeserializeObject<PeriodDto>(filter.Values[0].ToString());

                                        if (period != null)
                                        {
                                            if (period.PeriodType == PeriodTypeEnum.Period)
                                            {
                                                DateTime date = DateTime.UtcNow;
                                                DateTime dateFrom = DateTime.UtcNow;
                                                DateTime dateTo = DateTime.UtcNow;
                                                if (period.DurationTime == DurationTimeEnum.Next)
                                                {
                                                    dateFrom = date;
                                                    dateTo = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, period.Duration);
                                                }
                                                else if (period.DurationTime == DurationTimeEnum.Past)
                                                {
                                                    dateFrom = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, -period.Duration);
                                                    dateTo = date;
                                                }

                                                queryableEntity = queryableEntity.Where(x => x.PlanFrom >= dateFrom.Date && x.PlanUntil <= dateTo);
                                            }
                                            else if (period.PeriodType == PeriodTypeEnum.FromToUntil)
                                            {
                                                if (period.FromDate.HasValue && period.FromDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Where(x => x.PlanFrom >= period.FromDate.Value.Date);
                                                }

                                                if (period.ToDate.HasValue && period.ToDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Where(x => x.PlanUntil <= period.ToDate.Value.Date);
                                                }
                                            }
                                        }
                                    }

                                    break;
                                }
                        }
                    }
                }
            }

            return queryableEntity;

        }


        /// <summary>
        /// Filters by SubhireEntity
        /// </summary>
        /// <param name="filterList"></param>
        /// <param name="queryableEntity"></param>
        /// <returns></returns>
        public static IQueryable<SubhireEntity> CreateFilterForSubhireEntity(this IQueryable<SubhireEntity> queryableEntity, List<SelectedFilter> filterList, out string searchText)
        {
            searchText = "";
            if (filterList != null)
            {
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
                                        searchText = values.FirstOrDefault().ToLower();
                                    }

                                    break;
                                }
                            case FilterEnum.Period:
                                {
                                    if (filter.Values != null && filter.Values.Count > 0)
                                    {
                                        PeriodDto period = JsonConvert.DeserializeObject<PeriodDto>(filter.Values[0].ToString());

                                        if (period != null)
                                        {
                                            if (period.PeriodType == PeriodTypeEnum.Period)
                                            {
                                                DateTime date = DateTime.UtcNow;
                                                DateTime dateFrom = DateTime.UtcNow;
                                                DateTime dateTo = DateTime.UtcNow;
                                                if (period.DurationTime == DurationTimeEnum.Next)
                                                {
                                                    dateFrom = date;
                                                    dateTo = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, period.Duration);
                                                }
                                                else if (period.DurationTime == DurationTimeEnum.Past)
                                                {
                                                    dateFrom = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, -period.Duration);
                                                    dateTo = date;
                                                }

                                                queryableEntity = queryableEntity.Where(x => x.UsagePeriodStart >= dateFrom.Date && x.UsagePeriodEnd <= dateTo.Date);


                                            }
                                            else if (period.PeriodType == PeriodTypeEnum.FromToUntil)
                                            {
                                                if (period.FromDate.HasValue && period.FromDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Where(x => x.UsagePeriodStart >= period.FromDate.Value.Date);
                                                }

                                                if (period.ToDate.HasValue && period.ToDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Where(x => x.UsagePeriodEnd <= period.ToDate.Value.Date);
                                                }
                                            }
                                        }
                                    }

                                    break;
                                }
                            case FilterEnum.Tags:
                                {
                                    if (filter.Values?.Count > 0)
                                    {
                                        queryableEntity = queryableEntity.Include(x => x.Tags).ThenInclude(y => y.TagDirectory);

                                        if (filter.SelectedAll)
                                        {
                                            queryableEntity = queryableEntity.Where(x => filter.Values.Any(i => i.ToString() == x.CreationUserId));
                                        }
                                        else
                                        {
                                            queryableEntity = queryableEntity.Where(x => x.Tags.Any(t => filter.Values.Contains(t.TagDirectoryId)));
                                        }
                                    }

                                    break;
                                }
                        }
                    }
                }
            }

            return queryableEntity.OrderByDescending(x => x.CreationDate);
        }



        /// <summary>
        /// Filters by CrewMemberEntity
        /// </summary>
        /// <param name="filterList"></param>
        /// <param name="queryableEntity"></param>
        /// <returns></returns>
        public static IQueryable<ContactEntity> CreateFilterForContactEntity(this IQueryable<ContactEntity> queryableEntity, List<SelectedFilter> filterList, out string searchText)
        {
            searchText = String.Empty;
            if (filterList != null)
            {
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
                            case FilterEnum.Folders:
                                {
                                    queryableEntity = queryableEntity.Where(x => x.FolderId > 0 && filter.Values.Any(i => Convert.ToInt32(i) == x.FolderId));
                                    break;
                                }
                            case FilterEnum.Tags:
                                {
                                    if (filter.Values?.Count > 0)
                                    {
                                        queryableEntity = queryableEntity.Include(x => x.Tags).ThenInclude(y => y.TagDirectory);

                                        if (filter.SelectedAll)
                                        {
                                            queryableEntity = queryableEntity.Where(x => x.Tags.Any());
                                        }
                                        else
                                        {
                                            queryableEntity = queryableEntity.Where(x => x.Tags.Any(t => filter.Values.Contains(t.TagDirectoryId)));
                                        }
                                    }

                                    break;
                                }
                        }
                    }
                }
            }

            return queryableEntity.OrderByDescending(x => x.CreationDate);
        }

        public static IQueryable<TaskEntity> CreateFilterForTaskEntity(this IQueryable<TaskEntity> queryableEntity, List<SelectedFilter> filterList, out string searchText)
        {
            searchText = String.Empty;
            if (filterList != null)
            {
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
                                        queryableEntity = queryableEntity.Include(x => x.Tags).ThenInclude(y => y.TagDirectory);

                                        if (filter.SelectedAll)
                                        {
                                            queryableEntity = queryableEntity.Where(x => x.Tags.Any());
                                        }
                                        else
                                        {
                                            queryableEntity = queryableEntity.Where(x => x.Tags.Any(t => filter.Values.Contains(t.TagDirectoryId)));
                                        }
                                    }

                                    break;
                                }
                            case FilterEnum.Period:
                                {
                                    if (filter.Values != null && filter.Values.Count > 0)
                                    {
                                        PeriodDto period = JsonConvert.DeserializeObject<PeriodDto>(filter.Values[0].ToString());

                                        if (period != null)
                                        {
                                            if (period.PeriodType == PeriodTypeEnum.Period)
                                            {
                                                DateTime date = DateTime.UtcNow;
                                                DateTime dateFrom = DateTime.UtcNow;
                                                DateTime dateTo = DateTime.UtcNow;
                                                if (period.DurationTime == DurationTimeEnum.Next)
                                                {
                                                    dateFrom = date;
                                                    dateTo = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, period.Duration);
                                                }
                                                else if (period.DurationTime == DurationTimeEnum.Past)
                                                {
                                                    dateFrom = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, -period.Duration);
                                                    dateTo = date;
                                                }

                                                queryableEntity = queryableEntity.Where(x =>  x.DeadLine.Date >= dateFrom.Date && x.DeadLine.Date <= dateTo.Date);

                                            }
                                            else if (period.PeriodType == PeriodTypeEnum.FromToUntil)
                                            {
                                                if (period.FromDate.HasValue && period.FromDate.Value != DateTime.MinValue && !period.ToDate.HasValue)
                                                {
                                                    queryableEntity = queryableEntity.Where(x => x.DeadLine.Date >= period.FromDate.Value.Date);
                                                }

                                                if (period.ToDate.HasValue && period.ToDate.Value != DateTime.MinValue && !period.FromDate.HasValue)
                                                {
                                                    queryableEntity = queryableEntity.Where(x => x.DeadLine.Date <= period.ToDate.Value.Date);
                                                }

                                                if (period.ToDate.HasValue && period.ToDate.Value != DateTime.MinValue && period.FromDate.HasValue && period.FromDate.Value != DateTime.MinValue)
                                                {
                                                    queryableEntity = queryableEntity.Where(x => x.DeadLine >= period.FromDate.Value.Date && x.DeadLine.Date <= period.ToDate.Value.Date);
                                                }
                                            }
                                        }
                                    }

                                    break;
                                }
                        }
                    }
                }
            }

            return queryableEntity.OrderByDescending(x => x.CreationDate);
        }


    }
}


