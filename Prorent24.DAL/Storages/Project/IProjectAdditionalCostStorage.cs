using Prorent24.Entity.Project;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Project
{
    public interface IProjectAdditionalCostStorage : IBaseStorage<ProjectAdditionalCostEntity>, IForeignDependencyStorage<ProjectAdditionalCostEntity>
    {
        Task<List<ProjectAdditionalCostEntity>> GetAllByProject(int projectId);
    }
}
