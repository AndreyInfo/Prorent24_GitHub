using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.TimeRegistration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.TimeRegistration
{
    public interface ITimeRegistrationService : IBaseService<TimeRegistrationDto, int>
    {
        Task<List<ModuleDetailDto>> GetDetails(int id);
    }
}
