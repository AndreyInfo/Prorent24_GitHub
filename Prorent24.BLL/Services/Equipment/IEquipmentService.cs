using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.Common.Models.Filters;
using Prorent24.Entity.Equipment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Equipment
{
    public interface IEquipmentService: IBaseService<EquipmentDto, int>
    {
        Task<BaseListDto> GetEquipmentGroupsByFolder(List<SelectedFilter> filterList);
        Task<List<ModuleDetailDto>> GetDetails(int id);
        Task<List<EquipmentDto>> Search(string text);
        Task SetFolderId(int equipmentId, int folderId);
        Task Import(List<EquipmentEntity> entities);
        Task<string> Export(string[] columns, List<SelectedFilter> filterList);
    }
}
