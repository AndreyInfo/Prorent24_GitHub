using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Project;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Project
{
    public interface IProjectService:IBaseService<ProjectDto, int>
    {
        Task<List<ProjectDto>> GeList();

        Task<List<ModuleDetailDto>> GetDetails(int id);

        Task<bool> DeleteTime(int id);

        Task<ProjectTimeDto> CreateTime(ProjectTimeDto model);
        
        Task<ProjectTimeDto> UpdateTime(ProjectTimeDto model);

        Task ChangeStatus(int id, ProjectChangeStatusEnum status);
        Task SetStatus(int id, ProjectStatusEnum status);
    }
}
