using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.Common.Models.Filters;
using Prorent24.Enum.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.Filter
{
    public interface IFilterService
    {
        /// <summary>
        /// Get list filters by MenuType
        /// </summary>
        /// <param name="menuType"></param>
        /// <returns></returns>
        Task<List<FilterListDto>> GetListFilters(ModuleTypeEnum moduleType);

        /// <summary>
        /// Get filters by preset
        /// </summary>
        /// <param name="presetId"></param>
        /// <returns></returns>
        Task<List<FilterListDto>> GetFiltersByPreset(int presetId);

        /// <summary>
        /// Create filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<SavedFilterDto> SaveFilter(SavedFilterDto filter);

        /// <summary>
        /// Delete filter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteFilter(int id);
    }
}
