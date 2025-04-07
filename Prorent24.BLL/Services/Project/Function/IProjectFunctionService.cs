using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Project;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Project.Function
{
    public interface IProjectFunctionService
    {
        Task<BaseListDto> GetAllFunctionsByProject(int? projectId);

        Task<ProjectFunctionDto> GetFunctionById(int id);

        Task<ProjectFunctionDto> CreateFunction(ProjectFunctionDto model);

        Task<ProjectFunctionGridDto> CreateFunctionForProject(ProjectFunctionDto model, VatSchemeRateDto vatScheme);
        

        Task<ProjectFunctionDto> UpdateFunction(ProjectFunctionDto model);

        Task<bool> DeleteFunction(int id);
        Task<bool> DeleteGroup(int id);
        Task<BaseListDto> GetAllGroupByProject(int projectId);
        Task<ProjectFunctionGridDto> UpdateGroup(ProjectFunctionGroupDto dto);
        Task<ProjectFunctionGridDto> CreateGroup(ProjectFunctionGroupDto dto);

        Task<ProjectFunctionGridDto> UpdateFunctionForProject(ProjectFunctionDto model, VatSchemeRateDto vatScheme);
    }
}
