using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Maintenance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Maintenance.Inspection
{
    public interface IInspectionService: IBaseService<InspectionDto, int>
    {
        Task<List<ModuleDetailDto>> GetDetails(int id);
    }
}
