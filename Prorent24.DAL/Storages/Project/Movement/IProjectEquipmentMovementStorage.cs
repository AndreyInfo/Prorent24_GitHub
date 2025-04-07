using Prorent24.Entity.Project;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Project.Movement
{
    public interface IProjectEquipmentMovementStorage : IBaseStorage<ProjectEquipmentMovementEntity>
    {
        Task<List<ProjectEquipmentMovementEntity>> GetEquipmentMovementByProjectId(int projectId);
        Task MoveEquipment(ProjectEquipmentMovementEntity entity);
        Task MoveEquipments(List<ProjectEquipmentMovementEntity> entities);
        Task<List<ProjectEquipmentMovementEntity>> GetEquipmentMovementByProjectEquipmentId(int projectEquipmentId);
        Task UpdateMoveEquipments(List<ProjectEquipmentMovementEntity> entities);
        Task CreateMoveEquipments(List<ProjectEquipmentMovementEntity> entities);
        
    }
}
