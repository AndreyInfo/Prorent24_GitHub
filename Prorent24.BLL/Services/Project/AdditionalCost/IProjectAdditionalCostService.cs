using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Project.AdditionalCost
{
    public interface IProjectAdditionalCostService : IBaseService<ProjectAdditionalCostDto, int>, IForeignDependencyService<ProjectAdditionalCostDto>
    {
        Task<List<ProjectAdditionalCostDto>> GetAdditionalCosts(int projectId);
    }
}
