using Prorent24.Entity.Project;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Project
{
    public interface IProjectFinancialStorage: IBaseStorage<ProjectFinancialEntity>
    {
        /// <summary>
        /// Get by ProjectId
        /// </summary>
        /// <returns></returns>
        Task<ProjectFinancialEntity> GetByProjectId(int projectId);

}
}
