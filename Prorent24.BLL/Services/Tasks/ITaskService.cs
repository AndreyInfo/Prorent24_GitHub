using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Tasks
{
    public interface ITaskService : IBaseService<TaskDto, int>
    {
        Task<List<ModuleDetailDto>> GetDetails(int id);

        Task<TaskDto> CloseTask(int id, DateTime ? date);
    }
}
