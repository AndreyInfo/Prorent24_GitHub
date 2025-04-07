using Prorent24.BLL.Models.Configuration.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.Settings.ProjectType
{
    public interface IProjectTypeService : IBaseService<ProjectTypeDto, int>
    {
        Task<ProjectTypeDefaultDto> GetProjectTypeByDefault();
        Task<ProjectTypeDefaultDto> UpdateProjectTypeByDefault(ProjectTypeDefaultDto defaultProjectType);

        /// <summary>
        /// Delete data multiple
        /// </summary>
        /// <param name="Values"></param>
        /// <returns></returns>
        Task<bool> DeleteMultiple(List<int> Values);
    }
}
