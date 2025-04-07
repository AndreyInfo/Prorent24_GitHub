using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Subhire;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Subhire
{
    public interface ISubhireService : IBaseService<SubhireDto, int>
    {
        Task<List<ModuleDetailDto>> GetDetails(int id);
        Task<List<ModuleDetailDto>> GetShortageDetails(int projectId, int equipmentId, int[] subhireIds);

        Task<SubhireListDto> GetSubhiresByProject(List<int> projectIds);

        Task<SubhireDto> SaveFromShortage(int? subhireId, List<ShortageEquipmentDto> equipments, DateTime? from, DateTime? to);
        
    }
}
