using Prorent24.Entity.General;
using Prorent24.Enum.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.General.Tag
{
    public interface ITagStorage : IBaseStorage<TagEntity>
    {
        Task<List<TagEntity>> GetListTags(ModuleTypeEnum menuType);
        Task<List<TagEntity>> SearchListTags(ModuleTypeEnum moduleType, string search_string);
    }
}
