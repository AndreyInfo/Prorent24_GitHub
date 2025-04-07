using Prorent24.BLL.Models.Project;
using Prorent24.Enum.Project;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Project.Movement
{
    public interface IProjectEquipmentMovementService
    {
        Task<List<ProjectEquipmentMovementDto>> GetListEquipmentMovement(int projectId);
        Task<ProjectEquipmentMovementDto> MovementEquipment(int projectId, int groupId, ProjectEquipmentMovementDto dto);
        Task<List<ProjectEquipmentMovementDto>> MovementEquipments(int projectId, List<ProjectEquipmentMovementDto> dtos);
        Task<List<ProjectEquipmentMovementDto>> MovementEquipmentsByProjectId(int projectId, ProjectChangeStatusEnum status);
        Task<List<ProjectEquipmentMovementDto>> MovementEquipmentsByProjectId(int projectId, ProjectStatusEnum status);
    }
}
