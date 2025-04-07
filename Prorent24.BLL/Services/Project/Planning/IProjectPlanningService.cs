using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Project;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Warehouse;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Project.Planning
{
    public interface IProjectPlanningService
    {
        Task<ProjectPlanningGridDto> CreatePlanning(ProjectPlanningDto model);

        Task<ProjectPlanningGridDto> UpdatePlanning(ProjectPlanningDto model);

        Task<bool> DeletePlanning(int id);

        Task<BaseListDto> GetGridPlanningByGroupedFunctions(ProjectPlanningFilter filter);
        Task<List<ProjectPlanningGridDto>> GetPlanningGroupedFunctions(int projectId, ProjectFunctionTypeEnum type);

        Task<ProjectPlanningDto> GetPlanningById(int id);
        Task<WarhouseEventResourseModel> GetWarhouseTimeLineCrewOrTransport(DateTime date, ProjectFunctionTypeEnum projectFunctionType);

        Task<WarhouseEventResourseModel> GetForGeneralTimeLine(List<SelectedFilter> filters);

        Task<WarhouseEventResourseModel> CreatePlanningFromCrewPlanner(int projectId, List<ProjectPlanningDto> model, ProjectFunctionTypeEnum type, int functionId);

    }
}
