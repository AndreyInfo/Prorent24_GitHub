using Prorent24.BLL.Models.CrewPlanner;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Schedulers;
using Prorent24.Common.Models.Warehouse;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.CrewPlanner
{
    public interface ICrewPlannerService
    {
        /// <summary>
        /// Create CrewPlanner
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<CrewPlannerDto> Create(CrewPlannerDto model);

        Task<CrewPlannerDto> Update(CrewPlannerDto model);

        /// <summary>
        /// Get Short Scheduler CrewMember
        /// </summary>
        /// <returns></returns>
        Task<List<SchedulerCrewMember>> GetShortSchedulerCrewMember();

        Task<WarhouseEventResourseModel> GetAll(ProjectFunctionTypeEnum type, List<string> ids, List<SelectedFilter> filters);

        Task<WarhouseEventResourseModel> GetAllForModuleTimeLine(ProjectFunctionTypeEnum type, List<string> ids, DateTime? dateFrom, DateTime? dateUntil);
    }
}
