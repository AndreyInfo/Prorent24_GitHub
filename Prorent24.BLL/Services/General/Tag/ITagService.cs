using Prorent24.BLL.Models.General.Tag;
using Prorent24.Enum.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.Tag
{
    public interface ITagService //: IBaseService<TagDto, int>
    {
        /// <summary>
        /// Get list tags
        /// </summary>
        /// <returns></returns>
        Task<List<TagDto>> GetTags(ModuleTypeEnum moduleType);

        Task<List<TagDto>> SearchTags(ModuleTypeEnum moduleType, string search_string);

        Task<TagDto> CreateTag(TagDto model);

        Task<bool> Delete(int id);
    }
}
