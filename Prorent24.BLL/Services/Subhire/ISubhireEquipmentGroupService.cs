using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Subhire;
using Prorent24.Common.Models.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Subhire
{
    public interface ISubhireEquipmentGroupService
    {
        Task<List<SubhireEquipmentGroupDto>> GetAllGroupBySubhire(int subhireId);

        Task<SubhireEquipmentGroupDto> CreateGroup(SubhireEquipmentGroupDto model);

        Task<SubhireEquipmentGroupDto> GetGroupById(int id);

        Task<SubhireEquipmentGroupDto> UpdateGroup(SubhireEquipmentGroupDto model);

        Task<bool> DeleteGroup(int id);
        Task<BaseListDto> GetEquipmentsBySubhire(int subhireId);

        Task<SubhireEquipmentGridDto> CreateEquipment(SubhireEquipmentDto model);

        Task<SubhireEquipmentGridDto> UpdateEquipment(SubhireEquipmentDto model);

        Task<bool> DeleteEquipment(int id);

        Task<BaseListDto> GetShortages(List<SelectedFilter> filters);
    }
}
