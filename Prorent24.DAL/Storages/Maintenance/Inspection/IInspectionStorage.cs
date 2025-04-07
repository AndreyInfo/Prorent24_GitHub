using Prorent24.Common.Models.Filters;
using Prorent24.Entity.Maintenance;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Maintenance.Inspection
{
    public interface IInspectionStorage: IBaseStorage<InspectionEntity>
    {
        Task<IPagedList<InspectionEntity>> GetAllByFilter(List<SelectedFilter> filterList);
    }
}
