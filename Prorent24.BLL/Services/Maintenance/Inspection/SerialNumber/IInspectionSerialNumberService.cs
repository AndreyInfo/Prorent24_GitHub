using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Maintenance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Maintenance.Inspection.SerialNumber
{
    public interface IInspectionSerialNumberService : IBaseService<InspectionSerialNumberDto, int>
    {
        Task<BaseListDto> GetPagedList(int id);
    }
}
