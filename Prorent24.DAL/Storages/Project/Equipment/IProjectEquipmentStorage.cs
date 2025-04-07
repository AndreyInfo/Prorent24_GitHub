using Prorent24.Entity.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Project
{
    public interface IProjectEquipmentStorage
    {
        Task<ProjectEquipmentEntity> Create(ProjectEquipmentEntity model);
        
        Task<ProjectEquipmentEntity> Update(ProjectEquipmentEntity model);
        Task<List<ProjectEquipmentEntity>> Update(List<ProjectEquipmentEntity> models);

        Task<bool> Delete(int id);

        Task<ProjectEquipmentEntity> GetById(object id);
        Task<List<ProjectEquipmentEntity>> GetAllByProject(int projectId);

        Task<ProjectEquipmentEntity> GetForEquipmentMovement(int projectId, int groupId, int projEquipmentId);
        Task<List<ProjectEquipmentEntity>> GetByGroupId(int id);

        IQueryable<ProjectEquipmentEntity> QueryableEntity { get; }

        Task<List<ProjectEquipmentEntity>> GetAllByFilter(IQueryable<ProjectEquipmentEntity> queryableEntity, string searchText);
    }
}
