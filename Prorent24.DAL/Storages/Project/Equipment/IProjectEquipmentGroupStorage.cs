using Prorent24.Entity.Project;
using Prorent24.Entity.Subhire;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Project
{
    public interface IProjectEquipmentGroupStorage : IBaseStorage<ProjectEquipmentGroupEntity>
    {
        Task<List<ProjectEquipmentGroupEntity>> GetAllByProject(int projectId);

    }
}
