using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Models.Subhire;
using Prorent24.Common.Models.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Project.Equipment
{
    public interface IProjectEquipmentService
    {
        Task<List<ProjectEquipmentGroupDto>> GetAllGroupByProject(int projectId);
        Task<BaseListDto> GetBaseGridAllEquipmentsByProjectId(int projectId);
        Task<List<ProjectEquipmentGridDto>> GetEquipmentsByProjectId(int projectId);

        Task<ProjectEquipmentGridDto> CreateGroup(ProjectEquipmentGroupDto model);

        Task<ProjectEquipmentGridDto> UpdateGroup(ProjectEquipmentGroupDto model);

        Task<bool> DeleteGroup(int id);

        Task<ProjectEquipmentGridDto> CreateEquipment(ProjectEquipmentDto model);

        Task<ProjectEquipmentGridDto> UpdateEquipment(ProjectEquipmentDto model);

        Task<bool> DeleteEquipment(int id);

        Task<ProjectEquipmentDto> GetById(int id);

        Task<List<ModuleDetailDto>> GetDetails(int id);

        Task<BaseListDto> GetSublist(object id);
    }
}
