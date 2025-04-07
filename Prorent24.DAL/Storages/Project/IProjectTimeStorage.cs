using Prorent24.Entity.Project;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Project
{
    public interface IProjectTimeStorage
    {
        Task<ProjectTimeEntity> Create(ProjectTimeEntity model);

        Task<ProjectTimeEntity> Update(ProjectTimeEntity model);

        Task<bool> Delete(int id);
        Task<bool> DeleteAllOtherByProjectId(int Id);
    }
}
