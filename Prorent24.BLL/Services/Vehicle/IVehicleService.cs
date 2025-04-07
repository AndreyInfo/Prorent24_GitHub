using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Vehicle;
using Prorent24.Common.Models.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Vehicle
{
    public interface IVehicleService : IBaseService<VehicleDto, int>
    {
        Task<List<ModuleDetailDto>> GetDetails(int id);
        Task Import(List<VehicleDto> vehicleDtos);
        Task<BaseListDto> GetForPlanning();
        Task<string> Export(string[] columns, List<SelectedFilter> filterList);
    }
}
