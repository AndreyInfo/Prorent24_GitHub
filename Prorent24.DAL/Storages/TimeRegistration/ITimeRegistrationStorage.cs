using Prorent24.Common.Models.Filters;
using Prorent24.Entity.TimeRegistration;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.TimeRegistration
{
    public interface ITimeRegistrationStorage : IBaseStorage<TimeRegistrationEntity>
    {
        IQueryable<TimeRegistrationEntity> GetAllByFilter(List<SelectedFilter> filterList);


        Task<IPagedList<TimeRegistrationEntity>> GetAllByFilter(IQueryable<TimeRegistrationEntity> queryableEntity, string searchText);
    }
}
